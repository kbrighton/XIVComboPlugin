using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboVX.Combos {
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
			Hypercharge = 17209,
			HeatBlast = 7410,
			HotShot = 2872,
			Drill = 16498,
			AirAnchor = 16500,
			Chainsaw = 25788;

		public static class Buffs {
			// public const ushort placeholder = 0;
		}

		public static class Debuffs {
			// public const ushort placeholder = 0;
		}

		public static class Levels {
			public const byte
				SlugShot = 2,
				GaussRound = 15,
				CleanShot = 26,
				Hypercharge = 30,
				HeatBlast = 35,
				RookOverdrive = 40,
				Ricochet = 50,
				AutoCrossbow = 52,
				HeatedSplitShot = 54,
				Drill = 58,
				HeatedSlugshot = 60,
				HeatedCleanShot = 64,
				ChargedActionMastery = 74,
				AirAnchor = 76,
				QueenOverdrive = 80,
				Chainsaw = 90;
		}
	}

	internal class MachinistMainCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.MachinistMainCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { MCH.CleanShot, MCH.HeatedCleanShot };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is MCH.CleanShot or MCH.HeatedCleanShot) {

				if (comboTime > 0) {
					if (lastComboMove is MCH.SplitShot or MCH.HeatedSplitShot && level >= MCH.Levels.SlugShot)
						return OriginalHook(MCH.SlugShot);

					if (lastComboMove is MCH.SlugShot or MCH.HeatedSlugshot && level >= MCH.Levels.CleanShot)
						return OriginalHook(MCH.CleanShot);
				}

				return OriginalHook(MCH.SplitShot);
			}

			return actionID;
		}
	}

	internal class MachinistGaussRoundRicochetFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.MachinistGaussRoundRicochetFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { MCH.GaussRound, MCH.Ricochet };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is MCH.GaussRound or MCH.Ricochet) {

				if (level >= MCH.Levels.Ricochet)
					return PickByCooldown(actionID, MCH.Ricochet, MCH.GaussRound);

				return MCH.GaussRound;
			}

			return actionID;
		}
	}

	internal class MachinistOverheatFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.MachinistOverheatFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { MCH.HeatBlast, MCH.AutoCrossbow };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is MCH.HeatBlast or MCH.AutoCrossbow) {

				if (!GetJobGauge<MCHGauge>().IsOverheated)
					return MCH.Hypercharge;

				if (level < MCH.Levels.AutoCrossbow)
					return MCH.HeatBlast;

			}

			return actionID;
		}
	}

	internal class MachinistSpreadShotFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.MachinistSpreadShotFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { MCH.SpreadShot, MCH.Scattergun };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is MCH.SpreadShot or MCH.Scattergun) {

				if (level >= MCH.Levels.AutoCrossbow && GetJobGauge<MCHGauge>().IsOverheated)
					return MCH.AutoCrossbow;

			}

			return actionID;
		}
	}

	internal class MachinistOverdriveFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.MachinistOverdriveFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { MCH.RookAutoturret, MCH.AutomatonQueen };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is MCH.RookAutoturret or MCH.AutomatonQueen) {

				if (level >= MCH.Levels.RookOverdrive && GetJobGauge<MCHGauge>().IsRobotActive)
					return OriginalHook(MCH.RookOverdrive);

			}

			return actionID;
		}
	}

	internal class MachinistDrillAirAnchorFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.MachinistDrillAirAnchorFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { MCH.HotShot, MCH.AirAnchor, MCH.Drill, MCH.Chainsaw };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is MCH.HotShot or MCH.AirAnchor or MCH.Drill or MCH.Chainsaw) {

				if (level >= MCH.Levels.Chainsaw)
					return PickByCooldown(actionID, MCH.Chainsaw, MCH.AirAnchor, MCH.Drill);

				if (level >= MCH.Levels.AirAnchor)
					return PickByCooldown(actionID, MCH.AirAnchor, MCH.Drill);

				if (level >= MCH.Levels.Drill)
					return PickByCooldown(actionID, MCH.Drill, MCH.HotShot);

				return MCH.HotShot;
			}

			return actionID;
		}
	}
}
