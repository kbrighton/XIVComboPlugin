
using Dalamud.Game.ClientState.Conditions;

using XIVComboVX;
using XIVComboVX.Combos;

namespace XIVCombo.Combos {
	internal static class DOL {
		public const byte JobID = 99,
			MinID = 16,
			BtnID = 17,
			FshID = 18;

		public const uint
			AgelessWords = 215,
			SolidReason = 232,
			Bait = 288,
			Cast = 289,
			Hook = 296,
			Quit = 299,
			Snagging = 4100,
			Chum = 4104,
			SurfaceSlap = 4595,
			IdenticalCast = 4596,
			Gig = 7632,
			VeteranTrade = 7906,
			NaturesBounty = 7909,
			Salvage = 7910,
			MinWiseToTheWorld = 26521,
			BtnWiseToTheWorld = 26522,
			MakeshiftBait = 26805,
			PrizeCatch = 26806,
			VitalSight = 26870,
			BaitedBreath = 26871,
			ElectricCurrent = 26872;

		public static class Buffs {
			public const ushort
				EurekaMoment = 2765;
		}

		public static class Debuffs {
			public const ushort
				Placeholder = 0;
		}

		public static class Levels {
			public const byte
				Snagging = 36,
				Gig = 61,
				Salvage = 67,
				VeteranTrade = 63,
				NaturesBounty = 69,
				SurfaceSlap = 71,
				PrizeCatch = 81,
				WiseToTheWorld = 90;
		}
	}

	internal class EurekaFeature: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.GatherEurekaFeature;
		public override uint[] ActionIDs => new[] { DOL.SolidReason, DOL.AgelessWords };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= DOL.Levels.WiseToTheWorld && SelfHasEffect(DOL.Buffs.EurekaMoment)) {
				return IsJob(DOL.MinID)
					? DOL.MinWiseToTheWorld
					: DOL.BtnWiseToTheWorld;
			}

			return actionID;
		}
	}

	internal class FisherSwapFeatures: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.DolAny;
		// No ActionIDs are set because this applies to a wide enough variety of actions that it's too much duplication

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (!IsJob(DOL.FshID)) {
				Service.Logger.debug($"Not a fisher ({LocalPlayer.ClassJob.Id} != {DOL.FshID})");
				return actionID;
			}

			if (HasCondition(ConditionFlag.Fishing)) {

				Service.Logger.debug($"Checking {this.ModuleName}'s fishing swaps");

				if (actionID is DOL.Cast && IsEnabled(CustomComboPreset.FisherCastHookFeature))
					return DOL.Hook;

			}
			else if (HasCondition(ConditionFlag.Diving)) {

				Service.Logger.debug($"Checking {this.ModuleName}'s diving swaps");

				if (actionID is DOL.Cast && IsEnabled(CustomComboPreset.FisherCastGigFeature))
					return DOL.Gig;

				else if (actionID is DOL.SurfaceSlap && IsEnabled(CustomComboPreset.FisherSurfaceTradeFeature))
					return DOL.VeteranTrade;

				else if (actionID is DOL.PrizeCatch && IsEnabled(CustomComboPreset.FisherPrizeBountyFeature))
					return DOL.NaturesBounty;

				else if (actionID is DOL.Snagging && IsEnabled(CustomComboPreset.FisherSnaggingSalvageFeature))
					return DOL.Salvage;

				else if (actionID is DOL.IdenticalCast && IsEnabled(CustomComboPreset.FisherIdenticalSightFeature))
					return DOL.VitalSight;

				else if (actionID is DOL.MakeshiftBait && IsEnabled(CustomComboPreset.FisherMakeshiftBreathFeature))
					return DOL.BaitedBreath;

				else if (actionID is DOL.Chum && IsEnabled(CustomComboPreset.FisherElectricChumFeature))
					return DOL.ElectricCurrent;

			}
			else {
				Service.Logger.debug("Neither fishing nor diving");
			}

			return actionID;
		}
	}
}
