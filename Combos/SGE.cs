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

	internal abstract class SageCustomCombo: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SgeAny;
	}

	internal class SageSwiftcastRaiserFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.SageSwiftcastRaiserFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { SGE.Egeiro };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (actionID is SGE.Egeiro && ShouldSwiftcast)
				return Common.Swiftcast;

			return actionID;
		}
	}

	internal class SageSoteria: SageCustomCombo {
		protected internal override uint[] ActionIDs { get; } = new[] { SGE.Soteria };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SGE.Soteria) {
				if (IsEnabled(CustomComboPreset.SageSoteriaKardionFeature)) {

					if (SelfHasEffect(SGE.Buffs.Kardion) && IsOffCooldown(SGE.Soteria))
						return SGE.Soteria;

					return SGE.Kardia;
				}
			}

			return actionID;
		}
	}

	internal class SageTaurochole: SageCustomCombo {
		protected internal override uint[] ActionIDs { get; } = new[] { SGE.Taurochole };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SGE.Taurochole) {
				SGEGauge gauge = GetJobGauge<SGEGauge>();

				if (IsEnabled(CustomComboPreset.SageTaurocholeRhizomataFeature)) {
					if (level >= SGE.Levels.Rhizomata && gauge.Addersgall == 0)
						return SGE.Rhizomata;
				}

				if (IsEnabled(CustomComboPreset.SageTaurocholeDruocholeFeature)) {
					if (level >= SGE.Levels.Taurochole && IsOffCooldown(SGE.Taurochole))
						return SGE.Taurochole;

					return SGE.Druochole;
				}
			}

			return actionID;
		}
	}

	internal class SageDruochole: SageCustomCombo {
		protected internal override uint[] ActionIDs { get; } = new[] { SGE.Druochole };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SGE.Druochole) {
				SGEGauge? gauge = GetJobGauge<SGEGauge>();

				if (IsEnabled(CustomComboPreset.SageDruocholeRhizomataFeature)) {
					if (level >= SGE.Levels.Rhizomata && gauge.Addersgall == 0)
						return SGE.Rhizomata;

				}
			}

			return actionID;
		}
	}

	internal class SageIxochole: SageCustomCombo {
		protected internal override uint[] ActionIDs { get; } = new[] { SGE.Ixochole };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SGE.Ixochole) {
				SGEGauge? gauge = GetJobGauge<SGEGauge>();

				if (IsEnabled(CustomComboPreset.SageIxocholeRhizomataFeature)) {
					if (level >= SGE.Levels.Rhizomata && gauge.Addersgall == 0)
						return SGE.Rhizomata;
				}
			}

			return actionID;
		}
	}

	internal class SageKerachole: SageCustomCombo {
		protected internal override uint[] ActionIDs { get; } = new[] { SGE.Kerachole };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID == SGE.Kerachole) {
				SGEGauge? gauge = GetJobGauge<SGEGauge>();

				if (IsEnabled(CustomComboPreset.SageKeracholaRhizomataFeature)) {
					if (level >= SGE.Levels.Rhizomata && gauge.Addersgall == 0)
						return SGE.Rhizomata;
				}
			}

			return actionID;
		}
	}
}
