using System;

using Dalamud.Game.ClientState.JobGauge.Types;

namespace PrincessRTFM.XIVComboVX.Combos;

internal static class RDM {
	public const byte JobID = 35;

	public const int
		ManaCostMelee1 = 20,
		ManaCostMelee2 = 15,
		ManaCostMelee3 = 15;

	public const uint
		Verraise = 7523,
		Verthunder = 7505,
		Corpsacorps = 7506,
		Veraero = 7507,
		Scatter = 7509,
		Displacement = 7515,
		Veraero2 = 16525,
		Verthunder2 = 16524,
		Impact = 16526,
		Embolden = 7520,
		Manafication = 7521,
		Redoublement = 7516,
		EnchantedRedoublement = 7529,
		Zwerchhau = 7512,
		EnchantedZwerchhau = 7528,
		Riposte = 7504,
		EnchantedRiposte = 7527,
		Jolt = 7503,
		Verstone = 7511,
		Verfire = 7510,
		Moulinet = 7513,
		Fleche = 7517,
		Acceleration = 7518,
		ContreSixte = 7519,
		Jolt2 = 7524,
		Verholy = 7526,
		Verflare = 7525,
		Engagement = 16527,
		Scorch = 16530,
		Resolution = 25858,
		Jolt3 = 37004,
		ViceOfThorns = 37005,
		GrandImpact = 37006,
		Prefulgence = 37007,
		EnchantedMoulinet = 7530,
		EnchantedMoulinetDeux = 37002,
		EnchantedMoulinetTrois = 37003;

	public static class Buffs {
		public const ushort
			VerfireReady = 1234,
			VerstoneReady = 1235,
			Acceleration = 1238,
			GrandImpactReady = 3877,
			ThornedFlourish = 3876,
			MagickedSwordplay = 3875,
			PrefulgenceReady = 3878,
			Dualcast = 1249;
	}

	public static class Debuffs {
		// public const ushort placeholder = 0;
	}

	public static class Levels {
		public const byte
			Jolt = 2,
			Verthunder = 4,
			Corpsacorps = 6,
			Veraero = 10,
			Scatter = 15,
			Swiftcast = 18,
			Verthunder2 = 18,
			Veraero2 = 22,
			Zwerchhau = 35,
			Displacement = 40,
			Engagement = 40,
			Fleche = 45,
			Redoublement = 50,
			Acceleration = 50,
			EnchantedMoulinets = 52,
			Vercure = 54,
			ContreSixte = 56,
			Embolden = 58,
			Manafication = 60,
			Jolt2 = 62,
			Verraise = 64,
			Impact = 66,
			Verflare = 68,
			Verholy = 70,
			Scorch = 80,
			Resolution = 90,
			ViceOfThorns = 92,
			GrandImpact = 96,
			Prefulgence = 100;
	}

#pragma warning disable IDE0045 // Convert to conditional expression - helper function readability
	public static bool CheckFinishers(ref uint actionID, uint lastComboMove, byte level) {
		const int
			finisherDelta = 11,
			imbalanceDiffMax = 30;

		if (lastComboMove is Verflare or Verholy && level >= Levels.Scorch) {
			actionID = Scorch;
			return true;
		}

		if (lastComboMove is Scorch && level >= Levels.Resolution) {
			actionID = Resolution;
			return true;
		}

		RDMGauge gauge = CustomCombo.GetJobGauge<RDMGauge>();

		if (gauge.ManaStacks == 3 && level >= Levels.Verflare) {
			int black = gauge.BlackMana;
			int white = gauge.WhiteMana;
			bool canFinishWhite = level >= Levels.Verholy;
			int blackThreshold = white + imbalanceDiffMax;
			int whiteThreshold = black + imbalanceDiffMax;
			bool verfireUp = CustomCombo.SelfHasEffect(Buffs.VerfireReady);
			bool verstoneUp = CustomCombo.SelfHasEffect(Buffs.VerstoneReady);

			if (black >= white && canFinishWhite) {
				// If we can already Verstone, but we can't Verfire, and Verflare WON'T imbalance us, use Verflare
				if (verstoneUp && !verfireUp && (black + finisherDelta <= blackThreshold))
					actionID = Verflare;
				else
					actionID = Verholy;
			}
			// If we can already Verfire, but we can't Verstone, and we can use Verholy, and it WON'T imbalance us, use Verholy
			else if (verfireUp && !verstoneUp && canFinishWhite && (white + finisherDelta <= whiteThreshold)) {
				actionID = Verholy;
			}
			else {
				actionID = Verflare;
			}

			return true;
		}

		return false;
	}

