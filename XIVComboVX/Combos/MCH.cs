namespace PrincessRTFM.XIVComboVX.Combos;

using Dalamud.Game.ClientState.JobGauge.Types;

using PrincessRTFM.XIVComboVX;

internal static class MCH {
	public const byte JobID = 31;

	public const uint
		// Single target
		CleanShot = 2873,
		HeatedCleanShot = 7413,
		SplitShot = 2866,
		HeatedSplitShot = 7411,
		SlugShot = 2868,
		HeatedSlugshot = 7412,
		// Charges
		GaussRound = 2874,
		Ricochet = 2890,
		// AoE
		SpreadShot = 2870,
		AutoCrossbow = 16497,
		Scattergun = 25786,
		// Rook
		RookAutoturret = 2864,
		RookOverdrive = 7415,
		AutomatonQueen = 16501,
		QueenOverdrive = 16502,
		// Other
		BarrelStabiliser = 7414,
		Tactician = 16889,
		Dismantle = 2887,
		Wildfire = 2878,
		Detonator = 16766,
		Hypercharge = 17209,
		HeatBlast = 7410,
		HotShot = 2872,
		Drill = 16498,
		Bioblaster = 16499,
		AirAnchor = 16500,
		Chainsaw = 25788;

	public static class Buffs {
		public const ushort
			Reassembled = 851;
	}

	public static class Debuffs {
		// public const ushort placeholder = 0;
	}

	public static class Levels {
		public const byte
			SlugShot = 2,
			HotShot = 4,
			GaussRound = 15,
			CleanShot = 26,
			Hypercharge = 30,
			HeatBlast = 35,
			RookOverdrive = 40,
			Wildfire = 45,
			Ricochet = 50,
			AutoCrossbow = 52,
			HeatedSplitShot = 54,
			Tactician = 56,
			Drill = 58,
			HeatedSlugshot = 60,
			Dismantle = 62,
			HeatedCleanShot = 64,
			BarrelStabiliser = 66,
			ChargedActionMastery = 74,
			AirAnchor = 76,
			QueenOverdrive = 80,
			Chainsaw = 90;
	}
}

internal class MachinistCleanShot: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.MachinistMainCombo;
	public override uint[] ActionIDs { get; } = new[] { MCH.CleanShot, MCH.HeatedCleanShot };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) { // XXX all of this needs to be ripped out and rewritten from scratch
		MCHGauge gauge = GetJobGauge<MCHGauge>();

		if (CanWeave(actionID)) {

			if (IsEnabled(CustomComboPreset.MachinistMainComboWeaveHypercharge) && level >= MCH.Levels.Hypercharge) {
				if (gauge.Heat >= Service.Configuration.MachinistMainComboWeaveHyperchargeHeatThreshold && !gauge.IsOverheated) {

					if (IsEnabled(CustomComboPreset.MachinistHyperfire) && level >= MCH.Levels.Wildfire) {
						if (IsOffCooldown(MCH.Wildfire) && HasTarget)
							return MCH.Wildfire;
					}

					return MCH.Hypercharge;
				}
			}

			if (IsEnabled(CustomComboPreset.MachinistGaussRoundRicochetDirectWeave) && level >= MCH.Levels.GaussRound) {
				if (gauge.IsOverheated) {
					if (level < MCH.Levels.HeatBlast || CanWeave(MCH.HeatBlast)) { // Heat Blast has a 1.5s cooldown instead of the normal GCD

						if (level >= MCH.Levels.Ricochet)
							return PickByCooldown(MCH.GaussRound, MCH.GaussRound, MCH.Ricochet);

						return MCH.GaussRound;
					}
				}
			}

			if (IsEnabled(CustomComboPreset.MachinistMainComboWeaveBarrelStabiliser) && level >= MCH.Levels.BarrelStabiliser) {
				if (!gauge.IsOverheated && gauge.Heat <= 50 && InCombat && CanUse(MCH.BarrelStabiliser))
					return MCH.BarrelStabiliser;
			}

		}

		if (IsEnabled(CustomComboPreset.MachinistMainComboReassembledOverride)) {

			if (level is >= MCH.Levels.HotShot and < MCH.Levels.CleanShot) { // note that Hot Shot is LESS potency than Clean Shot when part of the combo
				if (SelfHasEffect(MCH.Buffs.Reassembled)) {
					if (CanUse(MCH.HotShot))
						return MCH.HotShot;
				}
			}

			if (level >= MCH.Levels.Drill) {
				if (SelfHasEffect(MCH.Buffs.Reassembled)) {

					if (level >= MCH.Levels.Chainsaw && IsEnabled(CustomComboPreset.MachinistMainComboReassembledOverridePlus)) {
						return gauge.Battery > 80
							? PickByCooldown(MCH.Drill, actionID, MCH.Chainsaw, MCH.AirAnchor, MCH.Drill)
							: PickByCooldown(MCH.AirAnchor, actionID, MCH.Chainsaw, MCH.Drill, MCH.AirAnchor);
					}

					if (level >= MCH.Levels.AirAnchor) {
						return gauge.Battery > 80
							? PickByCooldown(MCH.Drill, actionID, MCH.AirAnchor, MCH.Drill)
							: PickByCooldown(MCH.AirAnchor, actionID, MCH.Drill, MCH.AirAnchor);
					}

					return CanUse(MCH.Drill) ? MCH.Drill : actionID;
				}
			}
		}

		if (IsEnabled(CustomComboPreset.MachinistMainComboHeatBlast) && level >= MCH.Levels.HeatBlast && gauge.IsOverheated)
			return MCH.HeatBlast;

		if (comboTime > 0) {

			if (lastComboMove is MCH.SplitShot or MCH.HeatedSplitShot && level >= MCH.Levels.SlugShot)
				return OriginalHook(MCH.SlugShot);

			if (lastComboMove is MCH.SlugShot or MCH.HeatedSlugshot && level >= MCH.Levels.CleanShot)
				return OriginalHook(MCH.CleanShot);

		}

		return OriginalHook(MCH.SplitShot);
	}
}

