using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboVX.Combos {
	internal static class WAR {
		public const byte JobID = 21;

		public const uint
			HeavySwing = 31,
			Maim = 37,
			Berserk = 38,
			ThrillOfBattle = 40,
			Overpower = 41,
			StormsPath = 42,
			StormsEye = 45,
			InnerBeast = 49,
			SteelCyclone = 51,
			Infuriate = 52,
			FellCleave = 3549,
			Decimate = 3550,
			RawIntuition = 3551,
			Equilibrium = 3552,
			InnerRelease = 7389,
			MythrilTempest = 16462,
			ChaoticCyclone = 16463,
			NascentFlash = 16464,
			InnerChaos = 16465,
			Bloodwhetting = 25751,
			PrimalRend = 25753;

		public static class Buffs {
			public const ushort
				Berserk = 86,
				InnerRelease = 1177,
				NascentChaos = 1897,
				PrimalRendReady = 2624,
				SurgingTempest = 2677;
		}

		public static class Debuffs {
			// public const ushort placeholder = 0;
		}

		public static class Levels {
			public const byte
				Maim = 4,
				Berserk = 6,
				StormsPath = 26,
				ThrillOfBattle = 30,
				InnerBeast = 35,
				MythrilTempest = 40,
				StormsEye = 50,
				Infuriate = 50,
				FellCleave = 54,
				RawIntuition = 56,
				Equilibrium = 58,
				Decimate = 60,
				InnerRelease = 70,
				MythrilTempestTrait = 74,
				NascentFlash = 76,
				InnerChaos = 80,
				Bloodwhetting = 82,
				PrimalRend = 90;
		}
	}

	internal class WarriorStunInterruptFeature: StunInterruptCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.WarriorStunInterruptFeature;
	}

	internal class WarriorStormsPathCombo: CustomCombo {
		public override CustomComboPreset Preset => CustomComboPreset.WarriorStormsPathCombo;
		public override uint[] ActionIDs { get; } = new[] { WAR.StormsPath };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (IsEnabled(CustomComboPreset.WarriorInnerReleaseFeature) && level >= WAR.Levels.InnerRelease && SelfHasEffect(WAR.Buffs.InnerRelease))
				return OriginalHook(WAR.FellCleave);

			if (IsEnabled(CustomComboPreset.WarriorGaugeOvercapPathFeature) && level >= WAR.Levels.InnerRelease && GetJobGauge<WARGauge>().BeastGauge > 70)
				return OriginalHook(WAR.FellCleave);

			if (IsEnabled(CustomComboPreset.WarriorSmartStormCombo) && level >= WAR.Levels.StormsEye && SelfEffectDuration(WAR.Buffs.SurgingTempest) < 7) {
				return SimpleChainCombo(level, lastComboMove, comboTime, (1, WAR.HeavySwing),
					(WAR.Levels.Maim, WAR.Maim),
					(WAR.Levels.StormsEye, WAR.StormsEye)
				);
			}

			return SimpleChainCombo(level, lastComboMove, comboTime, (1, WAR.HeavySwing),
				(WAR.Levels.Maim, WAR.Maim),
				(WAR.Levels.StormsPath, WAR.StormsPath)
			);
		}
	}

	internal class WarriorStormsEyeCombo: CustomCombo {
		public override CustomComboPreset Preset => CustomComboPreset.WarriorStormsEyeCombo;
		public override uint[] ActionIDs { get; } = new[] { WAR.StormsEye };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (IsEnabled(CustomComboPreset.WarriorInnerReleaseFeature) && level >= WAR.Levels.InnerRelease && SelfHasEffect(WAR.Buffs.InnerRelease))
				return OriginalHook(WAR.FellCleave);

			if (IsEnabled(CustomComboPreset.WarriorGaugeOvercapEyeFeature) && level >= WAR.Levels.InnerRelease && GetJobGauge<WARGauge>().BeastGauge > 80)
				return OriginalHook(WAR.FellCleave);

			return SimpleChainCombo(level, lastComboMove, comboTime, (1, WAR.HeavySwing),
				(WAR.Levels.Maim, WAR.Maim),
				(WAR.Levels.StormsEye, WAR.StormsEye)
			);
		}
	}

	internal class WarriorMythrilTempestCombo: CustomCombo {
		public override CustomComboPreset Preset => CustomComboPreset.WarriorMythrilTempestCombo;

		public override uint[] ActionIDs { get; } = new[] { WAR.MythrilTempest };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (IsEnabled(CustomComboPreset.WarriorInnerReleaseFeature) && SelfHasEffect(WAR.Buffs.InnerRelease))
				return OriginalHook(WAR.Decimate);

			if (IsEnabled(CustomComboPreset.WarriorGaugeOvercapTempestFeature) && level >= WAR.Levels.MythrilTempestTrait && GetJobGauge<WARGauge>().BeastGauge > 80)
				return OriginalHook(WAR.Decimate);

			if (comboTime > 0 && lastComboMove == WAR.Overpower && level >= WAR.Levels.MythrilTempest)
				return WAR.MythrilTempest;

			return WAR.Overpower;
		}
	}

	internal class WarriorNascentFlashFeature: CustomCombo {
		public override CustomComboPreset Preset => CustomComboPreset.WarriorNascentFlashFeature;
		public override uint[] ActionIDs { get; } = new[] { WAR.NascentFlash };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level < WAR.Levels.NascentFlash)
				return WAR.RawIntuition;

			return actionID;
		}
	}

	internal class WarriorFellCleaveDecimate: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.WarAny;
		public override uint[] ActionIDs { get; } = new[] { WAR.InnerBeast, WAR.FellCleave, WAR.SteelCyclone, WAR.Decimate };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (IsEnabled(CustomComboPreset.WarriorPrimalBeastFeature)) {
				if (level >= WAR.Levels.PrimalRend && SelfHasEffect(WAR.Buffs.PrimalRendReady))
					return WAR.PrimalRend;
			}

			if (IsEnabled(CustomComboPreset.WarriorInfuriateBeastFeature)) {
				if (level >= WAR.Levels.Infuriate && GetJobGauge<WARGauge>().BeastGauge < 50 && !SelfHasEffect(WAR.Buffs.InnerRelease))
					return WAR.Infuriate;
			}

			return actionID;
		}
	}

	internal class WarriorBerserkInnerRelease: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.WarriorPrimalReleaseFeature;
		public override uint[] ActionIDs { get; } = new[] { WAR.Berserk, WAR.InnerRelease };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= WAR.Levels.PrimalRend && SelfHasEffect(WAR.Buffs.PrimalRendReady))
				return WAR.PrimalRend;

			return actionID;
		}
	}

	internal class WarriorBloodwhetting: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.WarriorHealthyBalancedDietFeature;
		public override uint[] ActionIDs { get; } = new[] { WAR.Bloodwhetting, WAR.RawIntuition };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= WAR.Levels.Bloodwhetting && IsOffCooldown(WAR.Bloodwhetting))
				return WAR.Bloodwhetting;

			if (level >= WAR.Levels.ThrillOfBattle && IsOffCooldown(WAR.ThrillOfBattle))
				return WAR.ThrillOfBattle;

			if (level >= WAR.Levels.Equilibrium && IsOffCooldown(WAR.Equilibrium))
				return WAR.Equilibrium;

			return actionID;
		}
	}

}