	public static bool CheckMeleeST(ref uint actionID, uint lastComboMove, byte level, bool checkComboStart) {
		RDMGauge gauge = CustomCombo.GetJobGauge<RDMGauge>();
		byte black = gauge.BlackMana;
		byte white = gauge.WhiteMana;
		byte mana = black != white || black == 100
			? Math.Min(black, white)
			: (byte)0;
		bool buff = CustomCombo.SelfHasEffect(RDM.Buffs.MagickedSwordplay);

		if (lastComboMove is Zwerchhau or EnchantedZwerchhau && level >= Levels.Redoublement && (buff || mana >= ManaCostMelee3)) {
			actionID = EnchantedRedoublement;
			return true;
		}

		if (lastComboMove is Riposte or EnchantedRiposte && level >= Levels.Zwerchhau && (buff || mana >= ManaCostMelee2)) {
			actionID = EnchantedZwerchhau;
			return true;
		}

		if (checkComboStart && (buff || mana >= ManaCostMelee1 + ManaCostMelee2 + ManaCostMelee3)) {
			actionID = EnchantedRiposte;
			return true;
		}

		return false;
	}

	public static bool CheckMeleeAOE(ref uint actionID, uint lastComboMove, byte level, bool checkComboStart) {
		if (level < Levels.EnchantedMoulinets)
			return false;

		RDMGauge gauge = CustomCombo.GetJobGauge<RDMGauge>();
		byte mana = Math.Min(gauge.BlackMana, gauge.WhiteMana);

		if (lastComboMove is EnchantedMoulinetDeux && mana >= ManaCostMelee3) {
			actionID = EnchantedMoulinetTrois;
			return true;
		}

		if (lastComboMove is Moulinet or EnchantedMoulinet && mana >= ManaCostMelee2) {
			actionID = EnchantedMoulinetDeux;
			return true;
		}

		if (checkComboStart && mana >= ManaCostMelee1) {
			actionID = EnchantedMoulinet;
			return true;
		}

		return false;
	}

	public static bool CheckAbilityAttacks(ref uint actionID, byte level) {
		if (!CustomCombo.IsEnabled(CustomComboPreset.RedMageContreFleche))
			return false;

		float prefulgenceTimeLeft = CustomCombo.IsEnabled(CustomComboPreset.RedMageContreFlechePrefulgence) && level >= RDM.Levels.Prefulgence
			? CustomCombo.SelfEffectDuration(Buffs.PrefulgenceReady)
			: 0f;
		float thornsTimeLeft = CustomCombo.IsEnabled(CustomComboPreset.RedMageContreFlecheThorns) && level >= Levels.ViceOfThorns
			? CustomCombo.SelfEffectDuration(Buffs.ThornedFlourish)
			: 0f;

		if (prefulgenceTimeLeft > 0) {

			// If we're almost out of time to use VoT but Prefulgence has enough time left to use VoT and also itself, use VoT first to save it from being lost
			if (thornsTimeLeft is > 0 and < 3 && prefulgenceTimeLeft >= 3)
				actionID = RDM.ViceOfThorns;
			else
				actionID = Prefulgence;

			return true;
		}

		if (thornsTimeLeft > 0) {
			actionID = ViceOfThorns;
			return true;
		}

		if (level >= Levels.ContreSixte) {
			actionID = CustomCombo.PickByCooldown(actionID, Fleche, ContreSixte);
			return true;
		}

		if (level >= Levels.Fleche) {
			actionID = Fleche;
			return true;
		}

		return false;
	}

#pragma warning restore IDE0045 // Convert to conditional expression
}

internal class RedMageSwiftcastRaiserFeature: SwiftRaiseCombo {
	public override CustomComboPreset Preset => CustomComboPreset.RedMageSwiftcastRaiserFeature;
}

internal class RedMageAoECombo: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.RedMageAoECombo;
	public override uint[] ActionIDs { get; } = [RDM.Veraero2, RDM.Verthunder2];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (RDM.CheckFinishers(ref actionID, lastComboMove, level))
			return actionID;

		if (level >= RDM.Levels.Scatter && (IsFastcasting || SelfHasEffect(RDM.Buffs.Acceleration) || SelfHasEffect(RDM.Buffs.GrandImpactReady)))
			return OriginalHook(RDM.Scatter);

		return actionID;
	}
}

