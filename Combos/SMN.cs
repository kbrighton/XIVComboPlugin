using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboVX.Combos {
	internal static class SMN {
		public const byte JobID = 27;

		public const uint
			Resurrection = 173,
			Ruin = 163,
			Ruin2 = 172,
			Ruin3 = 3579,
			Ruin4 = 7426,
			Fester = 181,
			Painflare = 3578,
			DreadwyrmTrance = 3581,
			SummonBahamut = 7427,
			EnkindleBahamut = 7429,
			EnergySyphon = 16510,
			Outburst = 16511,
			EnergyDrain = 16508,
			SummonCarbuncle = 25798,
			RadiantAegis = 25799,
			Aethercharge = 25800,
			SearingLight = 25801,
			AstralFlow = 25822,
			TriDisaster = 25826,
			CrimsonCyclone = 25835,
			MountainBuster = 25836,
			Slipstream = 25837,
			CrimsonStrike = 25885,
			Gemshine = 25883,
			PreciousBrilliance = 25884;

		public static class Buffs {
			public const ushort
				FurtherRuin = 2701,
				IfritsFavor = 2724,
				GarudasFavor = 2725,
				TitansFavor = 2853;
		}

		public static class Debuffs {
			// public const ushort placeholder = 0;
		}

		public static class Levels {
			public const byte
				SummonCarbuncle = 2,
				RadiantAegis = 2,
				Gemshine = 6,
				EnergyDrain = 10,
				PreciousBrilliance = 26,
				Painflare = 40,
				EnergySyphon = 52,
				Ruin3 = 54,
				Ruin4 = 62,
				SearingLight = 66,
				EnkindleBahamut = 70,
				Rekindle = 80,
				ElementalMastery = 86,
				SummonPhoenix = 80;
		}
	}

	internal class SummonerSwiftcastRaiserFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.SummonerSwiftcastRaiserFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { SMN.Resurrection };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (actionID is SMN.Resurrection && ShouldSwiftcast)
				return Common.Swiftcast;

			return actionID;
		}
	}

	internal class SummonerEDFesterCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.SummonerEDFesterCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { SMN.Fester };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (actionID is SMN.Fester && level >= SMN.Levels.EnergyDrain && !GetJobGauge<SMNGauge>().HasAetherflowStacks)
				return SMN.EnergyDrain;

			return actionID;
		}
	}

	internal class SummonerESPainflareCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.SummonerESPainflareCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { SMN.Painflare };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (actionID is SMN.Painflare && level >= SMN.Levels.EnergySyphon && !GetJobGauge<SMNGauge>().HasAetherflowStacks)
				return SMN.EnergySyphon;

			return actionID;
		}
	}

	internal class SummonerRuinFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SmnAny;
		protected internal override uint[] ActionIDs { get; } = new[] { SMN.Ruin, SMN.Ruin2, SMN.Ruin3 };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SMN.Ruin or SMN.Ruin2 or SMN.Ruin3) {
				SMNGauge gauge = GetJobGauge<SMNGauge>();

				if (IsEnabled(CustomComboPreset.SummonerRuinTitansFavorFeature) && level >= SMN.Levels.ElementalMastery && SelfHasEffect(SMN.Buffs.TitansFavor))
					return SMN.MountainBuster;

				if (IsEnabled(CustomComboPreset.SummonerRuinFeature) && level >= SMN.Levels.Gemshine && (gauge.IsIfritAttuned || gauge.IsTitanAttuned || gauge.IsGarudaAttuned))
					return OriginalHook(SMN.Gemshine);

				if (IsEnabled(CustomComboPreset.SummonerFurtherRuinFeature) && level >= SMN.Levels.Ruin4 && gauge.SummonTimerRemaining == 0 && gauge.AttunmentTimerRemaining == 0 && SelfHasEffect(SMN.Buffs.FurtherRuin))
					return SMN.Ruin4;

			}

			return actionID;
		}
	}

	internal class SummonerOutburstFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SmnAny;
		protected internal override uint[] ActionIDs { get; } = new[] { SMN.Outburst, SMN.TriDisaster };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SMN.Outburst or SMN.TriDisaster) {
				SMNGauge gauge = GetJobGauge<SMNGauge>();

				if (IsEnabled(CustomComboPreset.SummonerRuinTitansFavorFeature) && level >= SMN.Levels.ElementalMastery && SelfHasEffect(SMN.Buffs.TitansFavor))
					return SMN.MountainBuster;

				if (IsEnabled(CustomComboPreset.SummonerOutburstFeature) && level >= SMN.Levels.PreciousBrilliance && (gauge.IsIfritAttuned || gauge.IsTitanAttuned || gauge.IsGarudaAttuned))
					return OriginalHook(SMN.PreciousBrilliance);

				if (IsEnabled(CustomComboPreset.SummonerFurtherOutburstFeature) && level >= SMN.Levels.Ruin4 && gauge.SummonTimerRemaining == 0 && gauge.AttunmentTimerRemaining == 0 && SelfHasEffect(SMN.Buffs.FurtherRuin))
					return SMN.Ruin4;

			}

			return actionID;
		}
	}

	internal class SummonerShinyFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SmnAny;

		protected internal override uint[] ActionIDs { get; } = new[] { SMN.Gemshine, SMN.PreciousBrilliance };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SMN.Gemshine or SMN.PreciousBrilliance) {
				SMNGauge gauge = GetJobGauge<SMNGauge>();

				if (IsEnabled(CustomComboPreset.SummonerShinyTitansFavorFeature) && level >= SMN.Levels.ElementalMastery && SelfHasEffect(SMN.Buffs.TitansFavor))
					return SMN.MountainBuster;

				if (IsEnabled(CustomComboPreset.SummonerShinyEnkindleFeature) && level >= SMN.Levels.EnkindleBahamut && !gauge.IsIfritAttuned && !gauge.IsTitanAttuned && !gauge.IsGarudaAttuned && gauge.SummonTimerRemaining > 0)
					return OriginalHook(SMN.EnkindleBahamut);

				if (IsEnabled(CustomComboPreset.SummonerFurtherShinyFeature) && level >= SMN.Levels.Ruin4 && gauge.SummonTimerRemaining == 0 && gauge.AttunmentTimerRemaining == 0 && SelfHasEffect(SMN.Buffs.FurtherRuin))
					return SMN.Ruin4;

			}

			return actionID;
		}
	}

	internal class SummonerDemiFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SummonerDemiEnkindleFeature;

		protected internal override uint[] ActionIDs { get; } = new[] { SMN.Aethercharge, SMN.DreadwyrmTrance, SMN.SummonBahamut };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SMN.Aethercharge or SMN.DreadwyrmTrance or SMN.SummonBahamut) {
				SMNGauge gauge = GetJobGauge<SMNGauge>();

				if (level >= SMN.Levels.EnkindleBahamut && !gauge.IsIfritAttuned && !gauge.IsTitanAttuned && !gauge.IsGarudaAttuned && gauge.SummonTimerRemaining > 0)
					return OriginalHook(SMN.EnkindleBahamut);

			}

			return actionID;
		}
	}

	internal class SummonerRadiantCarbundleFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SummonerRadiantCarbuncleFeature;

		protected internal override uint[] ActionIDs { get; } = new[] { SMN.RadiantAegis };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (actionID is SMN.RadiantAegis && level >= SMN.Levels.SummonCarbuncle && !HasPetPresent() && GetJobGauge<SMNGauge>().Attunement == 0)
				return SMN.SummonCarbuncle;

			return actionID;
		}
	}

	internal class SummonerSearingCarbuncleFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SummonerSearingCarbuncleFeature;

		protected internal override uint[] ActionIDs { get; } = new[] { SMN.SearingLight };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (actionID is SMN.SearingLight && level >= SMN.Levels.SummonCarbuncle && !HasPetPresent() && GetJobGauge<SMNGauge>().Attunement == 0)
				return SMN.SummonCarbuncle;

			return actionID;
		}
	}
}
