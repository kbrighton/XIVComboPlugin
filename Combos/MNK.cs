using System;
using System.Collections.Generic;
using System.Linq;

using Dalamud.Game.ClientState.JobGauge.Types;

/*
 * All credit to daemitus (this was literally edited only enough to compile because I don't play or understand monk and there's been too many changes)
 * Original is on dae's repo at https://github.com/daemitus/XIVComboPlugin/blob/master/XIVComboExpanded/Combos/MNK.cs
 * 
 * Someday™ I might actually write proper monk combos for VX, but not today.
 */

namespace XIVComboVX.Combos {
	internal static class MNK {
		public const byte ClassID = 2;
		public const byte JobID = 20;

		public const uint
			Bootshine = 53,
			DragonKick = 74,
			TrueStrike = 54,
			SnapPunch = 56,
			TwinSnakes = 61,
			ArmOfTheDestroyer = 62,
			Demolish = 66,
			PerfectBalance = 69,
			Rockbreaker = 70,
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
				TwinSnakes = 101,
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
				MasterfulBlitz = 60,
				RiddleOfFire = 68,
				Enlightenment = 70,
				Brotherhood = 70,
				RiddleOfWind = 72,
				ShadowOfTheDestroyer = 82;
		}
	}

	internal class MonkAoECombo: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.MnkAny;

		public override uint[] ActionIDs { get; } = new[] { MNK.Rockbreaker, MNK.FourPointFury };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID == MNK.Rockbreaker) {
				MyMNKGauge? gauge = new(GetJobGauge<MNKGauge>());

				if (IsEnabled(CustomComboPreset.MonkAoEBalanceFeature)) {
					if (level >= MNK.Levels.MasterfulBlitz && !gauge.BeastChakra.Contains(BeastChakra.NONE))
						return OriginalHook(MNK.MasterfulBlitz);
				}

				if (IsEnabled(CustomComboPreset.MonkAoESolarFeature)) {
					if (level >= MNK.Levels.PerfectBalance && SelfHasEffect(MNK.Buffs.PerfectBalance) && (!gauge.Nadi.HasFlag(Nadi.SOLAR) || gauge.Nadi == (Nadi.LUNAR | Nadi.SOLAR))) {
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
				}

				if (IsEnabled(CustomComboPreset.MonkAoELunarFeature)) {
					if (level >= MNK.Levels.PerfectBalance && SelfHasEffect(MNK.Buffs.PerfectBalance) && !gauge.Nadi.HasFlag(Nadi.LUNAR)) {
						return level >= MNK.Levels.ShadowOfTheDestroyer
							? MNK.ShadowOfTheDestroyer
							: MNK.Rockbreaker;
					}
				}

				if (IsEnabled(CustomComboPreset.MonkAoEDisciplinedFeature)) {
					if (level >= MNK.Levels.FormShift && SelfHasEffect(MNK.Buffs.FormlessFist)) {
						if (level >= MNK.Levels.FourPointFury)
							return MNK.FourPointFury;
					}
				}

				if (IsEnabled(CustomComboPreset.MonkAoECombo)) {
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

			if (actionID == MNK.FourPointFury) {
				if (IsEnabled(CustomComboPreset.MonkAoEFpfFeature)) {
					if (level >= MNK.Levels.PerfectBalance && SelfHasEffect(MNK.Buffs.PerfectBalance)) {
						return level >= MNK.Levels.ShadowOfTheDestroyer
							? MNK.ShadowOfTheDestroyer
							: MNK.Rockbreaker;
					}
				}
			}

			return actionID;
		}
	}

	internal class MonkHowlingFistMeditationFeature: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.MonkHowlingFistMeditationFeature;

		public override uint[] ActionIDs { get; } = new[] { MNK.HowlingFist, MNK.Enlightenment };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			MyMNKGauge? gauge = new(GetJobGauge<MNKGauge>());

			if (level >= MNK.Levels.Meditation && gauge.Chakra < 5)
				return MNK.Meditation;

			// Enlightenment
			return OriginalHook(MNK.HowlingFist);
		}
	}

	internal class MonkPerfectBalanceFeature: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.MonkPerfectBalanceFeature;

		public override uint[] ActionIDs { get; } = new[] { MNK.PerfectBalance };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			MyMNKGauge? gauge = new(GetJobGauge<MNKGauge>());

			if (level >= MNK.Levels.MasterfulBlitz && (!gauge.BeastChakra.Contains(BeastChakra.NONE) || SelfHasEffect(MNK.Buffs.PerfectBalance)))
				return OriginalHook(MNK.MasterfulBlitz);

			return actionID;
		}
	}

	internal class MonkDragonKickFeature: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.MnkAny;
		public override uint[] ActionIDs { get; } = new[] { MNK.DragonKick };

		protected override uint Invoke(uint actionID, uint lastComboActionId, float comboTime, byte level) {
			MyMNKGauge? gauge = new(GetJobGauge<MNKGauge>());

			if (IsEnabled(CustomComboPreset.MonkDragonBlitzFeature) && level >= MNK.Levels.MasterfulBlitz && !gauge.BeastChakra.Contains(BeastChakra.NONE))
				return MNK.MasterfulBlitz;

			if (IsEnabled(CustomComboPreset.MonkDragonBootshineFeature) && (level < MNK.Levels.DragonKick || SelfHasEffect(MNK.Buffs.LeadenFist)))
				return MNK.Bootshine;

			return actionID;
		}
	}

	internal class MonkTwinStrikeFeature: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.MonkTwinStrikeFeature;
		public override uint[] ActionIDs { get; } = new[] { MNK.TwinSnakes };

		protected override uint Invoke(uint actionID, uint lastComboActionId, float comboTime, byte level) {

			return level < MNK.Levels.TwinSnakes || SelfFindEffect(MNK.Buffs.DisciplinedFist)?.RemainingTime > 5
				? MNK.TrueStrike
				: actionID;

		}
	}

	internal class MonkDemolishSnapFeature: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.MonkDemolishSnapFeature;
		public override uint[] ActionIDs { get; } = new[] { MNK.Demolish };

		protected override uint Invoke(uint actionID, uint lastComboActionId, float comboTime, byte level) {

			return level < MNK.Levels.Demolish || TargetFindOwnEffect(MNK.Debuffs.Demolish)?.RemainingTime > 5
				? MNK.SnapPunch
				: actionID;

		}
	}

	internal class MonkRiddleOfFireFeature: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.MnkAny;
		public override uint[] ActionIDs { get; } = new[] { MNK.RiddleOfFire };

		protected override uint Invoke(uint actionID, uint lastComboActionId, float comboTime, byte level) {

			List<uint> options = new();

			if (IsEnabled(CustomComboPreset.MonkFireWindFeature) && level > MNK.Levels.RiddleOfWind)
				options.Add(MNK.RiddleOfWind);

			if (IsEnabled(CustomComboPreset.MonkBrotherlyFireFeature) && level >= MNK.Levels.Brotherhood)
				options.Add(MNK.Brotherhood);

			options.Add(MNK.RiddleOfFire);

			return options.Count == 1
				? options[0]
				: PickByCooldown(actionID, options.ToArray());
		}
	}

	internal unsafe class MyMNKGauge {
		private readonly IntPtr address;

		internal MyMNKGauge(MNKGauge gauge) {
			this.address = gauge.Address;
		}

		public byte Chakra => *(byte*)(this.address + 0x8);

		public BeastChakra[] BeastChakra => new[]
		{
			*(BeastChakra*)(this.address + 0x9),
			*(BeastChakra*)(this.address + 0xA),
			*(BeastChakra*)(this.address + 0xB),
		};

		public Nadi Nadi => *(Nadi*)(this.address + 0xC);

		public ushort BlitzTimeRemaining => *(ushort*)(this.address + 0xE);
	}

	internal enum BeastChakra: byte {
		NONE = 0,
		COEURL = 1,
		OPOOPO = 2,
		RAPTOR = 3,
	}

	[Flags]
	internal enum Nadi: byte {
		NONE = 0,
		LUNAR = 2,
		SOLAR = 4,
	}
}