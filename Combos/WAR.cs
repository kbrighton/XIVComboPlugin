using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboVX.Combos {
	internal static class WAR {
		public const byte JobID = 21;

		public const uint
			HeavySwing = 31,
			Maim = 37,
			Overpower = 41,
			StormsPath = 42,
			StormsEye = 45,
			InnerBeast = 49,
			SteelCyclone = 51,
			Infuriate = 52,
			FellCleave = 3549,
			Decimate = 3550,
			RawIntuition = 3551,
			MythrilTempest = 16462,
			ChaoticCyclone = 16463,
			NascentFlash = 16464,
			InnerChaos = 16465,
			PrimalRend = 25753;

		public static class Buffs {
			public const ushort
				InnerRelease = 1177,
				NascentChaos = 1897,
				PrimalRendReady = 2624;
		}

		public static class Debuffs {
			// public const ushort placeholder = 0;
		}

		public static class Levels {
			public const byte
				Maim = 4,
				StormsPath = 26,
				MythrilTempest = 40,
				StormsEye = 50,
				FellCleave = 54,
				Decimate = 60,
				MythrilTempestTrait = 74,
				NascentFlash = 76,
				InnerChaos = 80,
				PrimalRend = 90;
		}
	}

	internal class WarriorStormsPathCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.WarriorStormsPathCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { WAR.StormsPath };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is WAR.StormsPath) {

				if (IsEnabled(CustomComboPreset.WarriorInnerReleaseFeature) && SelfHasEffect(WAR.Buffs.InnerRelease))
					return OriginalHook(WAR.FellCleave);

				return SimpleChainCombo(level, lastComboMove, comboTime, (1, WAR.HeavySwing),
					(WAR.Levels.Maim, WAR.Maim),
					(WAR.Levels.StormsPath, WAR.StormsPath)
				);
			}

			return actionID;
		}
	}

	internal class WarriorStormsEyeCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.WarriorStormsEyeCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { WAR.StormsEye };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is WAR.StormsEye) {

				if (IsEnabled(CustomComboPreset.WarriorInnerReleaseFeature) && SelfHasEffect(WAR.Buffs.InnerRelease))
					return OriginalHook(WAR.FellCleave);

				return SimpleChainCombo(level, lastComboMove, comboTime, (1, WAR.HeavySwing),
					(WAR.Levels.Maim, WAR.Maim),
					(WAR.Levels.StormsEye, WAR.StormsEye)
				);
			}

			return actionID;
		}
	}

	internal class WarriorMythrilTempestCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.WarriorMythrilTempestCombo;

		protected internal override uint[] ActionIDs { get; } = new[] { WAR.MythrilTempest };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is WAR.MythrilTempest) {

				if (IsEnabled(CustomComboPreset.WarriorInnerReleaseFeature) && SelfHasEffect(WAR.Buffs.InnerRelease))
					return OriginalHook(WAR.Decimate);

				if (comboTime > 0 && lastComboMove == WAR.Overpower && level >= WAR.Levels.MythrilTempest) {
					if (IsEnabled(CustomComboPreset.WarriorGaugeOvercapFeature) && level >= WAR.Levels.MythrilTempestTrait && GetJobGauge<WARGauge>().BeastGauge > 80)
						return OriginalHook(WAR.Decimate);

					return WAR.MythrilTempest;
				}

				return WAR.Overpower;
			}

			return actionID;
		}
	}

	internal class WarriorNascentFlashFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.WarriorNascentFlashFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { WAR.NascentFlash };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is WAR.NascentFlash && level < WAR.Levels.NascentFlash)
				return WAR.RawIntuition;

			return actionID;
		}
	}

	internal class WArriorPrimalRendFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.WarriorPrimalRendFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { WAR.InnerBeast, WAR.SteelCyclone };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is WAR.InnerBeast or WAR.SteelCyclone) {

				if (level >= WAR.Levels.PrimalRend && SelfHasEffect(WAR.Buffs.PrimalRendReady))
					return WAR.PrimalRend;

				return OriginalHook(actionID);
			}

			return actionID;
		}
	}
}
