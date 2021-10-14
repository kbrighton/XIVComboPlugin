using System.Linq;

using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.ClientState.Statuses;
using Dalamud.Utility;

namespace XIVComboVeryExpandedPlugin.Combos {
	internal abstract class CustomCombo {
		#region static 

		public const uint InvalidObjectID = 0xE000_0000;

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
			CustomComboInfoAttribute presetInfo = this.Preset.GetAttribute<CustomComboInfoAttribute>();
			this.JobID = presetInfo.JobID;
			this.ActionIDs = presetInfo.ActionIDs;
		}

		public bool TryInvoke(uint actionID, uint lastComboActionId, float comboTime, byte level, out uint newActionID) {
			newActionID = 0;

			if (LocalPlayer is null
				|| !IsEnabled(this.Preset)
				|| (this.JobID != LocalPlayer.ClassJob.Id && this.ClassID != LocalPlayer.ClassJob.Id)
				|| !this.ActionIDs.Contains(actionID)
			)
				return false;

			uint resultingActionID = this.Invoke(actionID, lastComboActionId, comboTime, level);
			if (resultingActionID == 0 || actionID == resultingActionID)
				return false;

			newActionID = resultingActionID;
			return true;
		}

		protected abstract uint Invoke(uint actionID, uint lastComboActionId, float comboTime, byte level);

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

		protected static Status? SelfFindEffect(short effectId) => FindEffect(effectId, LocalPlayer, null);
		protected static bool SelfHasEffect(short effectId) => SelfFindEffect(effectId) is not null;
		protected static float SelfEffectDuration(short effectId) {
			Status? eff = SelfFindEffect(effectId);
			return eff?.RemainingTime ?? 0;
		}
		protected static float SelfEffectStacks(short effectId) {
			Status? eff = SelfFindEffect(effectId);
			return eff?.StackCount ?? 0;
		}

		protected static Status? TargetFindAnyEffect(short effectId) => FindEffect(effectId, CurrentTarget, null);
		protected static bool TargetHasAnyEffect(short effectId) => TargetFindAnyEffect(effectId) is not null;
		protected static float TargetAnyEffectDuration(short effectId) {
			Status? eff = TargetFindAnyEffect(effectId);
			return eff?.RemainingTime ?? 0;
		}
		protected static float TargetAnyEffectStacks(short effectId) {
			Status? eff = TargetFindAnyEffect(effectId);
			return eff?.StackCount ?? 0;
		}

		protected static Status? TargetFindOwnEffect(short effectId) => FindEffect(effectId, CurrentTarget, LocalPlayer?.ObjectId);
		protected static bool TargetHasOwnEffect(short effectId) => TargetFindOwnEffect(effectId) is not null;
		protected static float TargetOwnEffectDuration(short effectId) {
			Status? eff = TargetFindOwnEffect(effectId);
			return eff?.RemainingTime ?? 0;
		}
		protected static float TargetOwnEffectStacks(short effectId) {
			Status? eff = TargetFindOwnEffect(effectId);
			return eff?.StackCount ?? 0;
		}

		protected static Status? FindEffect(short effectId, GameObject? actor, uint? sourceId) {
			if (actor is null)
				return null;
			if (actor is not BattleChara chara)
				return null;
			foreach (Status status in chara.StatusList) {
				if (status.StatusId == effectId && (!sourceId.HasValue || status.SourceID == 0 || status.SourceID == InvalidObjectID || status.SourceID == sourceId))
					return status;
			}
			return null;
		}

		#endregion
	}
}