internal class MachinistGaussRoundRicochet: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.MachinistGaussRoundRicochet;
	public override uint[] ActionIDs { get; } = new[] { MCH.GaussRound, MCH.Ricochet };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (IsEnabled(CustomComboPreset.MachinistGaussRoundRicochetLimiter) && !GetJobGauge<MCHGauge>().IsOverheated)
			return actionID;

		if (level >= MCH.Levels.Ricochet)
			return PickByCooldown(actionID, MCH.Ricochet, MCH.GaussRound);

		return MCH.GaussRound;
	}
}

internal class MachinistHyperfire: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.MachinistHyperfire;
	public override uint[] ActionIDs { get; } = new[] { MCH.Hypercharge };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= MCH.Levels.Wildfire && IsOffCooldown(MCH.Wildfire) && HasTarget)
			return MCH.Wildfire;

		if (level >= MCH.Levels.Wildfire && IsOnCooldown(MCH.Hypercharge) && !IsOriginal(MCH.Wildfire))
			return MCH.Detonator;

		return actionID;
	}
}

internal class MachinistHeatBlastAutoCrossbow: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.MachinistOverheat;
	public override uint[] ActionIDs { get; } = new[] { MCH.HeatBlast, MCH.AutoCrossbow };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level < MCH.Levels.Hypercharge)
			return MCH.Hypercharge;

		MCHGauge gauge = GetJobGauge<MCHGauge>();

		if (IsEnabled(CustomComboPreset.MachinistHyperfire) && level >= MCH.Levels.Wildfire) {
			if (IsOffCooldown(MCH.Wildfire) && HasTarget)
				return MCH.Wildfire;
		}

		if (!gauge.IsOverheated)
			return MCH.Hypercharge;

		if ((actionID is MCH.HeatBlast || level < MCH.Levels.AutoCrossbow) && level >= MCH.Levels.HeatBlast) {
			if (IsEnabled(CustomComboPreset.MachinistGaussRoundRicochetDirectWeave)) {
				if (gauge.IsOverheated && CanWeave(MCH.HeatBlast)) { // Heat Blast has a 1.5s cooldown instead of the normal GCD

					if (level >= MCH.Levels.Ricochet)
						return PickByCooldown(MCH.GaussRound, MCH.GaussRound, MCH.Ricochet);

					return MCH.GaussRound;
				}
			}
		}

		if (level < MCH.Levels.AutoCrossbow)
			return MCH.HeatBlast;

		return actionID;
	}
}

internal class MachinistSpreadShotFeature: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.MachinistSpreadShot;
	public override uint[] ActionIDs { get; } = new[] { MCH.SpreadShot, MCH.Scattergun };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= MCH.Levels.AutoCrossbow && GetJobGauge<MCHGauge>().IsOverheated)
			return MCH.AutoCrossbow;

		return actionID;
	}
}

internal class MachinistOverdriveFeature: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.MachinistOverdrive;
	public override uint[] ActionIDs { get; } = new[] { MCH.RookAutoturret, MCH.AutomatonQueen };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= MCH.Levels.RookOverdrive && GetJobGauge<MCHGauge>().IsRobotActive)
			return OriginalHook(MCH.RookOverdrive);

		return actionID;
	}
}

internal class MachinistDrillAirAnchorFeature: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.MachinistDrillAirAnchor;
	public override uint[] ActionIDs { get; } = new[] { MCH.HotShot, MCH.AirAnchor, MCH.Drill };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= MCH.Levels.Chainsaw && IsEnabled(CustomComboPreset.MachinistDrillAirAnchorPlus)) {
			return GetJobGauge<MCHGauge>().Battery > 80
				? PickByCooldown(MCH.Drill, MCH.Chainsaw, MCH.AirAnchor, MCH.Drill)
				: PickByCooldown(MCH.AirAnchor, MCH.Chainsaw, MCH.Drill, MCH.AirAnchor);
		}

		if (level >= MCH.Levels.AirAnchor) {
			return GetJobGauge<MCHGauge>().Battery > 80
				? PickByCooldown(MCH.Drill, MCH.AirAnchor, MCH.Drill)
				: PickByCooldown(MCH.AirAnchor, MCH.Drill, MCH.AirAnchor);
		}

		if (level >= MCH.Levels.Drill)
			return PickByCooldown(actionID, MCH.HotShot, MCH.Drill);

		return MCH.HotShot;
	}
}

internal class MachinistTacticianDismantle: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.MachinistTacticianDismantle;
	public override uint[] ActionIDs { get; } = new uint[] { MCH.Tactician, MCH.Dismantle };

	protected override uint Invoke(uint actionID, uint lastComboActionId, float comboTime, byte level) {

		if (level <= MCH.Levels.Dismantle)
			return MCH.Tactician;

		if (!HasTarget || TargetDistance > 25)
			return MCH.Tactician;

		return PickByCooldown(actionID, MCH.Tactician, MCH.Dismantle);
	}
}
