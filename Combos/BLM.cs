using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboVX.Combos {
	internal static class BLM {
		public const byte JobID = 25;

		public const uint
			Transpose = 149,
			Fire = 141,
			Fire2 = 147,
			Fire3 = 152,
			Fire4 = 3577,
			HighFire2 = 25794,
			Flare = 162,
			Blizzard = 142,
			Blizzard2 = 25793,
			Blizzard3 = 154,
			Blizzard4 = 3576,
			HighBlizzard2 = 25795,
			Freeze = 159,
			Thunder = 144,
			Thunder3 = 153,
			Scathe = 156,
			LeyLines = 3573,
			BetweenTheLines = 7419,
			Despair = 16505,
			UmbralSoul = 16506,
			Xenoglossy = 16507,
			Paradox = 25797;

		public static class Buffs {
			public const ushort
				Thundercloud = 164,
				LeyLines = 737,
				Firestarter = 165;
		}

		public static class Debuffs {
			public const ushort
				Thunder = 161,
				Thunder3 = 163;
		}

		public static class Levels {
			public const byte
				Fire3 = 35,
				Blizzard3 = 35,
				Freeze = 40,
				Thunder3 = 45,
				Flare = 50,
				Blizzard4 = 58,
				Fire4 = 60,
				BetweenTheLines = 62,
				Despair = 72,
				UmbralSoul = 76,
				Xenoglossy = 80,
				HighFire2 = 82,
				HighBlizzard2 = 82,
				Paradox = 90;
		}
	}

	internal class BlackEnochianFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackEnochianFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { BLM.Fire4, BLM.Blizzard4 };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is BLM.Fire4 or BLM.Blizzard4) {
				BLMGauge gauge = GetJobGauge<BLMGauge>();

				if (level >= BLM.Levels.Blizzard4 && gauge.InUmbralIce)
					return BLM.Blizzard4;

				if (level >= BLM.Levels.Fire4 && gauge.InAstralFire)
					return BLM.Fire4;

			}

			return actionID;
		}
	}

	internal class BlackManaFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackManaFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { BLM.Transpose };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is BLM.Transpose) {
				BLMGauge gauge = GetJobGauge<BLMGauge>();

				if (level >= BLM.Levels.UmbralSoul && gauge.IsEnochianActive && gauge.InUmbralIce)
					return BLM.UmbralSoul;

			}

			return actionID;
		}
	}

	internal class BlackLeyLinesFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackLeyLinesFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { BLM.LeyLines };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (actionID is BLM.LeyLines && level >= BLM.Levels.BetweenTheLines && SelfHasEffect(BLM.Buffs.LeyLines))
				return BLM.BetweenTheLines;

			return actionID;
		}
	}


	internal class BlackFireFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackFireAstralFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { BLM.Fire };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is BLM.Fire) {

				if (level >= BLM.Levels.Fire3
					&& (
						(IsEnabled(CustomComboPreset.BlackFireAstralFeature) && !GetJobGauge<BLMGauge>().InAstralFire)
						|| (IsEnabled(CustomComboPreset.BlackFireProcFeature) && SelfHasEffect(BLM.Buffs.Firestarter))
					)
				)
					return BLM.Fire3;

				return OriginalHook(BLM.Fire);
			}

			return actionID;
		}
	}

	internal class BlackBlizzardFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackBlizzardFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { BLM.Blizzard };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is BLM.Blizzard) {

				if (level >= BLM.Levels.Blizzard3 && !GetJobGauge<BLMGauge>().InUmbralIce)
					return BLM.Blizzard3;

				return OriginalHook(BLM.Blizzard);
			}

			return actionID;
		}
	}

	internal class BlackFreezeFlareFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackFreezeFlareFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { BLM.Freeze, BLM.Flare };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is BLM.Freeze or BLM.Flare) {
				BLMGauge gauge = GetJobGauge<BLMGauge>();

				if (level >= BLM.Levels.Freeze && gauge.InUmbralIce)
					return BLM.Freeze;

				if (level >= BLM.Levels.Flare && gauge.InAstralFire)
					return BLM.Flare;

			}

			return actionID;
		}
	}

	internal class BlackFire2Feature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackFire2Feature;
		protected internal override uint[] ActionIDs { get; } = new[] { BLM.Fire2, BLM.HighFire2 };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is BLM.Fire or BLM.HighFire2) {
				BLMGauge gauge = GetJobGauge<BLMGauge>();

				if (level >= BLM.Levels.Flare && gauge.InAstralFire && gauge.UmbralHearts <= 1)
					return BLM.Flare;

			}

			return actionID;
		}
	}

	internal class BlackBlizzard2Feature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackBlizzard2Feature;
		protected internal override uint[] ActionIDs { get; } = new[] { BLM.Blizzard2, BLM.HighBlizzard2 };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is BLM.Blizzard2 or BLM.HighBlizzard2) {

				if (level >= BLM.Levels.Freeze && GetJobGauge<BLMGauge>().InUmbralIce)
					return BLM.Freeze;

			}

			return actionID;
		}
	}

	internal class BlackScatheFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.BlackScatheFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { BLM.Scathe };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is BLM.Scathe && level >= BLM.Levels.Xenoglossy && GetJobGauge<BLMGauge>().PolyglotStacks > 0)
				return BLM.Xenoglossy;

			return actionID;
		}
	}
}
