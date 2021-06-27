using System.Linq;

using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Actors.Types;

using Structs = Dalamud.Game.ClientState.Structs;

namespace XIVComboVeryExpandedPlugin.Combos {
	internal abstract class CustomCombo {
		#region static 

		private static IconReplacer IconReplacer;
		protected static XIVComboVeryExpandedConfiguration Configuration;

		public static void Initialize(IconReplacer iconReplacer, XIVComboVeryExpandedConfiguration configuration) {
			IconReplacer = iconReplacer;
			Configuration = configuration;
		}

		#endregion

		protected abstract CustomComboPreset Preset { get; }

		protected byte JobID { get; set; }

		protected virtual uint[] ActionIDs { get; set; }

		protected CustomCombo() {
			CustomComboInfoAttribute presetInfo = this.Preset.GetInfo();
			this.JobID = presetInfo.JobID;
			this.ActionIDs = presetInfo.ActionIDs;
		}

		public bool TryInvoke(uint actionID, uint lastComboMove, float comboTime, byte level, out uint newActionID) {
			newActionID = 0;

			if (!IsEnabled(this.Preset))
				return false;

			if (this.JobID != LocalPlayer.ClassJob.Id || !this.ActionIDs.Contains(actionID))
				return false;

			uint resultingActionID = this.Invoke(actionID, lastComboMove, comboTime, level);
			if (resultingActionID == 0 || actionID == resultingActionID)
				return false;

			newActionID = resultingActionID;
			return true;
		}

		protected abstract uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level);

		#region Passthru

		protected static uint OriginalHook(uint actionID) {
			return IconReplacer.OriginalHook(actionID);
		}

		protected static PlayerCharacter LocalPlayer => IconReplacer.LocalPlayer;

		protected static Actor CurrentTarget => IconReplacer.CurrentTarget;

		protected static bool IsEnabled(CustomComboPreset preset) {
			return Configuration.IsEnabled(preset);
		}

		protected static bool HasCondition(ConditionFlag flag) {
			return IconReplacer.HasCondition(flag);
		}

		protected static bool HasEffect(short effectID) {
			return IconReplacer.HasEffect(effectID);
		}

		protected static bool TargetHasEffect(short effectID) {
			return IconReplacer.TargetHasEffect(effectID);
		}

		protected static Structs.StatusEffect? FindEffect(short effectId) {
			return IconReplacer.FindEffect(effectId);
		}

		protected static Structs.StatusEffect? FindTargetEffect(short effectId) {
			return IconReplacer.FindTargetEffect(effectId);
		}

		protected static CooldownData GetCooldown(uint actionID) {
			return IconReplacer.GetCooldown(actionID);
		}

		protected static T GetJobGauge<T>() {
			return IconReplacer.GetJobGauge<T>();
		}

		#endregion
	}
}