using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Hooking;
using Dalamud.Logging;

using XIVComboVX.Combos;

namespace XIVComboVX {
	internal class IconReplacer: IDisposable {

		private delegate ulong IsIconReplaceableDelegate(uint actionID);
		private delegate uint GetIconDelegate(IntPtr actionManager, uint actionID);
		private delegate IntPtr GetActionCooldownSlotDelegate(IntPtr actionManager, int cooldownGroup);

		private readonly Hook<IsIconReplaceableDelegate> isIconReplaceableHook;
		private readonly Hook<GetIconDelegate> getIconHook;

		private IntPtr actionManager = IntPtr.Zero;

		private HashSet<uint> comboActionIDs = new();
		private readonly List<CustomCombo> customCombos;

		public IconReplacer() {

			this.customCombos = Assembly.GetAssembly(typeof(CustomCombo))!.GetTypes()
				.Where(t => !t.IsAbstract && (t.BaseType == typeof(CustomCombo) || t.BaseType?.BaseType == typeof(CustomCombo)))
				.Select(t => Activator.CreateInstance(t))
				.Cast<CustomCombo>()
				.ToList();

			this.UpdateEnabledActionIDs();

			this.getIconHook = new Hook<GetIconDelegate>(Service.Address.GetAdjustedActionId, this.getIconDetour);
			this.isIconReplaceableHook = new Hook<IsIconReplaceableDelegate>(Service.Address.IsActionIdReplaceable, this.isIconReplaceableDetour);

			this.getIconHook.Enable();
			this.isIconReplaceableHook.Enable();

		}

		public void Dispose() {
			this.getIconHook?.Dispose();
			this.isIconReplaceableHook?.Dispose();
		}

		/// <summary>
		/// Maps to <see cref="PluginConfiguration.EnabledActions"/>, these actions can potentially update their icon per the user configuration.
		/// </summary>
		public void UpdateEnabledActionIDs() {
			this.comboActionIDs = this.customCombos
				.Where(combo => Service.Configuration.EnabledActions.Contains(combo.Preset))
				.SelectMany(combo => combo.ActionIDs)
				.ToHashSet();
		}

		public uint GetNewAction(uint actionID, uint lastComboMove, float comboTime, byte level) {
			foreach (CustomCombo combo in this.customCombos) {
				if (combo.TryInvoke(actionID, lastComboMove, comboTime, level, out uint newActionID))
					return newActionID;
			}
			return this.OriginalHook(actionID);
		}

		private ulong isIconReplaceableDetour(uint actionID) => 1;

		/// <summary>
		/// Replace an ability with another ability
		/// actionID is the original ability to be "used"
		/// Return either actionID (itself) or a new Action table ID as the
		/// ability to take its place.
		/// I tend to make the "combo chain" button be the last move in the combo
		/// For example, Souleater combo on DRK happens by dragging Souleater
		/// onto your bar and mashing it.
		/// </summary>
		private uint getIconDetour(IntPtr actionManager, uint actionID) {
			try {
				this.actionManager = actionManager;
				Service.DataCache.updateActionManager(actionManager);

				if (LocalPlayer == null || !this.comboActionIDs.Contains(actionID))
					return this.OriginalHook(actionID);

				return this.GetNewAction(actionID, LastComboMove, ComboTime, LocalPlayer?.Level ?? 0);
			}
			catch (Exception ex) {
				PluginLog.Error(ex, "Don't crash the game");
				return this.getIconHook.Original(actionManager, actionID);
			}
		}

		#region Getters
#pragma warning disable IDE1006 // Naming Styles

		internal static PlayerCharacter LocalPlayer => Service.Client.LocalPlayer!;

		internal static uint LastComboMove => (uint)Marshal.ReadInt32(Service.Address.LastComboMove);

		internal static float ComboTime => Marshal.PtrToStructure<float>(Service.Address.ComboTimer);

		internal uint OriginalHook(uint actionID) => this.getIconHook.Original(this.actionManager, actionID);

#pragma warning restore IDE1006 // Naming Styles
		#endregion

	}

	[StructLayout(LayoutKind.Explicit)]
	internal struct CooldownData {
		[FieldOffset(0x0)] public bool IsCooldown;
		[FieldOffset(0x4)] public uint ActionID;
		[FieldOffset(0x8)] public float CooldownElapsed;
		[FieldOffset(0xC)] public float CooldownTotal;

		public float CooldownRemaining => this.IsCooldown ? this.CooldownTotal - this.CooldownElapsed : 0;
	}
}
