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

	internal static bool CheckLucidWeave(CustomComboPreset preset, byte level, uint manaThreshold, uint baseAction) {

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

internal abstract class SwiftRaiseCombo: CustomCombo {
	public override uint[] ActionIDs { get; } = new[] { AST.Ascend, RDM.Verraise, SCH.Resurrection, SGE.Egeiro, SMN.Resurrection, WHM.Raise };

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
