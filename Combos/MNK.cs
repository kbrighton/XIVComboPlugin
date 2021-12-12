using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboVX.Combos {
	internal static class MNK {
		public const byte JobID = 20;

		public const uint
			Bootshine = 53,
			DragonKick = 74,
			SnapPunch = 56,
			TwinSnakes = 61,
			ArmOfTheDestroyer = 62,
			Demolish = 66,
			Rockbreaker = 70,
			Meditation = 3546,
			FourPointFury = 16473,
			Enlightenment = 16474,
			HowlingFist = 25763;

		public static class Buffs {
			public const ushort
				TwinSnakes = 101,
				OpoOpoForm = 107,
				RaptorForm = 108,
				CoerlForm = 109,
				PerfectBalance = 110,
				LeadenFist = 1861,
				FormlessFist = 2513;
		}

		public static class Debuffs {
			public const ushort
				Demolish = 246;
		}

		public static class Levels {
			public const byte
				Meditation = 15,
				ArmOfTheDestroyer = 26,
				Rockbreaker = 30,
				Demolish = 30,
				FourPointFury = 45,
				HowlingFist = 40,
				DragonKick = 50,
				PerfectBalance = 50,
				FormShift = 52,
				Enlightenment = 70,
				ShadowOfTheDestroyer = 82;
		}
	}

	internal class MnkAoECombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.MnkAoECombo;
		protected internal override uint[] ActionIDs { get; } = new[] { MNK.Rockbreaker, MNK.FourPointFury };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is MNK.Rockbreaker) {

				if ((level >= MNK.Levels.PerfectBalance && SelfHasEffect(MNK.Buffs.PerfectBalance))
					|| (level >= MNK.Levels.FormShift && SelfHasEffect(MNK.Buffs.FormlessFist)))
					return MNK.Rockbreaker;

				if (level >= MNK.Levels.ArmOfTheDestroyer && SelfHasEffect(MNK.Buffs.OpoOpoForm))
					return OriginalHook(MNK.ArmOfTheDestroyer);

				if (level >= MNK.Levels.FourPointFury && SelfHasEffect(MNK.Buffs.RaptorForm))
					return MNK.FourPointFury;

				if (level >= MNK.Levels.Rockbreaker && SelfHasEffect(MNK.Buffs.CoerlForm))
					return MNK.Rockbreaker;

				return MNK.ArmOfTheDestroyer;
			}

			// Currently disabled because Dalamud doesn't seem to have support for MNK's Beast Chakra yet
			// if (actionID is MNK.FourPointFury) {
			// 	MNKGauge gauge = GetJobGauge<MNKGauge>();
			// 
			// 	if (level >= MNK.Levels.ArmOfTheDestroyer && !gauge.BeastChakra.Contains(BeastChakra.OPOOPO))
			// 		// Shadow of the Destroyer
			// 		return OriginalHook(MNK.ArmOfTheDestroyer);
			// 
			// 	if (level >= MNK.Levels.FourPointFury && !gauge.BeastChakra.Contains(BeastChakra.RAPTOR))
			// 		return MNK.FourPointFury;
			// 
			// 	if (level >= MNK.Levels.Rockbreaker && !gauge.BeastChakra.Contains(BeastChakra.COEURL))
			// 		return MNK.Rockbreaker;
			// 
			// 	return MNK.ArmOfTheDestroyer;
			// }

			return actionID;
		}
	}

	internal class MnkBootshineFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.MnkBootshineFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { MNK.DragonKick };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is MNK.DragonKick
				&& (level < MNK.Levels.DragonKick
					|| (SelfHasEffect(MNK.Buffs.LeadenFist) && SelfHasEffect(MNK.Buffs.OpoOpoForm)
						&& (
						SelfHasEffect(MNK.Buffs.FormlessFist) || SelfHasEffect(MNK.Buffs.PerfectBalance)
						)
					)
				)
			)
				return MNK.Bootshine;

			return actionID;
		}
	}

	internal class MonkHowlingFistMeditationFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.MonkHowlingFistMeditationFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { MNK.HowlingFist, MNK.Enlightenment };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is MNK.HowlingFist or MNK.Enlightenment) {
				MNKGauge gauge = GetJobGauge<MNKGauge>();

				unsafe {
					byte chakra = *(byte*)(gauge.Address + 0x8);

					if (level >= MNK.Levels.Meditation && chakra < 5)
						return MNK.Meditation;
				}

				return OriginalHook(MNK.HowlingFist);
			}

			return actionID;
		}
	}
}