internal class RedMageMeleeCombo: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.RedMageMeleeCombo;
	public override uint[] ActionIDs { get; } = [RDM.Riposte, RDM.Moulinet];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (IsEnabled(CustomComboPreset.RedMageMeleeComboPlus)) {
			if (RDM.CheckFinishers(ref actionID, lastComboMove, level))
				return actionID;
		}

		if (actionID is RDM.Riposte) {

			if (IsEnabled(CustomComboPreset.RedMageMeleeComboCloser)) {
				if (HasTarget && !InMeleeRange)
					return RDM.Corpsacorps;
			}

			RDM.CheckMeleeST(ref actionID, lastComboMove, level, true); // actionID will be untouched (Riposte), Enchanted Riposte, Enchanted Zwerchhau, or Enchanted Redoublement
		}
		else {
			RDM.CheckMeleeAOE(ref actionID, lastComboMove, level, true); // actionID will be untouched (Moulinet), Enchanted Moulinet, Enchanted Moulinet Deux, or Enchanted Moulinet Trois
		}

		return OriginalHook(actionID);
	}
}

internal class RedMageContreFlecheFeature: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.RedMageContreFleche;
	public override uint[] ActionIDs { get; } = [RDM.Fleche, RDM.ContreSixte];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
		RDM.CheckAbilityAttacks(ref actionID, level);

		return actionID;
	}
}

internal class RedMageSmartcastAoECombo: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.RedMageSmartcastAoE;
	public override uint[] ActionIDs { get; } = [RDM.Veraero2, RDM.Verthunder2];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
		const int normalDelta = 7;

		RDMGauge gauge = GetJobGauge<RDMGauge>();
		int black = gauge.BlackMana;
		int white = gauge.WhiteMana;
		bool weaving = CanWeave(actionID);

		if (Common.CheckLucidWeave(CustomComboPreset.RedMageSmartcastAoEWeaveLucid, level, Service.Configuration.RedMageSmartcastAoEWeaveLucidManaThreshold, actionID))
			return Common.LucidDreaming;

		// There is never a reason to NOT use the finishers when you have them.
		if (RDM.CheckFinishers(ref actionID, lastComboMove, level))
			return actionID;

		bool fastCast = IsFastcasting;

		// Yes, this block being below the finisher checks means that you won't get a smart weave while doing the finisher combo.
		// However, that's available on the ST smartcast option, which means it's still available while the AoE one here will show your GCD.
		// More importantly, I don't want to duplicate the whole block above the finishers, so deal with it.
		if ((IsEnabled(CustomComboPreset.RedMageSmartcastAoEWeaveAttack) && weaving) || (IsEnabled(CustomComboPreset.RedMageSmartcastAoEMovement) && IsMoving && !fastCast)) {
			if (RDM.CheckAbilityAttacks(ref actionID, level)) {
				return actionID;
			}
			else if (level >= RDM.Levels.ContreSixte) {
				return RDM.ContreSixte;
			}
		}

		if (fastCast || SelfHasEffect(RDM.Buffs.Acceleration) || SelfHasEffect(RDM.Buffs.GrandImpactReady) || level < RDM.Levels.Verthunder2)
			return OriginalHook(RDM.Impact);

		if (level < RDM.Levels.Veraero2)
			return RDM.Verthunder2;

		if (white < black || Math.Min(100, black + normalDelta) == white)
			return RDM.Veraero2;

		if (black < white || Math.Min(100, white + normalDelta) == black)
			return RDM.Verthunder2;

		return actionID;
	}
}

internal class RedmageSmartcastSingleComboOpener: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.RedMageSmartcastSingleTarget;
	public override uint[] ActionIDs { get; } = [RDM.Veraero, RDM.Verthunder];

	protected override uint Invoke(uint actionID, uint lastComboActionId, float comboTime, byte level) {
		const int longDelta = 6;
		RDMGauge gauge = GetJobGauge<RDMGauge>();
		int black = gauge.BlackMana;
		int white = gauge.WhiteMana;

		if (level < RDM.Levels.Verthunder)
			return RDM.Jolt;

		if (level is < RDM.Levels.Veraero and >= RDM.Levels.Verthunder)
			return OriginalHook(RDM.Verthunder);

		// This is for the long opener only, so we're not bothered about fast casting or finishers or anything like that
		// However, we DO want to prevent the mana levels from being perfectly even, cause that fucks up Manafication into melee as an opener

		if (black < white || Math.Min(100, white + longDelta) == black)
			return OriginalHook(RDM.Verthunder);

		if (white < black || Math.Min(100, black + longDelta) == white)
			return OriginalHook(RDM.Veraero);

		return actionID;
	}
}

