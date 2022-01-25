/*
 * All credit to daemitus (this was literally edited only enough to compile because I don't play or understand sage (yet?))
 * Original is on dae's repo at https://github.com/daemitus/XIVComboPlugin/blob/master/XIVComboExpanded/Combos/SGE.cs
 * 
 * Someday™ I'll write proper sage combos for VX, but not today. Except the swiftcast-egeiro one.
 */

using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboVX.Combos {
	internal static class SGE {
		public const byte JobID = 40;

		public const uint
			Egeiro = 24287,
			Diagnosis = 24284,
			Kardia = 24285,
			Soteria = 24294,
			Druochole = 24296,
			Kerachole = 24298,
			Ixochole = 24299,
			Taurochole = 24303,
			Holos = 24310,
			Rhizomata = 24309;

		public static class Buffs {
			public const ushort
				Kardion = 2604;
		}

		public static class Debuffs {
			public const ushort
				Placeholder = 0;
		}

		public static class Levels {
			public const ushort
				Dosis = 1,
				Prognosis = 10,
				Druochole = 45,
				Kerachole = 50,
				Taurochole = 62,
				Ixochole = 52,
				Dosis2 = 72,
				Rhizomata = 74,
				Holos = 76,
				Dosis3 = 82,
				Pneuma = 90;
		}
	}

	internal class SageSwiftcastRaiserFeature: SwiftRaiseCombo {
		public override CustomComboPreset Preset => CustomComboPreset.SageSwiftcastRaiserFeature;
		public override uint[] ActionIDs { get; } = new[] { SGE.Egeiro };
	}

	internal class SageSoteria: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.SageSoteriaKardionFeature;
		public override uint[] ActionIDs { get; } = new[] { SGE.Soteria };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (SelfHasEffect(SGE.Buffs.Kardion) && IsOffCooldown(SGE.Soteria))
				return SGE.Soteria;

			return SGE.Kardia;
		}
	}

	internal class SageTaurochole: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.SgeAny;
		public override uint[] ActionIDs { get; } = new[] { SGE.Taurochole };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (IsEnabled(CustomComboPreset.SageTaurocholeRhizomataFeature)) {
				if (level >= SGE.Levels.Rhizomata && GetJobGauge<SGEGauge>().Addersgall == 0)
					return SGE.Rhizomata;
			}

			if (IsEnabled(CustomComboPreset.SageTaurocholeDruocholeFeature)) {
				if (level >= SGE.Levels.Taurochole && IsOffCooldown(SGE.Taurochole))
					return SGE.Taurochole;

				return SGE.Druochole;
			}

			return actionID;
		}
	}

	internal class SageDruochole: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.SageDruocholeRhizomataFeature;
		public override uint[] ActionIDs { get; } = new[] { SGE.Druochole };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= SGE.Levels.Rhizomata && GetJobGauge<SGEGauge>().Addersgall == 0)
				return SGE.Rhizomata;

			return actionID;
		}
	}

	internal class SageIxochole: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.SageIxocholeRhizomataFeature;
		public override uint[] ActionIDs { get; } = new[] { SGE.Ixochole };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= SGE.Levels.Rhizomata && GetJobGauge<SGEGauge>().Addersgall == 0)
				return SGE.Rhizomata;

			return actionID;
		}
	}

	internal class SageKerachole: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.SageKeracholaRhizomataFeature;
		public override uint[] ActionIDs { get; } = new[] { SGE.Kerachole };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= SGE.Levels.Rhizomata && GetJobGauge<SGEGauge>().Addersgall == 0)
				return SGE.Rhizomata;

			return actionID;
		}
	}
}
