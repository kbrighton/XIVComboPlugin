using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboVX.Combos {
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
            HissatsuKyuten = 7491,
			MeikyoShisui = 7499,
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
				HissatsuKyuten = 64,
				TsubameGaeshi = 76,
				Shoha = 80,
				Shoha2 = 82,
				Hyosetsu = 86,
				Fuko = 86,
				KaeshiNamikiri = 90,
				OgiNamikiri = 90;
		}
	}

	internal class SamuraiYukikazeCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.SamuraiYukikazeCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { SAM.Yukikaze };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SAM.Yukikaze) {

				if ((level >= SAM.Levels.MeikyoShisui && SelfHasEffect(SAM.Buffs.MeikyoShisui))
					|| (comboTime > 0 && lastComboMove == SAM.Hakaze && level >= SAM.Levels.Yukikaze)
				)
					return SAM.Yukikaze;

				return SAM.Hakaze;
			}

			return actionID;
		}
	}

	internal class SamuraiGekkoCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.SamuraiGekkoCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { SAM.Gekko };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SAM.Gekko) {

				if (level >= SAM.Levels.MeikyoShisui && SelfHasEffect(SAM.Buffs.MeikyoShisui))
					return SAM.Gekko;

				if (comboTime > 0) {
					if (lastComboMove == SAM.Hakaze && level >= SAM.Levels.Jinpu)
						return SAM.Jinpu;

					if (lastComboMove == SAM.Jinpu && level >= SAM.Levels.Gekko)
						return SAM.Gekko;
				}

				return SAM.Hakaze;
			}

			return actionID;
		}
	}

	internal class SamuraiKashaCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.SamuraiKashaCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { SAM.Kasha };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SAM.Kasha) {

				if (level >= SAM.Levels.MeikyoShisui && SelfHasEffect(SAM.Buffs.MeikyoShisui))
					return SAM.Kasha;

				if (comboTime > 0) {
					if (lastComboMove == SAM.Hakaze && level >= SAM.Levels.Shifu)
						return SAM.Shifu;

					if (lastComboMove == SAM.Shifu && level >= SAM.Levels.Kasha)
						return SAM.Kasha;
				}

				return SAM.Hakaze;
			}

			return actionID;
		}
	}

	internal class SamuraiMangetsuCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.SamuraiMangetsuCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { SAM.Mangetsu };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SAM.Mangetsu) {

				if (level >= SAM.Levels.MeikyoShisui && SelfHasEffect(SAM.Buffs.MeikyoShisui))
					return SAM.Mangetsu;

				if (comboTime > 0 && lastComboMove is SAM.Fuga or SAM.Fuko && level >= SAM.Levels.Mangetsu)
					return SAM.Mangetsu;

				return OriginalHook(SAM.Fuga);
			}

			return actionID;
		}
	}

	internal class SamuraiOkaCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.SamuraiOkaCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { SAM.Oka };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SAM.Oka) {

				if (level >= SAM.Levels.MeikyoShisui && SelfHasEffect(SAM.Buffs.MeikyoShisui))
					return SAM.Oka;

				if (comboTime > 0 && lastComboMove is SAM.Fuga or SAM.Fuko && level >= SAM.Levels.Oka)
					return SAM.Oka;

				return OriginalHook(SAM.Fuga);
			}

			return actionID;
		}
	}

	internal class SamuraiJinpuShifuFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.SamuraiJinpuShifuFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { SAM.MeikyoShisui };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SAM.MeikyoShisui) {

				if (SelfHasEffect(SAM.Buffs.MeikyoShisui)) {

					if (!SelfHasEffect(SAM.Buffs.Jinpu))
						return SAM.Jinpu;

					if (!SelfHasEffect(SAM.Buffs.Shifu))
						return SAM.Shifu;

				}

				return SAM.MeikyoShisui;
			}

			return actionID;
		}
	}

	internal class SamuraiTsubameGaeshiShohaFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.SamuraiTsubameGaeshiShohaFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { SAM.TsubameGaeshi };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SAM.TsubameGaeshi) {

				if (level >= SAM.Levels.Shoha && GetJobGauge<SAMGauge>().MeditationStacks >= 3)
					return SAM.Shoha;

			}

			return actionID;
		}
	}

	internal class SamuraiIaijutsuShohaFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.SamuraiIaijutsuShohaFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { SAM.Iaijutsu };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SAM.Iaijutsu) {

				if (level >= SAM.Levels.Shoha && GetJobGauge<SAMGauge>().MeditationStacks >= 3)
					return SAM.Shoha;

			}

			return actionID;
		}
	}

	internal class SamuraiTsubameGaeshiIaijutsuFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.SamuraiTsubameGaeshiIaijutsuFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { SAM.TsubameGaeshi };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SAM.TsubameGaeshi) {

				if (level >= SAM.Levels.TsubameGaeshi && GetJobGauge<SAMGauge>().Sen == Sen.NONE)
					return OriginalHook(SAM.TsubameGaeshi);

				return OriginalHook(SAM.Iaijutsu);
			}

			return actionID;
		}
	}

	internal class SamuraiIaijutsuTsubameGaeshiFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.SamuraiIaijutsuTsubameGaeshiFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { SAM.Iaijutsu };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SAM.Iaijutsu) {

				if (level >= SAM.Levels.TsubameGaeshi && GetJobGauge<SAMGauge>().Sen == Sen.NONE)
					return OriginalHook(SAM.TsubameGaeshi);

				return OriginalHook(SAM.Iaijutsu);
			}

			return actionID;
		}
	}

	internal class SamuraiShoha2Feature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SamuraiShoha2Feature;
		protected internal override uint[] ActionIDs { get; } = new[] { SAM.HissatsuKyuten };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (actionID is SAM.HissatsuKyuten && level >= SAM.Levels.Shoha2 && GetJobGauge<SAMGauge>().MeditationStacks >= 3)
				return SAM.Shoha2;

			return actionID;
		}
	}

	internal class SamuraiIkishotenNamikiriFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SamuraiIkishotenNamikiriFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { SAM.Ikishoten };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is SAM.Ikishoten) {
				SAMGauge gauge = GetJobGauge<SAMGauge>();

				if (level >= SAM.Levels.Shoha && gauge.MeditationStacks >= 3)
					return SAM.Shoha;

				if (level >= SAM.Levels.KaeshiNamikiri && gauge.Kaeshi == Kaeshi.NAMIKIRI)
					return SAM.KaeshiNamikiri;

				if (level >= SAM.Levels.OgiNamikiri && SelfHasEffect(SAM.Buffs.OgiNamikiriReady))
					return SAM.OgiNamikiri;

			}

			return actionID;
		}
	}
}
