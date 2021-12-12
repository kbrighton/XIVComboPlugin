using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboVX.Combos {
	internal static class SMN {
		public const byte JobID = 27;

		public const uint
			Resurrection = 173,
			Ruin = 163,
			Ruin2 = 172,
			Fester = 181,
			Deathflare = 3582,
			Painflare = 3578,
			Ruin3 = 3579,
			Ruin4 = 7426,
			EnkindlePhoenix = 16516,
			EnkindleBahamut = 7429,
			DreadwyrmTrance = 3581,
			EnergySyphon = 16510,
			Outburst = 16511,
			EnergyDrain = 16508,
			SummonPhoenix = 25831,
			SummonBahamut = 7427,
			SummonCarbuncle = 25798,
			RadiantAegis = 25799,
			SearingLight = 25801,
			TriDisaster = 25826,
			Gemshine = 25883,
			PreciousBrilliance = 25884;

		public static class Buffs {
			public const ushort
				FurtherRuin = 2701;
		}

		public static class Debuffs {
			// public const ushort placeholder = 0;
		}

		public static class Levels {
			public const byte
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
				SummonPhoenix = 80;
		}
	}

	internal class SummonerSwiftcastRaiserFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.SummonerSwiftcastRaiserFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { SMN.Resurrection };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (actionID is SMN.Resurrection && CommonUtil.shouldSwiftcast)
				return CommonSkills.Swiftcast;

			return actionID;
		}
	}

	internal class SummonerEDFesterCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.SummonerEDFesterCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { SMN.Fester };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (actionID is SMN.Fester && !GetJobGauge<SMNGauge>().HasAetherflowStacks)
				return SMN.EnergyDrain;

			return actionID;
		}
	}

	internal class SummonerESPainflareCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.SummonerESPainflareCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { SMN.Painflare };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SMN.Painflare) {

				if (level >= SMN.Levels.EnergySyphon && !GetJobGauge<SMNGauge>().HasAetherflowStacks)
					return SMN.EnergySyphon;

				if (level >= SMN.Levels.Painflare)
					return SMN.Painflare;

				return SMN.EnergySyphon;
			}

			return actionID;
		}
	}

	internal class SummonerShinyRuinFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SummonerShinyRuinFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { SMN.Ruin, SMN.Ruin2, SMN.Ruin3 };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SMN.Ruin or SMN.Ruin2 or SMN.Ruin3) {
				SMNGauge gauge = GetJobGauge<SMNGauge>();

				if (level >= SMN.Levels.Gemshine && (gauge.IsIfritAttuned || gauge.IsTitanAttuned || gauge.IsGarudaAttuned))
					return OriginalHook(SMN.Gemshine);

				if (IsEnabled(CustomComboPreset.SummonerShinyRuinFeature)
					&& level >= SMN.Levels.Ruin4
					&& gauge.SummonTimerRemaining == 0
					&& gauge.AttunmentTimerRemaining == 0
					&& SelfHasEffect(SMN.Buffs.FurtherRuin)
				)
						return SMN.Ruin4;
			}

			return actionID;
		}
	}

	internal class SummonerShinyOutburstFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SummonerFurtherOutburstFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { SMN.Outburst, SMN.TriDisaster };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SMN.Outburst or SMN.TriDisaster) {
				SMNGauge gauge = GetJobGauge<SMNGauge>();

				if (level >= SMN.Levels.PreciousBrilliance && (gauge.IsIfritAttuned || gauge.IsTitanAttuned || gauge.IsGarudaAttuned))
					return OriginalHook(SMN.PreciousBrilliance);

				if (IsEnabled(CustomComboPreset.SummonerFurtherOutburstFeature)
					&& level >= SMN.Levels.Ruin4
					&& gauge.SummonTimerRemaining == 0
					&& gauge.AttunmentTimerRemaining == 0
					&& SelfHasEffect(SMN.Buffs.FurtherRuin)
				)
					return SMN.Ruin4;
			}

			return actionID;
		}
	}

	internal class SummonerFurtherRuinFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SummonerFurtherRuinFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { SMN.Ruin, SMN.Ruin2, SMN.Ruin3 };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SMN.Ruin or SMN.Ruin2 or SMN.Ruin3) {
				SMNGauge gauge = GetJobGauge<SMNGauge>();

				if (level >= SMN.Levels.Ruin4 && gauge.SummonTimerRemaining == 0 && gauge.AttunmentTimerRemaining == 0 && SelfHasEffect(SMN.Buffs.FurtherRuin))
					return SMN.Ruin4;

			}

			return actionID;
		}
	}

	internal class SummonerFurtherOutburstFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SummonerFurtherOutburstFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { SMN.Outburst, SMN.TriDisaster };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SMN.Outburst or SMN.TriDisaster) {
				SMNGauge gauge = GetJobGauge<SMNGauge>();

				if (level >= SMN.Levels.Ruin4 && gauge.SummonTimerRemaining == 0 && gauge.AttunmentTimerRemaining == 0 && SelfHasEffect(SMN.Buffs.FurtherRuin))
					return SMN.Ruin4;

			}

			return actionID;
		}
	}

	internal class SummonerDemiFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SummonerDemiFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { SMN.Gemshine, SMN.PreciousBrilliance };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SMN.Gemshine or SMN.PreciousBrilliance) {
				SMNGauge gauge = GetJobGauge<SMNGauge>();

				if (level >= SMN.Levels.EnkindleBahamut && !gauge.IsIfritAttuned && !gauge.IsTitanAttuned && !gauge.IsGarudaAttuned)
					return OriginalHook(SMN.EnkindleBahamut);

			}

			return actionID;
		}
	}
}
