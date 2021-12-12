using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboVX.Combos {
	internal static class SCH {
		public const byte JobID = 28;

		public const uint
			Resurrection = 173,
			FeyBless = 16543,
			Consolation = 16546,
			EnergyDrain = 167,
			Aetherflow = 166;

		public static class Buffs {
			// public const ushort placeholder = 0;
		}

		public static class Debuffs {
			// public const ushort placeholder = 0;
		}

		public static class Levels {
			public const byte
				Aetherflow = 45,
				Consolation = 80;
		}
	}

	internal class ScholarSwiftcastRaiserFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.ScholarSwiftcastRaiserFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { SCH.Resurrection };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (actionID is SCH.Resurrection && CommonUtil.shouldSwiftcast)
				return CommonSkills.Swiftcast;

			return actionID;
		}
	}

	internal class ScholarSeraphConsolationFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.ScholarSeraphConsolationFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { SCH.FeyBless };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (actionID is SCH.FeyBless && GetJobGauge<SCHGauge>().SeraphTimer > 0)
				return SCH.Consolation;

			return actionID;
		}
	}

	internal class ScholarEnergyDrainFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.ScholarEnergyDrainFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { SCH.EnergyDrain };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (actionID is SCH.EnergyDrain && GetJobGauge<SCHGauge>().Aetherflow == 0)
				return SCH.Aetherflow;

			return actionID;
		}
	}
}
