using System;
using System.Linq;

using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboVX.Combos {
	internal static class MNK {
		public const byte ClassID = 2;
		public const byte JobID = 20;

		public const uint
			Bootshine = 53,
			TrueStrike = 54,
			SnapPunch = 56,
			TwinSnakes = 61,
			ArmOfTheDestroyer = 62,
			Demolish = 66,
			PerfectBalance = 69,
			Rockbreaker = 70,
			DragonKick = 74,
			Meditation = 3546,
			RiddleOfFire = 7395,
			Brotherhood = 7396,
			FourPointFury = 16473,
			Enlightenment = 16474,
			HowlingFist = 25763,
			MasterfulBlitz = 25764,
			RiddleOfWind = 25766,
			ShadowOfTheDestroyer = 25767;

		public static class Buffs {
			public const ushort
				OpoOpoForm = 107,
				RaptorForm = 108,
				CoerlForm = 109,
				PerfectBalance = 110,
				LeadenFist = 1861,
				FormlessFist = 2513,
				DisciplinedFist = 3001;
		}

		public static class Debuffs {
			public const ushort
				Demolish = 246;
		}

		public static class Levels {
			public const byte
				TrueStrike = 4,
				SnapPunch = 6,
				Meditation = 15,
				TwinSnakes = 18,
				ArmOfTheDestroyer = 26,
				Rockbreaker = 30,
				Demolish = 30,
				FourPointFury = 45,
				HowlingFist = 40,
				DragonKick = 50,
				PerfectBalance = 50,
				FormShift = 52,
				EnhancedPerfectBalance = 60,
				MasterfulBlitz = 60,
				RiddleOfFire = 68,
				Brotherhood = 70,
				Enlightenment = 70,
				RiddleOfWind = 72,
				ShadowOfTheDestroyer = 82;
		}
	}

	internal class MonkAoECombo: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.MnkAny;

		public override uint[] ActionIDs { get; } = new[] { MNK.MasterfulBlitz };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			MNKGauge gauge = GetJobGauge<MNKGauge>();

			// Blitz
			if (level >= MNK.Levels.MasterfulBlitz && !gauge.BeastChakra.Contains(BeastChakra.NONE))
				return OriginalHook(MNK.MasterfulBlitz);

			if (level >= MNK.Levels.PerfectBalance && SelfHasEffect(MNK.Buffs.PerfectBalance)) {

				// Solar
				if (level >= MNK.Levels.EnhancedPerfectBalance && !gauge.Nadi.HasFlag(Nadi.SOLAR)) {
					if (level >= MNK.Levels.FourPointFury && !gauge.BeastChakra.Contains(BeastChakra.RAPTOR))
						return MNK.FourPointFury;

					if (level >= MNK.Levels.Rockbreaker && !gauge.BeastChakra.Contains(BeastChakra.COEURL))
						return MNK.Rockbreaker;

					if (level >= MNK.Levels.ArmOfTheDestroyer && !gauge.BeastChakra.Contains(BeastChakra.OPOOPO))
						// Shadow of the Destroyer
						return OriginalHook(MNK.ArmOfTheDestroyer);

					return level >= MNK.Levels.ShadowOfTheDestroyer
						? MNK.ShadowOfTheDestroyer
						: MNK.Rockbreaker;
				}

				// Lunar.  Also used if we have both Nadi as Tornado Kick/Phantom Rush isn't picky, or under 60.
				return level >= MNK.Levels.ShadowOfTheDestroyer
					? MNK.ShadowOfTheDestroyer
					: MNK.Rockbreaker;
			}

			// FPF with FormShift
			if (level >= MNK.Levels.FormShift && SelfHasEffect(MNK.Buffs.FormlessFist)) {
				if (level >= MNK.Levels.FourPointFury)
					return MNK.FourPointFury;
			}

			// 1-2-3 combo
			if (level >= MNK.Levels.FourPointFury && SelfHasEffect(MNK.Buffs.RaptorForm))
				return MNK.FourPointFury;

			if (level >= MNK.Levels.ArmOfTheDestroyer && SelfHasEffect(MNK.Buffs.OpoOpoForm))
				// Shadow of the Destroyer
				return OriginalHook(MNK.ArmOfTheDestroyer);

			if (level >= MNK.Levels.Rockbreaker && SelfHasEffect(MNK.Buffs.CoerlForm))
				return MNK.Rockbreaker;

			// Shadow of the Destroyer
			return OriginalHook(MNK.ArmOfTheDestroyer);
		}
	}

	internal class MonkHowlingFistEnlightenment: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.MonkHowlingFistMeditationFeature;
		public override uint[] ActionIDs => new[] { MNK.HowlingFist, MNK.Enlightenment };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			MNKGauge gauge = GetJobGauge<MNKGauge>();

			if (level >= MNK.Levels.Meditation && gauge.Chakra < 5)
				return MNK.Meditation;

			return actionID;
		}
	}

	internal class MonkDragonKick: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.MnkAny;
		public override uint[] ActionIDs => new[] { MNK.DragonKick };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			MNKGauge gauge = GetJobGauge<MNKGauge>();

			if (IsEnabled(CustomComboPreset.MonkDragonKickMeditationFeature)) {
				if (level >= MNK.Levels.Meditation && gauge.Chakra < 5 && !InCombat)
					return MNK.Meditation;
			}

			if (IsEnabled(CustomComboPreset.MonkDragonKickBalanceFeature)) {
				if (level >= MNK.Levels.MasterfulBlitz && !gauge.BeastChakra.Contains(BeastChakra.NONE))
					return OriginalHook(MNK.MasterfulBlitz);
			}

			if (IsEnabled(CustomComboPreset.MonkBootshineFeature)) {
				if (level < MNK.Levels.DragonKick || SelfHasEffect(MNK.Buffs.LeadenFist))
					return MNK.Bootshine;
			}

			return actionID;
		}
	}

	internal class MonkTwinSnakes: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.MnkAny;
		public override uint[] ActionIDs => new[] { MNK.TwinSnakes };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (IsEnabled(CustomComboPreset.MonkTwinSnakesFeature)) {
				if (level < MNK.Levels.TwinSnakes || SelfEffectDuration(MNK.Buffs.DisciplinedFist) > Service.Configuration.MonkTwinSnakesBuffTime)
					return MNK.TrueStrike;
			}

			return actionID;
		}
	}

	internal class MonkDemolish: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.MnkAny;
		public override uint[] ActionIDs => new[] { MNK.Demolish };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (IsEnabled(CustomComboPreset.MonkDemolishFeature)) {
				if (level < MNK.Levels.Demolish || TargetFindOwnEffect(MNK.Debuffs.Demolish)?.RemainingTime > 6.0)
					return MNK.SnapPunch;
			}

			return actionID;
		}
	}

	internal class MonkPerfectBalance: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.MonkPerfectBalanceFeature;
		public override uint[] ActionIDs => new[] { MNK.PerfectBalance };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= MNK.Levels.MasterfulBlitz && !GetJobGauge<MNKGauge>().BeastChakra.Contains(BeastChakra.NONE))
				// Chakra actions
				return OriginalHook(MNK.MasterfulBlitz);

			return actionID;
		}
	}

	internal class MonkRiddleOfFire: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.MnkAny;
		public override uint[] ActionIDs => new[] { MNK.RiddleOfFire };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (IsEnabled(CustomComboPreset.MonkBrotherlyFire)) {
				if (level >= MNK.Levels.Brotherhood && IsOffCooldown(MNK.Brotherhood) && IsOnCooldown(MNK.RiddleOfFire))
					return MNK.Brotherhood;
			}

			if (IsEnabled(CustomComboPreset.MonkFireWind)) {
				if (level >= MNK.Levels.RiddleOfWind && IsOffCooldown(MNK.RiddleOfWind) && IsOnCooldown(MNK.RiddleOfFire))
					return MNK.RiddleOfWind;
			}

			return actionID;
		}
	}

}
