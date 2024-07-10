namespace PrincessRTFM.XIVComboVX.Combos;

internal static class SAM {
	public const byte JobID = 34;

	public const uint
		// Single target
		Hakaze = 7477,
		Jinpu = 7478,
		Shifu = 7479,
		Yukikaze = 7480,
		Gekko = 7481,
		Kasha = 7482,
		// AoE
		Fuga = 7483,
		Mangetsu = 7484,
		Oka = 7485,
		Fuko = 25780,
		// Iaijutsu and Tsubame
		Iaijutsu = 7867,
		TsubameGaeshi = 16483,
		KaeshiHiganbana = 16484,
		Shoha = 16487,
		// Misc
		HissatsuShinten = 7490,
		HissatsuKyuten = 7491,
		HissatsuSenei = 16481,
		HissatsuGuren = 7496,
		Ikishoten = 16482,
		Shoha2 = 25779,
		OgiNamikiri = 25781,
		KaeshiNamikiri = 25782;

	public static class Buffs {
		public const ushort
			MeikyoShisui = 1233,
			EyesOpen = 1252,
			Jinpu = 1298,
			Shifu = 1299,
			OgiNamikiriReady = 2959;
	}

	public static class Debuffs {
		// public const ushort placeholder = 0;
	}

	public static class Levels {
		public const byte
			Jinpu = 4,
			Shifu = 18,
			Gekko = 30,
			Mangetsu = 35,
			Kasha = 40,
			Oka = 45,
			Yukikaze = 50,
			MeikyoShisui = 50,
			HissatsuKyuten = 62,
			HissatsuGuren = 70,
			HissatsuSenei = 72,
			TsubameGaeshi = 76,
			Shoha = 80,
			Shoha2 = 82,
			Hyosetsu = 86,
			Fuko = 86,
			KaeshiNamikiri = 90,
			OgiNamikiri = 90,
			Bloodbath = 12;
	}
}

// returning Soon™ (when we have the time to go over everything)

internal class SamuraiBloodbathReplacer: SecondBloodbathCombo {
	public override CustomComboPreset Preset => CustomComboPreset.SamuraiBloodbathReplacer;
}

internal class SamuraiGurenSeneiLevelSyncFeature: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SamuraiGurenSeneiLevelSyncFeature;
	public override uint[] ActionIDs { get; } = [SAM.HissatsuSenei];

	protected override uint Invoke(uint actionID, uint lastComboActionId, float comboTime, byte level)
		=> level >= SAM.Levels.HissatsuSenei ? SAM.HissatsuSenei : SAM.HissatsuGuren;
}

internal class SamuraiYukikazeCombo: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.SamuraiYukikazeCombo;
	public override uint[] ActionIDs { get; } = [SAM.Yukikaze];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if ((level >= SAM.Levels.MeikyoShisui && SelfHasEffect(SAM.Buffs.MeikyoShisui))
			|| (comboTime > 0 && lastComboMove == SAM.Hakaze && level >= SAM.Levels.Yukikaze)
		) {
			return SAM.Yukikaze;
		}

		return SAM.Hakaze;
	}
}

internal class SamuraiGekkoCombo: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.SamuraiGekkoCombo;
	public override uint[] ActionIDs { get; } = [SAM.Gekko];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= SAM.Levels.MeikyoShisui && SelfHasEffect(SAM.Buffs.MeikyoShisui))
			return SAM.Gekko;

		if (comboTime > 0) {

			if (lastComboMove == SAM.Hakaze && level >= SAM.Levels.Jinpu)
				return SAM.Jinpu;

			if (lastComboMove == SAM.Jinpu && level >= SAM.Levels.Gekko)
				return SAM.Gekko;

		}

		return IsEnabled(CustomComboPreset.SamuraiGekkoOption)
			? SAM.Jinpu
			: SAM.Hakaze;
	}
}

internal class SamuraiKashaCombo: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.SamuraiKashaCombo;
	public override uint[] ActionIDs { get; } = [SAM.Kasha];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= SAM.Levels.MeikyoShisui && SelfHasEffect(SAM.Buffs.MeikyoShisui))
			return SAM.Kasha;

		if (comboTime > 0) {
			if (lastComboMove == SAM.Hakaze && level >= SAM.Levels.Shifu)
				return SAM.Shifu;

			if (lastComboMove == SAM.Shifu && level >= SAM.Levels.Kasha)
				return SAM.Kasha;
		}

		return IsEnabled(CustomComboPreset.SamuraiKashaOption)
			? SAM.Shifu
			: SAM.Hakaze;
	}
}

internal class SamuraiMangetsuCombo: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.SamuraiMangetsuCombo;
	public override uint[] ActionIDs { get; } = [SAM.Mangetsu];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (
			(level >= SAM.Levels.MeikyoShisui && SelfHasEffect(SAM.Buffs.MeikyoShisui))
			|| (comboTime > 0 && lastComboMove is SAM.Fuga or SAM.Fuko && level >= SAM.Levels.Mangetsu)
		) {
			return SAM.Mangetsu;
		}

		return OriginalHook(SAM.Fuga);
	}
}

