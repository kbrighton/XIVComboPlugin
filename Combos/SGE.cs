namespace XIVComboVX.Combos;

using Dalamud.Game.ClientState.JobGauge.Types;

using XIVComboVX;

internal static class SGE {
	public const byte JobID = 40;

	public const uint
		Dosis = 24283,
		Diagnosis = 24284,
		Kardia = 24285,
		Prognosis = 24286,
		Egeiro = 24287,
		Physis = 24288,
		Phlegma = 24289,
		Eukrasia = 24290,
		Soteria = 24294,
		Druochole = 24296,
		Dyskrasia = 24297,
		Kerachole = 24298,
		Ixochole = 24299,
		Zoe = 24300,
		Pepsis = 24301,
		Physis2 = 24302,
		Taurochole = 24303,
		Toxikon = 24304,
		Haima = 24305,
		Phlegma2 = 24307,
		Rhizomata = 24309,
		Holos = 24310,
		Panhaima = 24311,
		Phlegma3 = 24313,
		Krasis = 24317,
		Pneuma = 24318;

	public static class Buffs {
		public const ushort
			Kardion = 2604;
	}

	public static class Debuffs {
		public const ushort
			Placeholder = 0;
	}

	public static class Levels {
		public const ushort
			Dosis = 1,
			Prognosis = 10,
			Phlegma = 26,
			Soteria = 35,
			Druochole = 45,
			Kerachole = 50,
			Ixochole = 52,
			Physis2 = 60,
			Taurochole = 62,
			Haima = 70,
			Phlegma2 = 72,
			Dosis2 = 72,
			Rhizomata = 74,
			Holos = 76,
			Panhaima = 80,
			Phlegma3 = 82,
			Dosis3 = 82,
			Krasis = 86,
			Pneuma = 90;
	}
}

internal class SageSoteria: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SageSoteriaKardionFeature;
	public override uint[] ActionIDs => new[] { SGE.Soteria };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (SelfHasEffect(SGE.Buffs.Kardion) && IsOffCooldown(SGE.Soteria))
			return SGE.Soteria;

		return SGE.Kardia;
	}
}

internal class SageTaurochole: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SgeAny;
	public override uint[] ActionIDs => new[] { SGE.Taurochole };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (IsEnabled(CustomComboPreset.SageTaurocholeRhizomataFeature)) {
			if (level >= SGE.Levels.Rhizomata && GetJobGauge<SGEGauge>().Addersgall == 0)
				return SGE.Rhizomata;
		}

		if (IsEnabled(CustomComboPreset.SageTaurocholeDruocholeFeature)) {
			if (level >= SGE.Levels.Taurochole && IsOffCooldown(SGE.Taurochole))
				return SGE.Taurochole;

			return SGE.Druochole;
		}

		return actionID;
	}
}

internal class SageDruochole: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SageDruocholeRhizomataFeature;
	public override uint[] ActionIDs => new[] { SGE.Druochole };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= SGE.Levels.Rhizomata && GetJobGauge<SGEGauge>().Addersgall == 0)
			return SGE.Rhizomata;

		return actionID;
	}
}

internal class SageIxochole: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SageIxocholeRhizomataFeature;
	public override uint[] ActionIDs => new[] { SGE.Ixochole };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= SGE.Levels.Rhizomata && GetJobGauge<SGEGauge>().Addersgall == 0)
			return SGE.Rhizomata;

		return actionID;
	}
}

internal class SageKerachole: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SageKeracholaRhizomataFeature;
	public override uint[] ActionIDs => new[] { SGE.Kerachole };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= SGE.Levels.Rhizomata && GetJobGauge<SGEGauge>().Addersgall == 0)
			return SGE.Rhizomata;

		return actionID;
	}
}

internal class SagePhlegma: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SgeAny;
	public override uint[] ActionIDs => new[] { SGE.Phlegma, SGE.Phlegma2, SGE.Phlegma3 };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (IsEnabled(CustomComboPreset.SagePhlegmaDyskrasia)) {
			if (!HasTarget)
				return OriginalHook(SGE.Dyskrasia);
		}

		if (IsEnabled(CustomComboPreset.SagePhlegmaToxicon)) {
			uint phlegma = level >= SGE.Levels.Phlegma3
				? SGE.Phlegma3
				: level >= SGE.Levels.Phlegma2
					? SGE.Phlegma2
					: level >= SGE.Levels.Phlegma
						? SGE.Phlegma
						: 0;

			if (phlegma != 0 && GetCooldown(phlegma).CooldownRemaining > 45 && GetJobGauge<SGEGauge>().Addersting > 0)
				return OriginalHook(SGE.Toxikon);
		}

		if (IsEnabled(CustomComboPreset.SagePhlegmaDyskrasia)) {
			uint phlegma = level >= SGE.Levels.Phlegma3
				? SGE.Phlegma3
				: level >= SGE.Levels.Phlegma2
					? SGE.Phlegma2
					: level >= SGE.Levels.Phlegma
						? SGE.Phlegma
						: 0;

			if (phlegma != 0 && GetCooldown(phlegma).CooldownRemaining > 45)
				return OriginalHook(SGE.Dyskrasia);
		}

		return actionID;
	}
}
