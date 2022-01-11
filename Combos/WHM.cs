using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboVX.Combos {
	internal static class WHM {
		public const byte JobID = 24;

		public const uint
			Raise = 125,
			Cure = 120,
			Medica = 124,
			Cure2 = 135,
			AfflatusSolace = 16531,
			AfflatusRapture = 16534,
			AfflatusMisery = 16535;

		public static class Buffs {
			// public const ushort placeholder = 0;
		}

		public static class Debuffs {
			// public const ushort placeholder = 0;
		}

		public static class Levels {
			public const byte
				Cure2 = 30,
				AfflatusSolace = 52,
				AfflatusMisery = 74,
				AfflatusRapture = 76;
		}
	}

	internal class WhiteMageSwiftcastRaiserFeature: SwiftRaiseCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.WhiteMageSwiftcastRaiserFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { WHM.Raise };
	}

	internal class WhiteMageSolaceMiseryFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.WhiteMageSolaceMiseryFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { WHM.AfflatusSolace };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= WHM.Levels.AfflatusMisery && GetJobGauge<WHMGauge>().BloodLily == 3)
				return WHM.AfflatusMisery;

			return actionID;
		}
	}

	internal class WhiteMageRaptureMiseryFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.WhiteMageRaptureMiseryFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { WHM.AfflatusRapture };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= WHM.Levels.AfflatusMisery && GetJobGauge<WHMGauge>().BloodLily == 3)
				return WHM.AfflatusMisery;

			return actionID;
		}
	}

	internal class WhiteMageCureFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.WhiteMageCureFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { WHM.Cure2 };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level < WHM.Levels.Cure2)
				return WHM.Cure;

			return actionID;
		}
	}

	internal class WhiteMageAfflatusFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.WhmAny;
		protected internal override uint[] ActionIDs { get; } = new[] { WHM.Cure2, WHM.Medica };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (actionID is WHM.Cure2) {
				WHMGauge gauge = GetJobGauge<WHMGauge>();

				if (IsEnabled(CustomComboPreset.WhiteMageSolaceMiseryFeature) && level >= WHM.Levels.AfflatusMisery && gauge.BloodLily == 3)
					return WHM.AfflatusMisery;

				if (IsEnabled(CustomComboPreset.WhiteMageAfflatusFeature) && level >= WHM.Levels.AfflatusSolace && gauge.Lily > 0)
					return WHM.AfflatusSolace;

			}
			else if (actionID is WHM.Medica) {
				WHMGauge gauge = GetJobGauge<WHMGauge>();

				if (IsEnabled(CustomComboPreset.WhiteMageRaptureMiseryFeature) && level >= WHM.Levels.AfflatusMisery && gauge.BloodLily == 3)
					return WHM.AfflatusMisery;

				if (IsEnabled(CustomComboPreset.WhiteMageAfflatusFeature) && level >= WHM.Levels.AfflatusRapture && gauge.Lily > 0)
					return WHM.AfflatusRapture;

			}

			return actionID;
		}
	}
}
