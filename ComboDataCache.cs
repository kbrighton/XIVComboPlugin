using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Dalamud.Game;
using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.ClientState.Statuses;

namespace XIVComboVX {
	internal class ComboDataCache: IDisposable {
		protected const uint InvalidObjectID = 0xE000_0000;

		// Invalidate these
		private readonly Dictionary<(uint StatusID, uint? TargetID, uint? SourceID), Status?> statusCache = new();
		private readonly Dictionary<uint, CooldownData> cooldownCache = new();
		private bool? canInterruptTarget = null;

		// Do not invalidate these
		private readonly Dictionary<uint, byte> cooldownGroupCache = new();
		private readonly Dictionary<Type, JobGaugeBase> jobGaugeCache = new();

		#region Core/setup

		private delegate IntPtr GetActionCooldownSlotDelegate(IntPtr actionManager, int cooldownGroup);
		private readonly GetActionCooldownSlotDelegate getActionCooldownSlot;
		private IntPtr actionManager = IntPtr.Zero;

		public ComboDataCache() {
			this.getActionCooldownSlot = Marshal.GetDelegateForFunctionPointer<GetActionCooldownSlotDelegate>(Service.Address.GetActionCooldown);

			Service.Framework.Update += this.invalidateCache;
		}

		public void Dispose() => Service.Framework.Update -= this.invalidateCache;

		internal void updateActionManager(IntPtr address) => this.actionManager = address;

		#endregion

		public bool CanInterruptTarget {
			get {
				if (!this.canInterruptTarget.HasValue) {
					GameObject? target = Service.Targets.Target;
					this.canInterruptTarget = target is not null
						&& target is BattleChara actor
						&& actor.IsCasting
						&& actor.IsCastInterruptible;
				}
				return this.canInterruptTarget.Value;
			}
		}

		public T GetJobGauge<T>() where T : JobGaugeBase {
			if (!this.jobGaugeCache.TryGetValue(typeof(T), out JobGaugeBase? gauge))
				gauge = this.jobGaugeCache[typeof(T)] = Service.JobGauge.Get<T>();

			return (T)gauge;
		}

		public Status? GetStatus(uint statusID, GameObject? actor, uint? sourceID) {
			(uint statusID, uint? ObjectId, uint? sourceID) key = (statusID, actor?.ObjectId, sourceID);
			if (this.statusCache.TryGetValue(key, out Status? found))
				return found;

			if (actor is null)
				return this.statusCache[key] = null;

			if (actor is not BattleChara chara)
				return this.statusCache[key] = null;

			foreach (Status? status in chara.StatusList) {
				if (status.StatusId == statusID && (!sourceID.HasValue || status.SourceID == 0 || status.SourceID == InvalidObjectID || status.SourceID == sourceID))
					return this.statusCache[key] = status;
			}

			return this.statusCache[key] = null;
		}

		public unsafe CooldownData GetCooldown(uint actionID) {
			if (this.cooldownCache.TryGetValue(actionID, out CooldownData found))
				return found;

			byte cooldownGroup = this.getCooldownGroup(actionID);
			if (this.actionManager == IntPtr.Zero)
				return this.cooldownCache[actionID] = new CooldownData() { ActionID = actionID };

			IntPtr cooldownPtr = this.getActionCooldownSlot(this.actionManager, cooldownGroup - 1);
			return this.cooldownCache[actionID] = *(CooldownData*)cooldownPtr;
		}

		private byte getCooldownGroup(uint actionID) {
			if (this.cooldownGroupCache.TryGetValue(actionID, out byte cooldownGroup))
				return cooldownGroup;

			Lumina.Excel.ExcelSheet<Lumina.Excel.GeneratedSheets.Action>? sheet = Service.GameData.GetExcelSheet<Lumina.Excel.GeneratedSheets.Action>()!;
			Lumina.Excel.GeneratedSheets.Action? row = sheet.GetRow(actionID);

			return this.cooldownGroupCache[actionID] = row!.CooldownGroup;
		}

		private unsafe void invalidateCache(Framework framework) {
			this.statusCache.Clear();
			this.cooldownCache.Clear();
			this.canInterruptTarget = null;
		}
	}
}