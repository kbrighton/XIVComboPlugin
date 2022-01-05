using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboVX.Combos {
	internal static class DRK {
		public const byte JobID = 32;

		public const uint
			HardSlash = 3617,
			Unleash = 3621,
			SyphonStrike = 3623,
			Souleater = 3632,
			SaltedEarth = 3639,
			AbyssalDrain = 3641,
			CarveAndSpit = 3643,
			Quietus = 7391,
			Bloodspiller = 7392,
			FloodOfDarkness = 16466,
			EdgeOfDarkness = 16467,
			StalwartSoul = 16468,
			FloodOfShadow = 16469,
			EdgeOfShadow = 16470,
			SaltAndDarkness = 25755,
			Shadowbringer = 25757;

		public static class Buffs {
			public const ushort
				BloodWeapon = 742,
				Darkside = 751,
				Delirium = 1972;
		}
		public static class Debuffs {
			// public const ushort placeholder = 0;
		}

		public static class Levels {
			public const byte
				SyphonStrike = 2,
				Souleater = 26,
				FloodOfDarkness = 30,
				EdgeOfDarkness = 40,
				SaltedEarth = 52,
				AbyssalDrain = 56,
				CarveAndSpit = 60,
				Bloodpiller = 62,
				Quietus = 64,
				Delirium = 68,
				StalwartSoul = 72,
				Shadow = 74,
				SaltAndDarkness = 86,
				Shadowbringer = 90;
		}
	}

	internal class DarkStunInterruptFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.DarkStunInterruptFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { Common.LowBlow, Common.Interject };

		protected override uint Invoke(uint actionID, uint lastComboActionId, float comboTime, byte level) {
			return CanInterrupt && IsOffCooldown(Common.Interject)
				? Common.Interject
				: Common.LowBlow;
		}
	}

	internal class DarkSouleaterCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.DrkAny;
		protected internal override uint[] ActionIDs { get; } = new[] { DRK.Souleater };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is DRK.Souleater) {

				if (IsEnabled(CustomComboPreset.DarkOvercapFeature)) {
					DRKGauge gauge = GetJobGauge<DRKGauge>();
					if (gauge.Blood > 80 || (gauge.Blood > 70 && SelfHasEffect(DRK.Buffs.BloodWeapon)))
						return DRK.Bloodspiller;
				}

				if (level >= DRK.Levels.Delirium && IsEnabled(CustomComboPreset.DarkDeliriumFeature) && SelfHasEffect(DRK.Buffs.Delirium))
					return DRK.Bloodspiller;

				if (IsEnabled(CustomComboPreset.DarkSouleaterCombo))
					return SimpleChainCombo(level, lastComboMove, comboTime, (1, DRK.HardSlash),
						(DRK.Levels.SyphonStrike, DRK.SyphonStrike),
						(DRK.Levels.Souleater, DRK.Souleater)
					);
			}

			return actionID;
		}
	}

	internal class DarkAoECombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.DrkAny;
		protected internal override uint[] ActionIDs { get; } = new[] { DRK.StalwartSoul };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is DRK.StalwartSoul) {

				if (IsEnabled(CustomComboPreset.DarkOvercapFeature)) {
					DRKGauge gauge = GetJobGauge<DRKGauge>();
					if (gauge.Blood > 80 || (gauge.Blood > 70 && SelfHasEffect(DRK.Buffs.BloodWeapon)))
						return DRK.Quietus;
				}

				if (level >= DRK.Levels.Delirium && IsEnabled(CustomComboPreset.DarkDeliriumFeature) && SelfHasEffect(DRK.Buffs.Delirium))
					return DRK.Quietus;

				if (level >= DRK.Levels.StalwartSoul && comboTime > 0 && lastComboMove == DRK.Unleash)
					return DRK.StalwartSoul;

				return DRK.Unleash;
			}

			return actionID;
		}
	}

	internal class DarkShadowbringerFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.DarkShadowbringerFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { DRK.EdgeOfShadow, DRK.FloodOfShadow };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= DRK.Levels.Shadowbringer && SelfHasEffect(DRK.Buffs.Darkside) && LocalPlayer.CurrentMp < 6000)
				return DRK.Shadowbringer;

			return actionID;
		}
	}
}
