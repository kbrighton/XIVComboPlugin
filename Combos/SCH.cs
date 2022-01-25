using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboVX.Combos {
	internal static class SCH {
		public const byte JobID = 28;

		public const uint
			Resurrection = 173,
			FeyBless = 16543,
			Consolation = 16546,
			EnergyDrain = 167,
			Aetherflow = 166,
			Lustrate = 189,
			Indomitability = 3583,
			Excogitation = 7434;

		public static class Buffs {
			public const ushort
				Recitation = 1896;
		}

		public static class Debuffs {
			// public const ushort placeholder = 0;
		}

		public static class Levels {
			public const byte
				Aetherflow = 45,
				Lustrate = 45,
				Excogitation = 62,
				Consolation = 80;
		}
	}

	internal class ScholarSwiftcastRaiserFeature: SwiftRaiseCombo {
		public override CustomComboPreset Preset => CustomComboPreset.ScholarSwiftcastRaiserFeature;
		public override uint[] ActionIDs { get; } = new[] { SCH.Resurrection };
	}

	internal class ScholarExcogFallbackFeature: CustomCombo {
		public override CustomComboPreset Preset => CustomComboPreset.ScholarExcogFallbackFeature;
		public override uint[] ActionIDs { get; } = new[] { SCH.Excogitation };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level < SCH.Levels.Excogitation || IsOnCooldown(SCH.Excogitation))
				return SCH.Lustrate;

			return actionID;
		}
	}

	internal class ScholarSeraphConsolationFeature: CustomCombo {
		public override CustomComboPreset Preset => CustomComboPreset.ScholarSeraphConsolationFeature;
		public override uint[] ActionIDs { get; } = new[] { SCH.FeyBless };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= SCH.Levels.Consolation && GetJobGauge<SCHGauge>().SeraphTimer > 0)
				return SCH.Consolation;

			return actionID;
		}
	}

	internal class ScholarEnergyDrainFeature: CustomCombo {
		public override CustomComboPreset Preset => CustomComboPreset.ScholarEnergyDrainFeature;
		public override uint[] ActionIDs { get; } = new[] { SCH.EnergyDrain };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= SCH.Levels.Aetherflow && GetJobGauge<SCHGauge>().Aetherflow == 0)
				return SCH.Aetherflow;

			return actionID;
		}
	}

	internal class ScholarLustrate: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.ScholarLustrateAetherflowFeature;
		public override uint[] ActionIDs { get; } = new[] { SCH.Lustrate };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= SCH.Levels.Aetherflow && GetJobGauge<SCHGauge>().Aetherflow == 0)
				return SCH.Aetherflow;

			return actionID;
		}
	}

	internal class ScholarIndomitability: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.ScholarIndomAetherflowFeature;
		public override uint[] ActionIDs { get; } = new[] { SCH.EnergyDrain };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= SCH.Levels.Aetherflow && GetJobGauge<SCHGauge>().Aetherflow == 0 && !SelfHasEffect(SCH.Buffs.Recitation))
				return SCH.Aetherflow;

			return actionID;
		}
	}

}
