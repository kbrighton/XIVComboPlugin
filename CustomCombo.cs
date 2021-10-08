using System.Linq;

using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.ClientState.Statuses;

namespace XIVComboVeryExpandedPlugin.Combos {
	internal abstract class CustomCombo {
		#region static 

		private static IconReplacer iconReplacer = null!;
		protected static XIVComboVeryExpandedConfiguration Configuration = null!;

		public static void Initialize(IconReplacer iconReplacer, XIVComboVeryExpandedConfiguration configuration) {
			CustomCombo.iconReplacer = iconReplacer;
			Configuration = configuration;
		}

		#endregion

		protected abstract CustomComboPreset Preset { get; }

		protected byte JobID { get; set; }
		public byte ClassID => this.JobID switch {
			>= 19 and <= 25 => (byte)(this.JobID - 18),
			27 or 28 => 26,
			30 => 29,
			_ => this.JobID,
		};

		protected virtual uint[] ActionIDs { get; set; }

		protected CustomCombo() {
			CustomComboInfoAttribute presetInfo = this.Preset.GetInfo();
			this.JobID = presetInfo.JobID;
			this.ActionIDs = presetInfo.ActionIDs;
		}

		public bool TryInvoke(uint actionID, uint lastComboMove, float comboTime, byte level, out uint newActionID) {
			newActionID = 0;

			if (LocalPlayer is null)
				return false;

			if (!IsEnabled(this.Preset))
				return false;

			if ((this.JobID != LocalPlayer.ClassJob.Id && this.ClassID != LocalPlayer.ClassJob.Id) || !this.ActionIDs.Contains(actionID))
				return false;

			uint resultingActionID = this.Invoke(actionID, lastComboMove, comboTime, level);
			if (resultingActionID == 0 || actionID == resultingActionID)
				return false;

			newActionID = resultingActionID;
			return true;
		}

		protected abstract uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level);

		#region Utility/convenience getters

		protected static uint OriginalHook(uint actionID) => iconReplacer.OriginalHook(actionID);

		protected static PlayerCharacter? LocalPlayer => XIVComboVeryExpandedPlugin.client.LocalPlayer;

		protected static GameObject? CurrentTarget => XIVComboVeryExpandedPlugin.targets.Target;

		protected static bool IsEnabled(CustomComboPreset preset) => Configuration.IsEnabled(preset);

		protected static bool HasCondition(ConditionFlag flag) => XIVComboVeryExpandedPlugin.conditions[flag];

		protected static CooldownData GetCooldown(uint actionID) => iconReplacer.GetCooldown(actionID);

		protected static T GetJobGauge<T>() where T : JobGaugeBase => XIVComboVeryExpandedPlugin.jobGauge.Get<T>();

		#endregion

		#region Effects

		protected static bool HasEffect(short effectId) => FindEffect(effectId) != null;

		protected static bool TargetHasEffect(short effectId) => FindTargetEffect(effectId) != null;

		protected static Status? FindEffect(short effectId) => FindEffect(effectId, LocalPlayer, null);

		protected static float EffectDuration(short effectId) {
			Status? eff = FindEffect(effectId);
			return eff?.RemainingTime ?? 0;
		}

		protected static float EffectStacks(short effectId) {
			Status? eff = FindEffect(effectId);
			return eff?.StackCount ?? 0;
		}

		protected static float TargetEffectDuration(short effectId) {
			Status? eff = FindTargetEffect(effectId);
			return eff?.RemainingTime ?? 0;
		}

		protected static float TargetEffectStacks(short effectId) {
			Status? eff = FindTargetEffect(effectId);
			return eff?.StackCount ?? 0;
		}

		protected static Status? FindTargetEffect(short effectId) => FindEffect(effectId, CurrentTarget, LocalPlayer?.ObjectId);

		protected static Status? FindEffect(short effectId, GameObject? actor, uint? sourceId) {
			if (actor is null)
				return null;
			if (actor is BattleChara chara)
				foreach (Status status in chara.StatusList) {
					if (status.StatusId == effectId) {
						if (!sourceId.HasValue || status.SourceID == sourceId)
							return status;
					}
				}

			return null;
		}

		#endregion
	}
}