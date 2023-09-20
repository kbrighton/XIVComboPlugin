namespace PrincessRTFM.XIVComboVX.Combos;

internal static class Common {
	public const uint
		// everyone
		Sprint = 4,
		// ranged DPS
		Peloton = 7557,
		// tanks
		LowBlow = 7540,
		Interject = 7538,
		// mages
		Swiftcast = 7561,
		// healers
		LucidDreaming = 7562;

	internal static class Buffs {
		public const ushort
			Swiftcast1 = 167,
			Swiftcast2 = 1325,
			Swiftcast3 = 1987,
			LostChainspell = 2560;
	}

	internal static class Levels {
		public const uint
			Peloton = 20,
			LucidDreaming = 14,
			Swiftcast = 18;
	}

	internal static bool checkLucidWeave(CustomComboPreset preset, byte level, uint manaThreshold, uint baseAction) {

		if (CustomCombo.IsEnabled(preset)) {
			if (level >= Levels.LucidDreaming) {
				if (CustomCombo.LocalPlayer.CurrentMp < manaThreshold) {
					if (CustomCombo.CanWeave(baseAction)) {
						if (CustomCombo.CanUse(Common.LucidDreaming))
							return true;
					}
				}
			}
		}

		return false;
	}
}

#if DEBUG
internal class PelotonSprintCombo: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.CommonSmartPelotonSprint;
	public override uint[] ActionIDs => new uint[] { Common.Sprint, Common.Peloton };
	protected override uint Invoke(uint actionID, uint lastComboActionId, float comboTime, byte level) {

		if (level < Common.Levels.Peloton) // peloton is not unlocked from level 1
			return Common.Sprint;
		if (InCombat) // you can't use peloton in combat
			return Common.Sprint;
		if (LocalPlayer.ClassJob.Id is not (BRD.JobID or (BRD.JobID - 18) or MCH.JobID or DNC.JobID)) // peloton is only available to ranged DPS
			return Common.Sprint;

		if (IsEnabled(CustomComboPreset.CommonSmartPelotonSprintPrioritySprint) && IsOffCooldown(Common.Sprint))
			return Common.Sprint;

		return Common.Peloton;
	}
}
#endif

internal abstract class SwiftRaiseCombo: CustomCombo {
	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
		return level >= Common.Levels.Swiftcast && ShouldSwiftcast
			? Common.Swiftcast
			: actionID;
	}
}

internal abstract class StunInterruptCombo: CustomCombo {
	public override uint[] ActionIDs { get; } = new[] { Common.LowBlow, Common.Interject };

	protected override uint Invoke(uint actionID, uint lastComboActionId, float comboTime, byte level) {
		return CanInterrupt && IsOffCooldown(Common.Interject)
			? Common.Interject
			: Common.LowBlow;
	}
}