internal class RedmageSmartcastSingleComboFull: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.RedMageSmartcastSingleTarget;
	public override uint[] ActionIDs { get; } = [RDM.Verstone, RDM.Verfire];

	private static uint noCastingSubCheck(byte level, bool engageCheck, bool holdOneEngageCharge, bool engageEarly, bool canMelee, bool allowAccelerate) {

		if (allowAccelerate) {
			bool canAccelerate = level >= RDM.Levels.Acceleration && CanUse(RDM.Acceleration);
			bool canSwiftcast = IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetAccelerationSwiftcast) && CanUse(Common.Swiftcast);

			if (canSwiftcast && IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetAccelerationSwiftcastFirst))
				return Common.Swiftcast;
			if (canAccelerate)
				return RDM.Acceleration;
			if (canSwiftcast)
				return Common.Swiftcast;
		}

		ushort engageCharges = engageCheck && canMelee ? GetCooldown(RDM.Engagement).RemainingCharges : (ushort)0;
		bool canEngage = engageCharges > 0;

		bool shouldEngage = canEngage
			&& (!holdOneEngageCharge || engageCharges > 1);

		if (shouldEngage && engageEarly)
			return RDM.Engagement;

		if (level >= RDM.Levels.Prefulgence && SelfHasEffect(RDM.Buffs.PrefulgenceReady))
			return RDM.Prefulgence;

		if (level >= RDM.Levels.ViceOfThorns && SelfHasEffect(RDM.Buffs.ThornedFlourish))
			return RDM.ViceOfThorns;

		// Grand Impact is SPECIFICALLY excluded because it's a spell, not an ability, which makes it a GCD.
		// Therefore, since this helper can be used for moving OR for weaving, it should be handled by the AOE spell combo instead.

		if (level >= RDM.Levels.Fleche) {
			uint actionID = RDM.Fleche;
			RDM.CheckAbilityAttacks(ref actionID, level);
			if (IsOffCooldown(actionID))
				return actionID;
		}

		if (shouldEngage)
			return RDM.Engagement;

		return 0;
	}

	protected override uint Invoke(uint actionID, uint lastComboActionId, float comboTime, byte level) {
		const int
			longDelta = 6,
			procDelta = 5,
			imbalanceDiffMax = 30;

		// This algorithm is a bit messy, because.. well, there's no clean way to do it, really.
		// The same conditions need to be checked at different stages in the flow because of priorities.
		// As a result, we kind of have to preload a bunch of checks, or else we'll be repeating them.
		// Plus, some of the conditions get complex, so it's better to split things up a little like this.
		// Just let the compiler handle optimisations :)

		bool fastCasting = IsFastcasting;
		bool accelerated = SelfHasEffect(RDM.Buffs.Acceleration);
		bool instacasting = fastCasting || accelerated;
		bool weaving = CanWeave(actionID);
		bool moving = IsMoving;
		bool targeting = HasTarget;
		bool fighting = InCombat;
		bool isClose = InMeleeRange;

		RDMGauge gauge = GetJobGauge<RDMGauge>();

		int black = gauge.BlackMana;
		int white = gauge.WhiteMana;
		int gaugeMin = Math.Min(black, white);
		int blackThreshold = white + imbalanceDiffMax;
		int whiteThreshold = black + imbalanceDiffMax;

		int minManaForEnchantedMelee = RDM.ManaCostMelee1 + (level >= RDM.Levels.Zwerchhau ? RDM.ManaCostMelee2 : 0) + (level >= RDM.Levels.Redoublement ? RDM.ManaCostMelee3 : 0);
		bool hasMeleeMana = (
				gaugeMin >= minManaForEnchantedMelee
				&& (black != white || black is 100 || level <= RDM.Levels.Verflare)
			)
			|| SelfHasEffect(RDM.Buffs.MagickedSwordplay);

		bool verfireUp = SelfHasEffect(RDM.Buffs.VerfireReady);
		bool verstoneUp = SelfHasEffect(RDM.Buffs.VerstoneReady);
		bool isFinishingAny = RDM.CheckFinishers(ref actionID, lastComboActionId, level);

		bool meleeCombo = IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetMeleeCombo)
			&& !isFinishingAny && targeting
			&& RDM.CheckMeleeST(ref actionID, lastComboActionId, level, IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetMeleeComboStarter));
		bool shouldCloseGap = IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetMeleeComboStarterCloser)
			&& meleeCombo && !isClose;
		meleeCombo &= isClose;

		bool smartWeave = IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetWeaveAttack) && weaving;
		bool smartMove = IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetMovement) && moving;

		bool accelerate = level >= Common.Levels.Swiftcast
			&& !instacasting
			&& !isFinishingAny
			&& !meleeCombo
			&& !verfireUp
			&& !verstoneUp
			&& IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetAcceleration);
		bool accelLimitCombat = IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetAccelerationCombat);
		bool allowAccel = accelerate && (fighting || !accelLimitCombat);
		bool accelWeave = allowAccel && IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetAccelerationWeave);
		bool accelMove = allowAccel && IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetAccelerationMoving);
		bool accelNoNormal = IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetAccelerationNoOverride);

		if (Common.CheckLucidWeave(CustomComboPreset.RedMageSmartcastSingleTargetWeaveLucid, level, Service.Configuration.RedMageSmartcastSingleWeaveLucidManaThreshold, actionID))
			return Common.LucidDreaming;

		if (smartWeave) {
			// This is basically universal.
			// I know it's a mess. Moving it into another method was basically the best I could do, since the whole thing is duplicated for weaving and moving but with different variables.
			bool
				engageCheck = IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetWeaveMelee) && weaving,
				holdOneEngageCharge = IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetWeaveMeleeHoldOne),
				engageEarly = IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetWeaveMeleeFirst);
			uint alt = noCastingSubCheck(level, engageCheck, holdOneEngageCharge, engageEarly, targeting && isClose, accelWeave);
			if (alt > 0)
				return alt;
		}
		if (isFinishingAny) {
			// This accounts for both the finisher combo chain (Scorch and Resolution) AND the initial decision of whether to START the finishers (Verflare or Verholy)
			// Since you lose your mana stacks when you cast ANY spell, you want to use the finishers as soon as they're up, so you don't lose them
			return actionID;
		}
		if (instacasting) {
			// TODO: need to account for hardcasting spells with no cast time!

			if (level is < RDM.Levels.Veraero and >= RDM.Levels.Verthunder)
				return RDM.Verthunder;

			if (verfireUp == verstoneUp) {

				// Either both procs are already up or neither is - use whatever gives us the mana we need
				if (black < white || Math.Min(100, white + longDelta) == black)
					return OriginalHook(RDM.Verthunder);

				if (white < black || Math.Min(100, black + longDelta) == white)
					return OriginalHook(RDM.Veraero);

				// If mana levels are equal, prioritise the colour that the original button was
				return actionID is RDM.Verstone
					? OriginalHook(RDM.Veraero)
					: OriginalHook(RDM.Verthunder);
			}

			if (verfireUp) {

				// If Veraero is feasible, use it
				if (white + longDelta <= whiteThreshold && Math.Min(100, white + longDelta) != black)
					return OriginalHook(RDM.Veraero);

				return OriginalHook(RDM.Verthunder);
			}

			if (verstoneUp) {

				// If Verthunder is feasible, use it
				if (black + longDelta <= blackThreshold && Math.Min(100, black + longDelta) != white)
					return OriginalHook(RDM.Verthunder);

				return OriginalHook(RDM.Veraero);
			}
		}
		if (shouldCloseGap) {
			// If this is the case, then meleeCombo CANNOT be, because one requires isClose and one requires !isClose, so the order of these two doesn't really matter.
			// I decided to put it here because logically, you need to close before you can melee.
			return RDM.Corpsacorps;
		}
		if (meleeCombo) {
			// If we're out of range while in the combo, become Corps-a-corps to get back in range. Otherwise, just run the combo.

			if (!(targeting && isClose))
				return RDM.Corpsacorps;

			return actionID; // meleeCombo is only true if the helper function assigned the appropriate actionID value
		}
		if (smartMove) {
			// Can't slowcast spells if you're moving, so we have to fall back to instants.
			// I know it's a mess. Moving it into another method was basically the best I could do, since the whole thing is duplicated for weaving and moving but with different variables.
			bool
				engageCheck = IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetMovementMelee) && moving,
				holdOneEngageCharge = IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetMovementMeleeHoldOne),
				engageEarly = IsEnabled(CustomComboPreset.RedMageSmartcastSingleTargetMovementMeleeFirst);
			uint alt = noCastingSubCheck(level, engageCheck, holdOneEngageCharge, engageEarly, targeting && isClose, accelMove);
			if (alt > 0)
				return alt;
		}

		// Stand fast, slow cast!

		if (verfireUp && verstoneUp) {

			// Decide by mana levels
			if (black < white || Math.Min(100, white + procDelta) == black)
				return RDM.Verfire;

			if (white < black || Math.Min(100, black + procDelta) == white)
				return RDM.Verstone;

			// If mana levels are equal, prioritise the original button
			return actionID;
		}

		// Only use Verfire if it won't imbalance us
		if (verfireUp && black + procDelta <= blackThreshold)
			return RDM.Verfire;

		// Only use Verstone if it won't imbalance us
		if (verstoneUp && white + procDelta <= whiteThreshold)
			return RDM.Verstone;

		// If there's NOTHING up right to use, should we override with Accleration (or Swiftcast)?
		if (allowAccel && !accelNoNormal) {
			uint alt = noCastingSubCheck(level, false, false, false, false, allowAccel);
			if (alt > 0)
				return alt;
		}

		// Finally, if all else fails, become Jolt (II[I]), or Grand Impact too I guess
		return OriginalHook(RDM.Jolt);
	}
}