internal class SamuraiOkaCombo: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.SamuraiOkaCombo;
	public override uint[] ActionIDs { get; } = [SAM.Oka];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (
			(level >= SAM.Levels.MeikyoShisui && SelfHasEffect(SAM.Buffs.MeikyoShisui))
			|| (comboTime > 0 && lastComboMove is SAM.Fuga or SAM.Fuko && level >= SAM.Levels.Oka)
		) {
			return SAM.Oka;
		}

		return OriginalHook(SAM.Fuga);
	}
}

/* internal class SamuraiTsubameGaeshiFeature: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.SamAny;
	public override uint[] ActionIDs { get; } = [SAM.TsubameGaeshi];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
		SAMGauge gauge = GetJobGauge<SAMGauge>();

		if (level >= SAM.Levels.Shoha && IsEnabled(CustomComboPreset.SamuraiTsubameGaeshiShohaFeature) && gauge.MeditationStacks >= 3)
			return SAM.Shoha;

		if (IsEnabled(CustomComboPreset.SamuraiTsubameGaeshiIaijutsuFeature)) {
			if (level >= SAM.Levels.TsubameGaeshi && gauge.Sen == Sen.NONE)
				return OriginalHook(SAM.TsubameGaeshi);

			return OriginalHook(SAM.Iaijutsu);
		}

		return actionID;
	}
}

internal class SamuraiIaijutsuFeature: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.SamAny;
	public override uint[] ActionIDs { get; } = [SAM.Iaijutsu];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
		SAMGauge gauge = GetJobGauge<SAMGauge>();

		if (level >= SAM.Levels.Shoha && IsEnabled(CustomComboPreset.SamuraiIaijutsuShohaFeature) && gauge.MeditationStacks >= 3)
			return SAM.Shoha;

		if (IsEnabled(CustomComboPreset.SamuraiIaijutsuTsubameGaeshiFeature)) {
			if (level >= SAM.Levels.TsubameGaeshi && gauge.Sen == Sen.NONE)
				return OriginalHook(SAM.TsubameGaeshi);

			return OriginalHook(SAM.Iaijutsu);
		}

		return actionID;
	}
}

internal class SamuraiShinten: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SamAny;
	public override uint[] ActionIDs { get; } = [SAM.HissatsuShinten];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (IsEnabled(CustomComboPreset.SamuraiShintenShohaFeature) && level >= SAM.Levels.Shoha && GetJobGauge<SAMGauge>().MeditationStacks >= 3)
			return SAM.Shoha;

		if (IsEnabled(CustomComboPreset.SamuraiShintenSeneiFeature)) {

			if (level >= SAM.Levels.HissatsuSenei && IsOffCooldown(SAM.HissatsuSenei)) {
				return SAM.HissatsuSenei;
			}

			if (IsEnabled(CustomComboPreset.SamuraiSeneiGurenFeature)) {
				if (level is >= SAM.Levels.HissatsuGuren and < SAM.Levels.HissatsuSenei && IsOffCooldown(SAM.HissatsuGuren))
					return SAM.HissatsuGuren;
			}

		}

		return actionID;
	}
}

internal class SamuraiSenei: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SamAny;
	public override uint[] ActionIDs { get; } = [SAM.HissatsuSenei];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (IsEnabled(CustomComboPreset.SamuraiSeneiGurenFeature)) {
			if (level is >= SAM.Levels.HissatsuGuren and < SAM.Levels.HissatsuSenei)
				return SAM.HissatsuGuren;
		}

		return actionID;
	}
}

internal class SamuraiKyuten: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SamAny;
	public override uint[] ActionIDs { get; } = [SAM.HissatsuKyuten];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (IsEnabled(CustomComboPreset.SamuraiKyutenShoha2Feature) && level >= SAM.Levels.Shoha2 && GetJobGauge<SAMGauge>().MeditationStacks >= 3)
			return SAM.Shoha2;

		if (IsEnabled(CustomComboPreset.SamuraiKyutenGurenFeature)
			&& level >= SAM.Levels.HissatsuGuren
			&& IsOffCooldown(SAM.HissatsuGuren)
		) {
			return SAM.HissatsuGuren;
		}

		return actionID;
	}
}

internal class SamuraiIkishotenNamikiriFeature: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SamuraiIkishotenNamikiriFeature;

	public override uint[] ActionIDs { get; } = [SAM.Ikishoten];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= SAM.Levels.OgiNamikiri) {
			SAMGauge gauge = GetJobGauge<SAMGauge>();

			if (level >= SAM.Levels.Shoha && gauge.MeditationStacks >= 3)
				return SAM.Shoha;

			if (gauge.Kaeshi == Kaeshi.NAMIKIRI)
				return SAM.KaeshiNamikiri;

			if (SelfHasEffect(SAM.Buffs.OgiNamikiriReady))
				return SAM.OgiNamikiri;
		}

		return actionID;
	}
}
*/
