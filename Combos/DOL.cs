
using Dalamud.Game.ClientState.Conditions;

using XIVComboVX;
using XIVComboVX.Combos;

namespace XIVCombo.Combos {
	internal static class DOL {
		public const byte JobID = 51;

		public const uint
			AgelessWords = 215,
			SolidReason = 232,
			Cast = 289,
			Hook = 296,
			MinWiseToTheWorld = 26521,
			BtnWiseToTheWorld = 26522;

		public static class Buffs {
			public const ushort
				EurekaMoment = 2765;
		}

		public static class Debuffs {
			public const ushort
				Placeholder = 0;
		}

		public static class Levels {
			public const byte WiseToTheWorld = 90;
		}
	}

	internal class EurekaFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.DolEurekaFeature;
		protected internal override uint[] ActionIDs => new[] { DOL.SolidReason, DOL.AgelessWords };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= DOL.Levels.WiseToTheWorld && SelfHasEffect(DOL.Buffs.EurekaMoment))
				return actionID is DOL.SolidReason
					? DOL.MinWiseToTheWorld
					: DOL.BtnWiseToTheWorld;

			return actionID;
		}
	}

	internal class FisherCastHookFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.DolCastHookFeature;
		protected internal override uint[] ActionIDs => new[] { DOL.Cast };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (HasCondition(ConditionFlag.Fishing))
				return DOL.Hook;

			return actionID;
		}
	}
}