internal class RedMageAcceleration: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.RedMageAccelerationSwiftcast;
	public override uint[] ActionIDs => [RDM.Acceleration];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= RDM.Levels.Acceleration) {
			bool canAccelerate = CanUse(RDM.Acceleration);
			bool canSwiftcast = CanUse(Common.Swiftcast);

			if (IsEnabled(CustomComboPreset.RedMageAccelerationSwiftcastFirst) && canAccelerate && canSwiftcast)
				return Common.Swiftcast;

			if (canAccelerate)
				return RDM.Acceleration;

			if (canSwiftcast)
				return Common.Swiftcast;

			return RDM.Acceleration;
		}

		return Common.Swiftcast;
	}
}

internal class RedMageManafication: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.RedMageManaficationIntoMelee;
	public override uint[] ActionIDs => [RDM.Manafication];

	protected override uint Invoke(uint actionID, uint lastComboActionId, float comboTime, byte level) {
		bool melee = SelfHasEffect(RDM.Buffs.MagickedSwordplay)
			|| lastComboActionId is RDM.Riposte or RDM.EnchantedRiposte or RDM.Zwerchhau or RDM.EnchantedZwerchhau;
		if (IsEnabled(CustomComboPreset.RedMageManaficationIntoMeleeGauge) && !melee) {
			RDMGauge gauge = GetJobGauge<RDMGauge>();
			byte black = gauge.BlackMana;
			byte white = gauge.WhiteMana;
			if ((black >= 50 && white >= 50 && black != white) || black == 100)
				melee = true;
		}

		if (melee) {
			if (IsEnabled(CustomComboPreset.RedMageMeleeComboCloser) && HasTarget && !InMeleeRange)
				return RDM.Corpsacorps;

			if (lastComboActionId is RDM.Zwerchhau or RDM.EnchantedZwerchhau)
				return RDM.EnchantedRedoublement;
			if (lastComboActionId is RDM.Riposte or RDM.EnchantedRiposte)
				return RDM.EnchantedZwerchhau;

			return RDM.EnchantedRiposte;
		}

		if (IsEnabled(CustomComboPreset.RedMageManaficationIntoMeleeFinisherFollowup))
			RDM.CheckFinishers(ref actionID, lastComboActionId, level);

		return actionID;
	}
}

internal class RedMageGapControl: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.RdmAny;
	public override uint[] ActionIDs => [RDM.Corpsacorps, RDM.Displacement];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
		if (level < RDM.Levels.Displacement || !HasTarget)
			return actionID;

		if (IsEnabled(CustomComboPreset.RedMageMeleeGapReverserBackstep) || IsEnabled(CustomComboPreset.RedMageMeleeGapReverserLunge))
			return InMeleeRange ? RDM.Displacement : RDM.Corpsacorps;

		return actionID;
	}
}
