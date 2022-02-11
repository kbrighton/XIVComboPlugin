using System;

using Dalamud.Utility;

using XIVCombo.Combos;

using XIVComboExpandedPlugin.Combos;

using XIVComboVX.Attributes;
using XIVComboVX.Combos;

namespace XIVComboVX {
	public enum CustomComboPreset {
		// ====================================================================================
		#region Universal
		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", 0)]
		AdvAny = 0,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", AST.JobID)]
		AstAny = AdvAny + AST.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", BLM.JobID)]
		BlmAny = AdvAny + BLM.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", BRD.JobID)]
		BrdAny = AdvAny + BRD.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", DNC.JobID)]
		DncAny = AdvAny + DNC.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", DRG.JobID)]
		DrgAny = AdvAny + DRG.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", DRK.JobID)]
		DrkAny = AdvAny + DRK.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", GNB.JobID)]
		GnbAny = AdvAny + GNB.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", MCH.JobID)]
		MchAny = AdvAny + MCH.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", MNK.JobID)]
		MnkAny = AdvAny + MNK.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", NIN.JobID)]
		NinAny = AdvAny + NIN.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", PLD.JobID)]
		PldAny = AdvAny + PLD.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", RDM.JobID)]
		RdmAny = AdvAny + RDM.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", RPR.JobID)]
		RprAny = AdvAny + RPR.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", SAM.JobID)]
		SamAny = AdvAny + SAM.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", SCH.JobID)]
		SchAny = AdvAny + SCH.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", SGE.JobID)]
		SgeAny = AdvAny + SGE.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", SMN.JobID)]
		SmnAny = AdvAny + SMN.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", WAR.JobID)]
		WarAny = AdvAny + WAR.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", WHM.JobID)]
		WhmAny = AdvAny + WHM.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", DOH.JobID)]
		DohAny = AdvAny + DOH.JobID,

		[CustomComboInfo("Any", "This should not be displayed. This always returns true when used with IsEnabled.", DOL.JobID)]
		DolAny = AdvAny + DOL.JobID,
		#endregion
		// ====================================================================================
		#region ASTROLOGIAN (33xx)

		[CustomComboInfo("Swiftcast Ascend", "Ascend turns into Swiftcast when available and reasonable.", AST.JobID)]
		AstrologianSwiftcastRaiserFeature = 3300,

		[CustomComboInfo("Draw on Play", "Play turns into Draw when no card is drawn, as well as the usual Play behavior.", AST.JobID)]
		AstrologianDrawOnPlayFeature = 3301,

		[CustomComboInfo("Astrodyne on Play", "Play turns into Astrodyne when seals are full.", AST.JobID)]
		AstrologianAstrodynePlayFeature = 3304,

		[CustomComboInfo("Minor Arcana Play Feature", "Changes Minor Arcana to Crown Play when a card drawn.", AST.JobID)]
		AstrologianMinorArcanaPlayFeature = 3302,

		[CustomComboInfo("Benefic 2 to Benefic Level Sync", "Changes Benefic 2 to Benefic when below level 26.", AST.JobID)]
		AstrologianBeneficFeature = 3303,

		#endregion
		// ====================================================================================
		#region BLACK MAGE (25xx)

		[CustomComboInfo("Enochian Feature", "Change Fire 4 or Blizzard 4 to whichever action you can currently use.", BLM.JobID)]
		BlackEnochianFeature = 2500,

		[CustomComboInfo("Umbral Soul/Transpose Switcher", "Change Transpose into Umbral Soul when Umbral Soul is usable.", BLM.JobID)]
		BlackManaFeature = 2501,

		[CustomComboInfo("(Between the) Ley Lines", "Change Ley Lines into BTL when Ley Lines is active.", BLM.JobID)]
		BlackLeyLinesFeature = 2502,

		[CustomComboInfo("Fire 1/3 Astral Feature", "Fire 1 becomes Fire 3 outside of Astral Fire.", BLM.JobID)]
		BlackFireAstralFeature = 2503,

		[CustomComboInfo("Fire 1/3 Proc Feature", "Fire 1 becomes Fire 3 when Firestarter proc is up.", BLM.JobID)]
		BlackFireProcFeature = 2509,

		[CustomComboInfo("Blizzard 1/3 Feature", "Blizzard 1 becomes Blizzard 3 when out of Umbral Ice.", BLM.JobID)]
		BlackBlizzardFeature = 2504,

		[CustomComboInfo("Freeze/Flare Feature", "Freeze and Flare become whichever action you can currently use.", BLM.JobID)]
		BlackFreezeFlareFeature = 2505,

		[CustomComboInfo("Fire 2 Feature", "(High) Fire 2 becomes Flare in Astral Fire with 1 or fewer Umbral Hearts.", BLM.JobID)]
		BlackFire2Feature = 2507,

		[CustomComboInfo("Ice 2 Feature", "(High) Blizzard 2 becomes Freeze in Umbral Ice.", BLM.JobID)]
		BlackBlizzard2Feature = 2508,

		[CustomComboInfo("Scathe/Xenoglossy Feature", "Scathe becomes Xenoglossy when available.", BLM.JobID)]
		BlackScatheFeature = 2506,

		#endregion
		// ====================================================================================
		#region BARD (23xx)

		[CustomComboInfo("Wanderer's into Pitch Perfect", "Replaces Wanderer's Minuet with Pitch Perfect while in WM.", BRD.JobID)]
		BardWanderersPitchPerfectFeature = 2300,

		[CustomComboInfo("Heavy Shot into Straight Shot", "Replaces Heavy Shot/Burst Shot with Straight Shot/Refulgent Arrow when procced.", BRD.JobID)]
		BardStraightShotUpgradeFeature = 2301,

		[CustomComboInfo("Iron Jaws Feature", "Iron Jaws is replaced with Caustic Bite/Stormbite if one or both are not up.\nAlternates between the two if Iron Jaws isn't available.", BRD.JobID)]
		BardIronJawsFeature = 2302,

		[CustomComboInfo("Apex Arrow Feature", "Replaces Heavy Shot, Burst Shot, Quick Nock, and Ladonsbit\nwith Blast Arrow when available, or Apex Arrow if gauge is full.", BRD.JobID)]
		BardApexFeature = 2303,

		[CustomComboInfo("Quick Nock / Ladonsbite into Shadowbite", "Replaces Quick Nock and Ladonsbite with Shadowbite when available.", BRD.JobID)]
		BardShadowbiteFeature = 2304,

		[CustomComboInfo("Bloodletter Feature", "Replaces Bloodletter with Empyreal Arrow and Sidewinder depending on which is available.", BRD.JobID)]
		BardBloodletterFeature = 2305,

		[CustomComboInfo("Rain of Death Feature", "Replaces Rain of Death with Empyreal Arrow and Sidewinder depending on which is available.", BRD.JobID)]
		BardRainOfDeathFeature = 2306,

		#endregion
		// ====================================================================================
		#region DANCER (38xx)

		[CustomComboInfo("Single Target Multibutton", "Change Cascade into procs and combos as available.", DNC.JobID)]
		DancerSingleTargetMultibutton = 3800,

		[CustomComboInfo("AoE Multibutton", "Change Windmill into procs and combos as available.", DNC.JobID)]
		DancerAoeMultibutton = 3801,

		[CustomComboInfo("Flourish Proc Saver", "Change Flourish into any available procs before using.", DNC.JobID)]
		DancerFlourishFeature = 3804,

		[ParentPreset(DancerFlourishFeature)]
		[Conflicts(DancerFlourishCooldownFeature)]
		[CustomComboInfo("Only for Fan Dance 4", "Only change Flourish into Fan Dance 4, not the other procs.", DNC.JobID)]
		DancerFlourishLimitedFeature = 3810,

		[ParentPreset(DancerFlourishFeature)]
		[Conflicts(DancerFlourishLimitedFeature)]
		[CustomComboInfo("Only when off CD", "Only change Flourish into procs (other than Fan Dance 4) when Flourish is off CD.", DNC.JobID)]
		DancerFlourishCooldownFeature = 3811,

		[Conflicts(DancerDanceComboCompatibility)]
		[CustomComboInfo("Dance Step Combo", "Change Standard Step and Technical Step into each dance step while dancing.", DNC.JobID)]
		DancerDanceStepCombo = 3805,

		[CustomComboInfo("Devilment Feature", "Change Devilment into Starfall Dance after use.", DNC.JobID)]
		DancerDevilmentFeature = 3807,

		// [CustomComboInfo("Fan Dance Switchers", "Change Fan Dance 1/2 into Fan Dance 3/4 based on the below combos.", DNC.JobID)]
		// DancerFanDanceSwitcher = 3810,

		[CustomComboInfo("Fan Dance 1/3 Combo", "Change Fan Dance 1 into Fan Dance 3 when available.", DNC.JobID)]
		DancerFanDance13Combo = 3802,

		[CustomComboInfo("Fan Dance 1/4 Combo", "Change Fan Dance 1 into Fan Dance 4 when available.", DNC.JobID)]
		DancerFanDance14Combo = 3808,

		[CustomComboInfo("Fan Dance 2/3 Combo", "Change Fan Dance 2 into Fan Dance 3 when available.", DNC.JobID)]
		DancerFanDance23Combo = 3803,

		[CustomComboInfo("Fan Dance 2/4 Combo", "Change Fan Dance 2 into Fan Dance 4 when available.", DNC.JobID)]
		DancerFanDance24Combo = 3809,

		[Conflicts(DancerDanceStepCombo)]
		[CustomComboInfo("Dance Step Feature", "Change custom actions into dance steps while dancing." +
			"\nThe defaults are Cascade, Flourish, Fan Dance and Fan Dance II. If set to 0, they will reset to these actions." +
			"\nYou can get Action IDs with Garland Tools by searching for the action and clicking the cog.", DNC.JobID)]
		DancerDanceComboCompatibility = 3806,

		#endregion
		// ====================================================================================
		#region DRAGOON (22xx)

		[CustomComboInfo("Coerthan Torment Combo", "Replace Coerthan Torment with its combo chain.", DRG.JobID)]
		DragoonCoerthanTormentCombo = 2200,

		[CustomComboInfo("Chaos Thrust Combo", "Replace Chaos Thrust with its combo chain.", DRG.JobID)]
		DragoonChaosThrustCombo = 2201,

		[ParentPreset(DragoonChaosThrustCombo)]
		[CustomComboInfo("Chaos Thrust from Disembowl", "Start the Chaos Thrust combo chain with Disembowl instead of True Thrust.", DRG.JobID)]
		DragoonChaosThrustLateOption = 2207,

		[CustomComboInfo("Full Thrust Combo", "Replace Full Thrust with its combo chain.", DRG.JobID)]
		DragoonFullThrustCombo = 2202,

		[ParentPreset(DragoonFullThrustCombo)]
		[CustomComboInfo("Full Thrust from Vorpal", "Start the Full Thrust combo chain with Vorpal Thrust instead of True Thrust.", DRG.JobID)]
		DragoonFullThrustLateOption = 2208,

		[CustomComboInfo("Jump + Mirage Dive", "Replace (High) Jump with Mirage Dive when Dive Ready.", DRG.JobID)]
		DragoonJumpFeature = 2203,

		[CustomComboInfo("Wheeling Thrust / Fang and Claw Option", "When you have either Enhanced Fang and Claw or Wheeling Thrust, Chaos Thrust becomes Wheeling Thrust and Full Thrust becomes Fang and Claw.", DRG.JobID)]
		DragoonFangThrustFeature = 2206,

		[Experimental]
		[CustomComboInfo("Dive Dive Dive!", "Replace Spineshatter Dive, Dragonfire Dive, and Stardiver with whichever is available.", DRG.JobID)]
		DragoonDiveFeature = 2205,

		#endregion
		// ====================================================================================
		#region DARK KNIGHT (32xx)

		[CustomComboInfo("Souleater Combo", "Replace Souleater with its combo chain.", DRK.JobID)]
		DarkSouleaterCombo = 3200,

		[CustomComboInfo("Stalwart Soul Combo", "Replace Stalwart Soul with its combo chain.", DRK.JobID)]
		DarkStalwartSoulCombo = 3201,

		[CustomComboInfo("Delirium Feature", "Replace Souleater and Stalwart Soul with Bloodspiller and Quietus when Delirium is active.", DRK.JobID)]
		DarkDeliriumFeature = 3202,

		[CustomComboInfo("Dark Knight Gauge Overcap Saver", "Replace AoE combo with gauge spender if you are about to overcap.", DRK.JobID)]
		DarkOvercapFeature = 3203,

		[Experimental]
		[CustomComboInfo("Shadows Galore", "Replace Flood and Edge of Darkness with Shadowbringer when under Darkside with less than 6000 MP left.", DRK.JobID)]
		DarkShadowbringerFeature = 3204,

		[CustomComboInfo("Stun/Interrupt Feature", "Turn Low Blow and Interject into Interject when off CD and your target can be interrupted, Low Blow otherwise.", DRK.JobID)]
		DarkStunInterruptFeature = 3205,

		#endregion
		// ====================================================================================
		#region GUNBREAKER (37xx)

		[CustomComboInfo("Solid Barrel Combo", "Replace Solid Barrel with its combo chain.", GNB.JobID)]
		GunbreakerSolidBarrelCombo = 3700,

		[CustomComboInfo("Gnashing Fang Continuation", "Replace Gnashing Fang with Continuation moves when appropriate.", GNB.JobID)]
		GunbreakerGnashingFangCont = 3702,

		[CustomComboInfo("Demon Slaughter Combo", "Replace Demon Slaughter with its combo chain.", GNB.JobID)]
		GunbreakerDemonSlaughterCombo = 3703,

		[CustomComboInfo("Fated Circle Feature", "In addition to the Demon Slaughter combo, add Fated Circle when charges are full.", GNB.JobID)]
		GunbreakerFatedCircleFeature = 3704,

		[CustomComboInfo("Empty Bloodfest Feature", "Replace Burst Strike and Fated Circle with Bloodfest if the powder gauge is empty.", GNB.JobID)]
		GunbreakerEmptyBloodfestFeature = 3705,

		[ParentPreset(GunbreakerEmptyBloodfestFeature)]
		[CustomComboInfo("Burst Strike Continuation", "Replace Burst Strike with Continuation moves when appropriate.", GNB.JobID)]
		GunbreakerBurstStrikeCont = 3708,

		[CustomComboInfo("No Mercy Feature", "Replace No Mercy with Bow Shock, and then Sonic Break, while No Mercy is active.", GNB.JobID)]
		GunbreakerNoMercyFeature = 3706,

		[CustomComboInfo("Bow Shock / Sonic Break Swap", "Replace Bow Shock and Sonic Break with one or the other, depending on which is on cooldown.", GNB.JobID)]
		GunbreakerBowShockSonicBreakFeature = 3707,

		[CustomComboInfo("Double Down Feature", "Replace Burst Strike and Fated Circle with Double Down when available.", GNB.JobID)]
		GunbreakerDoubleDownFeature = 3709,

		[CustomComboInfo("Stun/Interrupt Feature", "Turn Low Blow and Interject into Interject when off CD and your target can be interrupted, Low Blow otherwise.", GNB.JobID)]
		GunbreakerStunInterruptFeature = 3710,

		#endregion
		// ====================================================================================
		#region MACHINIST (31xx)

		[CustomComboInfo("(Heated) Shot Combo", "Replace either form of Clean Shot with its combo chain.", MCH.JobID)]
		MachinistMainCombo = 3100,

		[CustomComboInfo("Spread Shot Heat", "Replace Spread Shot with Auto Crossbow when overheated.", MCH.JobID)]
		MachinistSpreadShotFeature = 3101,

		[CustomComboInfo("Hypercharge Feature", "Replace Heat Blast and Auto Crossbow with Hypercharge when not overheated.", MCH.JobID)]
		MachinistOverheatFeature = 3102,

		[CustomComboInfo("Overdrive Feature", "Replace Rook Autoturret and Automaton Queen with their respective Overdrive while active.", MCH.JobID)]
		MachinistOverdriveFeature = 3103,

		[CustomComboInfo("Gauss Round / Ricochet Feature", "Replace Gauss Round and Ricochet with one or the other depending on which has less recharge time left.", MCH.JobID)]
		MachinistGaussRoundRicochetFeature = 3104,

		[CustomComboInfo("Hot Shot / Air Anchor / Drill Feature", "Replace Hot Shot (Air Anchor) and Drill with whichever is available.", MCH.JobID)]
		MachinistDrillAirAnchorFeature = 3105,

		[ParentPreset(MachinistDrillAirAnchorFeature)]
		[CustomComboInfo("HS/AA/D + Chain Saw Feature", "Also include Chain Saw in the above.", MCH.JobID)]
		MachinistDrillAirAnchorPlusFeature = 3106,

		#endregion
		// ====================================================================================
		#region MONK (20xx)

		[CustomComboInfo("Monk AoE Combo", "Replaces Rockbreaker with the AoE combo chain.", MNK.JobID)]
		MonkAoECombo = 2000,

		[CustomComboInfo("Dragon Kick to Bootshine Feature", "Replaces Dragon Kick with Bootshine if Leaden Fist is up.", MNK.JobID)]
		MonkDragonBootshineFeature = 2001,

		[CustomComboInfo("Dragon Kick to Masterful Blitz Feature", "Replaces Dragon Kick with Masterful Blitz if you have three Beast Chakra.", MNK.JobID)]
		MonkDragonBlitzFeature = 2012,

		[CustomComboInfo("Twin Snakes to True Strike Feature", "Replaces Twin Snakes with True Strike if Disciplined Fist is up.", MNK.JobID)]
		MonkTwinStrikeFeature = 2010,

		[CustomComboInfo("Demolish to Snap Punch Feature", "Replaces Demolish with Snap Punch if target is under Demolish.", MNK.JobID)]
		MonkDemolishSnapFeature = 2011,

		[CustomComboInfo("Howling Fist / Meditation Feature", "Replaces Howling Fist with Meditation when the Fifth Chakra is not open.", MNK.JobID)]
		MonkHowlingFistMeditationFeature = 2002,

		[CustomComboInfo("Disciplined AoE Feature", "Replace Rockbreaker with Four Point Fury while Formless Fist is active.", MNK.JobID)]
		MonkAoEDisciplinedFeature = 2007,

		[CustomComboInfo("Lunar AoE Feature", "Replace Rockbreaker with Shadow of the Destroyer (or Rockbreaker depending on level) when Perfect Balance is active and the Lunar Nadi is missing.", MNK.JobID)]
		MonkAoELunarFeature = 2006,

		[CustomComboInfo("Solar AoE Feature", "Replace Rockbreaker with whatever is necessary to acquire missing Beast Chakra when Perfect Balance is active and the Solar Nadi is missing.", MNK.JobID)]
		MonkAoESolarFeature = 2005,

		[CustomComboInfo("AoE Balance Feature", "Replaces Rockbreaker with Masterful Blitz if you have 3 Beast Chakra.", MNK.JobID)]
		MonkAoEBalanceFeature = 2009,

		[CustomComboInfo("Four Point Fury AoE Feature", "Replace Four Point Fury with Shadow of the Destroyer (or Rockbreaker depending on level) when Perfect Balance is active.", MNK.JobID)]
		MonkAoEFpfFeature = 2008,

		[CustomComboInfo("Perfect Balance Feature", "Replace Perfect Balance with Masterful Blitz when you have 3 Beast Chakra.", MNK.JobID)]
		MonkPerfectBalanceFeature = 2004,

		[CustomComboInfo("Riddle of Brotherly Fire", "Replace Riddle of Fire with Brotherhood if the former is on CD and the latter isn't.", MNK.JobID)]
		MonkBrotherlyFireFeature = 2013,

		[CustomComboInfo("Riddle of Fire and Wind", "Replace Riddle of Fire with Riddle of Wind if the former is on CD and the latter isn't.", MNK.JobID)]
		MonkFireWindFeature = 2014,

		#endregion
		// ====================================================================================
		#region NINJA (30xx)

		[CustomComboInfo("Armor Crush Combo", "Replace Armor Crush with its combo chain.", NIN.JobID)]
		NinjaArmorCrushCombo = 3000,

		[ParentPreset(NinjaArmorCrushCombo)]
		[Conflicts(NinjaArmorCrushForkedRaijuFeature)]
		[CustomComboInfo("Fleeting Crush Feature", "Replaces the Armor Crush combo with Fleeting Raiju when available.", NIN.JobID)]
		NinjaArmorCrushFleetingRaijuFeature = 3010,

		[ParentPreset(NinjaArmorCrushCombo)]
		[Conflicts(NinjaArmorCrushFleetingRaijuFeature)]
		[CustomComboInfo("Forked Crush Feature", "Replaces the Armor Crush combo with Forked Raiju when available.", NIN.JobID)]
		NinjaArmorCrushForkedRaijuFeature = 3017,

		[CustomComboInfo("Aeolian Edge Combo", "Replace Aeolian Edge with its combo chain.", NIN.JobID)]
		NinjaAeolianEdgeCombo = 3001,

		[ParentPreset(NinjaAeolianEdgeCombo)]
		[Conflicts(NinjaAeolianEdgeForkedRaijuFeature)]
		[CustomComboInfo("Fleeting Edge Feature", "Replaces the Aeolian Edge combo with Fleeting Raiju when available.", NIN.JobID)]
		NinjaAeolianEdgeFleetingRaijuFeature = 3011,

		[ParentPreset(NinjaArmorCrushCombo)]
		[Conflicts(NinjaAeolianEdgeFleetingRaijuFeature)]
		[CustomComboInfo("Forked Edge Feature", "Replaces the Aeolian Edge combo with Forked Raiju when available.", NIN.JobID)]
		NinjaAeolianEdgeForkedRaijuFeature = 3016,

		[CustomComboInfo("Aeolian Edge / Huton Feature", "Replaces Aeolian Edge with Armor Crush when Huton has less than 30 seconds remaining and Huraijin when missing.", NIN.JobID)]
		NinjaAeolianEdgeHutonFeature = 3014,

		[CustomComboInfo("Hakke Mujinsatsu Combo", "Replace Hakke Mujinsatsu with its combo chain.", NIN.JobID)]
		NinjaHakkeMujinsatsuCombo = 3002,

		[CustomComboInfo("Smart Hide", "Replaces Hide with Trick Attack while under the effect of Suiton or Hidden, or else Mug if in combat.", NIN.JobID)]
		NinjaHideMugFeature = 3004,

		[CustomComboInfo("GCDs to Ninjutsu Feature", "Every GCD combo becomes Ninjutsu while Mudras are being used.", NIN.JobID)]
		NinjaGCDNinjutsuFeature = 3005,

		[CustomComboInfo("Kassatsu to Trick", "Replaces Kassatsu with Trick Attack while Suiton or Hidden is up.\nCooldown tracking plugin recommended.", NIN.JobID)]
		NinjaKassatsuTrickFeature = 3006,

		[CustomComboInfo("Ten Chi Jin to Meisui", "Replaces Ten Chi Jin (the move) with Meisui while Suiton is up.\nCooldown tracking plugin recommended.", NIN.JobID)]
		NinjaTCJMeisuiFeature = 3007,

		[CustomComboInfo("Kassatsu Chi/Jin Feature", "Replaces Chi with Jin while Kassatsu is up if you have Enhanced Kassatsu.", NIN.JobID)]
		NinjaKassatsuChiJinFeature = 3008,

		[Conflicts(NinjaHuraijinFleetingRaijuFeature)]
		[CustomComboInfo("Forked Huraijin Feature", "Replaces Huraijin with Forked Raiju when available.", NIN.JobID)]
		NinjaHuraijinForkedRaijuFeature = 3012,

		[Conflicts(NinjaHuraijinForkedRaijuFeature)]
		[CustomComboInfo("Fleeting Huraijin Feature", "Replaces Huraijin with Fleeting Raiju when available.", NIN.JobID)]
		NinjaHuraijinFleetingRaijuFeature = 3015,

		[CustomComboInfo("Huraijin / Crush Feature", "Replaces Huraijin with Armor Crush after Gust Slash.", NIN.JobID)]
		NinjaHuraijinCrushFeature = 3013,

		#endregion
		// ====================================================================================
		#region PALADIN (19xx)

		[CustomComboInfo("Goring Blade Combo", "Replace Goring Blade with its combo chain.", PLD.JobID)]
		PaladinGoringBladeCombo = 1900,

		[CustomComboInfo("Royal Authority Combo", "Replace Royal Authority/Rage of Halone with its combo chain.", PLD.JobID)]
		PaladinRoyalAuthorityCombo = 1901,

		[CustomComboInfo("Atonement Feature", "Replace Royal Authority and Goring Blade with Atonement when under the effect of Sword Oath.", PLD.JobID)]
		PaladinAtonementFeature = 1902,

		[CustomComboInfo("Prominence Combo", "Replace Prominence with its combo chain.", PLD.JobID)]
		PaladinProminenceCombo = 1903,

		[CustomComboInfo("Requiescat Confiteor", "Replace Requiescat with Confiteor while under the effect of Requiescat.\nReplace Requiescat with Confiteor's combos while chaining.", PLD.JobID)]
		PaladinRequiescatConfiteorCombo = 1904,

		[CustomComboInfo("Requiescat Feature", "Replace Royal Authority/Goring Blade with Holy Spirit, and Prominence with Holy Circle, while Requiescat is active.", PLD.JobID)]
		PaladinRequiescatFeature = 1905,

		[CustomComboInfo("Intervene Level Sync", "Replace Intervene with Shield Lob when under level.", PLD.JobID)]
		PaladinInterveneSyncFeature = 1906,

		[CustomComboInfo("Stun/Interrupt Feature", "Turn Low Blow and Interject into Interject when off CD and your target can be interrupted, Low Blow otherwise.", PLD.JobID)]
		PaladinStunInterruptFeature = 1907,

		#endregion
		// ====================================================================================
		#region RED MAGE (35xx)

		[CustomComboInfo("Swiftcast Verraise", "Verraise turns into Swiftcast when available and reasonable.", RDM.JobID)]
		RedMageSwiftcastRaiserFeature = 3500,

		[CustomComboInfo("Redoublement combo", "Replaces Redoublement with its combo chain, following enchantment rules.", RDM.JobID)]
		RedMageMeleeCombo = 3502,

		[ParentPreset(RedMageMeleeCombo)]
		[CustomComboInfo("Redoublement Combo Plus", "Replaces Redoublement (and Moulinet) with Verflare/Verholy (and then Scorch and Resolution) after 3 mana stacks, whichever is more appropriate.", RDM.JobID)]
		RedMageMeleeComboPlus = 3503,

		[Conflicts(RedMageSmartcastAoEFeature)]
		[CustomComboInfo("Red Mage AoE Combo", "Replaces Veraero/Verthunder 2 with Impact when under a cast speeder.", RDM.JobID)]
		RedMageAoECombo = 3501,

		[Conflicts(RedMageSmartcastSingleFeature)]
		[CustomComboInfo("Verproc into Jolt", "Replaces Verstone/Verfire with Jolt (2) when no proc is available.", RDM.JobID)]
		RedMageVerprocCombo = 3504,

		[ParentPreset(RedMageVerprocCombo)]
		[CustomComboInfo("Verproc into Jolt Plus", "Additionally replaces Verstone/Verfire with Veraero/Verthunder if fastcasting are up.", RDM.JobID)]
		RedMageVerprocComboPlus = 3505,

		[ParentPreset(RedMageVerprocComboPlus)]
		[CustomComboInfo("Verproc into Jolt Plus Veraero Opener", "Turns Verstone into Veraero when out of combat.", RDM.JobID)]
		RedMageVeraeroOpenerFeature = 3506,

		[ParentPreset(RedMageVerprocComboPlus)]
		[CustomComboInfo("Verproc into Jolt Plus Verthunder Opener", "Turns Verfire into Verthunder when out of combat.", RDM.JobID)]
		RedMageVerthunderOpenerFeature = 3507,

		[CustomComboInfo("Contre Sixte / Fleche Feature", "Turns Contre Sixte and Fleche into whichever is available.", RDM.JobID)]
		RedMageContreFlecheFeature = 3510,

		[Conflicts(RedMageAoECombo)]
		[CustomComboInfo("Smartcast AoE", "Dynamically replaces Veraero/Verthunder 2 with the appropriate spell based on your job gauge.\nIncludes Impact/Scatter when fastcasting.", RDM.JobID)]
		RedMageSmartcastAoEFeature = 3508,

		[Conflicts(RedMageVerprocCombo)]
		[CustomComboInfo("Smartcast Single Target", "Dynamically replaces Verstone/Verfire with the appropriate spell based on your job gauge.\nVeraero and Verthunder are replaced with one or the other accordingly, for openers.", RDM.JobID)]
		RedMageSmartcastSingleFeature = 3509,

		#endregion
		// ====================================================================================
		#region REAPER (39xx)

		[CustomComboInfo("Slice Combo", "Replace Infernal Slice with its combo chain.", RPR.JobID)]
		ReaperSliceCombo = 3901,

		[Conflicts(ReaperSliceGallowsFeature)]
		[CustomComboInfo("Slice Gibbet Feature", "Replace Infernal Slice with Gibbet while Reaving or Enshrouded.", RPR.JobID)]
		ReaperSliceGibbetFeature = 3903,

		[Conflicts(ReaperSliceGibbetFeature)]
		[CustomComboInfo("Slice Gallows Feature", "Replace Infernal Slice with Gallows while Reaving or Enshrouded.", RPR.JobID)]
		ReaperSliceGallowsFeature = 3904,

		[CustomComboInfo("Slice Enhanced Soul Reaver Feature", "Replace Infernal Slice with whichever of Gibbet or Gallows is currently enhanced while Reaving.", RPR.JobID)]
		ReaperSliceEnhancedSoulReaverFeature = 3913,

		[CustomComboInfo("Slice Enhanced Enshrouded Feature", "Replace Infernal Slice with whichever of Gibbet or Gallows is currently enhanced while Enshrouded.", RPR.JobID)]
		ReaperSliceEnhancedEnshroudedFeature = 3914,

		[CustomComboInfo("Slice Lemure's Feature", "Replace Infernal Slice with Lemure's Slice when two or more stacks of Void Shroud are active.", RPR.JobID)]
		ReaperSliceLemuresFeature = 3919,

		[CustomComboInfo("Slice Communio Feature", "Replace Infernal Slice with Communio when one stack of Shroud is left.", RPR.JobID)]
		ReaperSliceCommunioFeature = 3920,

		[Conflicts(ReaperShadowGibbetFeature)]
		[CustomComboInfo("Shadow Gallows Feature", "Replace Shadow of Death with Gallows while Reaving or Enshrouded.", RPR.JobID)]
		ReaperShadowGallowsFeature = 3905,

		[Conflicts(ReaperShadowGallowsFeature)]
		[CustomComboInfo("Shadow Gibbet Feature", "Replace Shadow of Death with Gibbet while Reaving or Enshrouded.", RPR.JobID)]
		ReaperShadowGibbetFeature = 3906,

		[CustomComboInfo("Shadow Lemure's Feature", "Replace Shadow of Death with Lemure's Slice when two or more stacks of Void Shroud are active.", RPR.JobID)]
		ReaperShadowLemuresFeature = 3923,

		[CustomComboInfo("Shadow Communio Feature", "Replace Shadow of Death with Communio when one stack of Shroud is left.", RPR.JobID)]
		ReaperShadowCommunioFeature = 3924,

		[Conflicts(ReaperSoulGibbetFeature)]
		[CustomComboInfo("Soul Gallows Feature", "Replace Soul Slice with Gallows while Reaving or Enshrouded.", RPR.JobID)]
		ReaperSoulGallowsFeature = 3925,

		[Conflicts(ReaperSoulGallowsFeature)]
		[CustomComboInfo("Soul Gibbet Feature", "Replace Soul Slice with Gibbet while Reaving or Enshrouded.", RPR.JobID)]
		ReaperSoulGibbetFeature = 3926,

		[CustomComboInfo("Soul Lemure's Feature", "Replace Soul Slice with Lemure's Slice when two or more stacks of Void Shroud are active.", RPR.JobID)]
		ReaperSoulLemuresFeature = 3927,

		[CustomComboInfo("Soul Communio Feature", "Replace Soul Slice with Communio when one stack of Shroud is left.", RPR.JobID)]
		ReaperSoulCommunioFeature = 3928,

		[CustomComboInfo("Scythe Combo", "Replace Nightmare Scythe with its combo chain.", RPR.JobID)]
		ReaperScytheCombo = 3902,

		[CustomComboInfo("Scythe Guillotine Feature", "Replace Nightmare Scythe with Guillotine while Reaving or Enshrouded.", RPR.JobID)]
		ReaperScytheGuillotineFeature = 3907,

		[CustomComboInfo("Scythe Lemure's Feature", "Replace Nightmare Scythe with Lemure's Slice when two or more stacks of Void Shroud are active.", RPR.JobID)]
		ReaperScytheLemuresFeature = 3921,

		[CustomComboInfo("Scythe Communio Feature", "Replace Nightmare Scythe with Communio when one stack is left of Shroud.", RPR.JobID)]
		ReaperScytheCommunioFeature = 3922,

		[CustomComboInfo("Enhanced Soul Reaver Feature", "Replace Gibbet and Gallows with whichever is currently enhanced while Reaving.", RPR.JobID)]
		ReaperEnhancedSoulReaverFeature = 3917,

		[CustomComboInfo("Enhanced Enshrouded Feature", "Replace Gibbet and Gallows with whichever is currently enhanced while Enshrouded.", RPR.JobID)]
		ReaperEnhancedEnshroudedFeature = 3918,

		[CustomComboInfo("Lemure's Soul Reaver Feature", "Replace Gibbet, Gallows, and Guillotine with Lemure's Slice or Scythe when two or more stacks of Void Shroud are active.", RPR.JobID)]
		ReaperLemuresSoulReaverFeature = 3911,

		[CustomComboInfo("Communio Soul Reaver Feature", "Replace Gibbet, Gallows, and Guillotine with Communio when one stack is left of Shroud.", RPR.JobID)]
		ReaperCommunioSoulReaverFeature = 3912,

		[CustomComboInfo("Enshroud Communio Feature", "Replace Enshroud with Communio when Enshrouded.", RPR.JobID)]
		ReaperEnshroudCommunioFeature = 3909,

		[CustomComboInfo("Arcane Harvest Feature", "Replace Arcane Circle with Plentiful Harvest when you have stacks of Immortal Sacrifice.", RPR.JobID)]
		ReaperHarvestFeature = 3908,

		[CustomComboInfo("Regress Feature", "Both Hell's Ingress and Egress turn into Regress when Threshold is active, instead of just the opposite of the one used.", RPR.JobID)]
		ReaperRegressFeature = 3910,

		[Experimental]
		[CustomComboInfo("Gluttony on Blood Stalk", "Replaces Blood Stalk with Gluttony when off cooldown.", RPR.JobID)]
		ReaperGluttonyOnBloodStalkFeature = 3929,

		[Experimental]
		[CustomComboInfo("Gluttony on Grim Swathe", "Replaces Grim Swathe with Gluttony when off cooldown.", RPR.JobID)]
		ReaperGluttonyOnGrimSwatheFeature = 3930,

		[Experimental]
		[CustomComboInfo("Gluttony on Unveiled Gibbet", "Replaces Unveiled Gibbet with Gluttony when off cooldown.", RPR.JobID)]
		ReaperGluttonyOnUnveiledGibbetFeature = 3931,

		[Experimental]
		[CustomComboInfo("Gluttony on Unveiled Gallows", "Replaces Unveiled Gallows with Gluttony when off cooldown.", RPR.JobID)]
		ReaperGluttonyOnUnveiledGallowsFeature = 3932,

		#endregion
		// ====================================================================================
		#region SAGE (40xx)

		[CustomComboInfo("Swiftcast Egeiro", "Egeiro turns into Swiftcast when available and reasonable.", SGE.JobID)]
		SageSwiftcastRaiserFeature = 4000,

		[CustomComboInfo("Taurochole Into Druochole Feature", "Replace Taurochole with Druochole when on cooldown", SGE.JobID)]
		SageTaurocholeDruocholeFeature = 4001,

		[CustomComboInfo("Taurochole Into Rhizomata Feature", "Replace Taurochole with Rhizomata when Addersgall is empty.", SGE.JobID)]
		SageTaurocholeRhizomataFeature = 4002,

		[CustomComboInfo("Druochole Into Rhizomata Feature", "Replace Druochole with Rhizomata when Addersgall is empty.", SGE.JobID)]
		SageDruocholeRhizomataFeature = 4003,

		[CustomComboInfo("Ixochole Into Rhizomata Feature", "Replace Ixochole with Rhizomata when Addersgall is empty.", SGE.JobID)]
		SageIxocholeRhizomataFeature = 4004,

		[CustomComboInfo("Kerachole Into Rhizomata Feature", "Replace Kerachole with Rhizomata when Addersgall is empty.", SGE.JobID)]
		SageKeracholaRhizomataFeature = 4005,

		[CustomComboInfo("Soteria Kardia Feature", "Replace Soteria with Kardia when off cooldown and missing Kardion.", SGE.JobID)]
		SageSoteriaKardionFeature = 4006,

		#endregion
		// ====================================================================================
		#region SAMURAI (34xx)

		[CustomComboInfo("Yukikaze Combo", "Replace Yukikaze with its combo chain.", SAM.JobID)]
		SamuraiYukikazeCombo = 3400,

		[CustomComboInfo("Gekko Combo", "Replace Gekko with its combo chain.", SAM.JobID)]
		SamuraiGekkoCombo = 3401,

		[ParentPreset(SamuraiGekkoCombo)]
		[CustomComboInfo("Gekko Combo from Jinpu", "Start the Gekko combo chain with Jinpu instead of Hakaze.", SAM.JobID)]
		SamuraiGekkoOption = 3416,

		[CustomComboInfo("Kasha Combo", "Replace Kasha with its combo chain.", SAM.JobID)]
		SamuraiKashaCombo = 3402,

		[ParentPreset(SamuraiKashaCombo)]
		[CustomComboInfo("Kasha Combo from Shifu", "Start the Kasha combo chain with Shifu instead of Hakaze.", SAM.JobID)]
		SamuraiKashaOption = 3417,

		[CustomComboInfo("Mangetsu Combo", "Replace Mangetsu with its combo chain.", SAM.JobID)]
		SamuraiMangetsuCombo = 3403,

		[CustomComboInfo("Oka Combo", "Replace Oka with its combo chain.", SAM.JobID)]
		SamuraiOkaCombo = 3404,

		[Conflicts(SamuraiIaijutsuTsubameGaeshiFeature)]
		[CustomComboInfo("Tsubame-gaeshi to Iaijutsu", "Replace Tsubame-gaeshi with Iaijutsu when Sen is empty.", SAM.JobID)]
		SamuraiTsubameGaeshiIaijutsuFeature = 3407,

		[Conflicts(SamuraiIaijutsuShohaFeature)]
		[CustomComboInfo("Tsubame-gaeshi to Shoha", "Replace Tsubame-gaeshi with Shoha when meditation is 3.", SAM.JobID)]
		SamuraiTsubameGaeshiShohaFeature = 3409,

		[Conflicts(SamuraiTsubameGaeshiIaijutsuFeature)]
		[CustomComboInfo("Iaijutsu to Tsubame-gaeshi", "Replace Iaijutsu with Tsubame-gaeshi when Sen is not empty.", SAM.JobID)]
		SamuraiIaijutsuTsubameGaeshiFeature = 3408,

		[Conflicts(SamuraiTsubameGaeshiShohaFeature)]
		[CustomComboInfo("Iaijutsu to Shoha", "Replace Iaijutsu with Shoha when meditation is 3.", SAM.JobID)]
		SamuraiIaijutsuShohaFeature = 3410,

		[CustomComboInfo("Shinten to Senei", "Replace Hissatsu: Shinten with Senei when available.", SAM.JobID)]
		SamuraiShintenSeneiFeature = 3414,

		[CustomComboInfo("Shinten to Shoha", "Replace Hissatsu: Shinten with Shoha when Meditation is full.", SAM.JobID)]
		SamuraiShintenShohaFeature = 3413,

		[CustomComboInfo("Kyuten to Guren", "Replace Hissatsu: Kyuten with Guren when available.", SAM.JobID)]
		SamuraiKyutenGurenFeature = 3415,

		[CustomComboInfo("Kyuten to Shoha 2", "Replace Hissatsu: Kyuten with Shoha 2 when Meditation is full.", SAM.JobID)]
		SamuraiKyutenShoha2Feature = 3412,

		[CustomComboInfo("Ikishoten Namikiri Feature", "Replace Ikishoten with Shoha, Kaeshi Namikiri, and then Ogi Namikiri when available.", SAM.JobID)]
		SamuraiIkishotenNamikiriFeature = 3411,

		[CustomComboInfo("Hissatsu Senei/Guren Sync Feature", "Replace Hissatsu Senei with Hissatsu Guren when underlevel.", SAM.JobID)]
		SamuraiGurenSeneiLevelSyncFeature = 3418,

		#endregion
		// ====================================================================================
		#region SCHOLAR (28xx)

		[CustomComboInfo("Swiftcast Resurrection", "Resurrection turns into Swiftcast when available and reasonable.", SCH.JobID)]
		ScholarSwiftcastRaiserFeature = 2800,

		[CustomComboInfo("Seraph Fey Blessing/Consolation", "Change Fey Blessing into Consolation when Seraph is out.", SCH.JobID)]
		ScholarSeraphConsolationFeature = 2801,

		[CustomComboInfo("ED Aetherflow", "Change Energy Drain into Aetherflow when you have no more Aetherflow stacks.", SCH.JobID)]
		ScholarEnergyDrainFeature = 2802,

		[CustomComboInfo("Lustrous Aetherflow", "Change Lustrate into Aetherflow when you have no more Aetherflow stacks.", SCH.JobID)]
		ScholarLustrateAetherflowFeature = 2803,

		[CustomComboInfo("Indomitable Aetherflow", "Change Indomitability into Aetherflow when you have no more Aetherflow stacks.", SCH.JobID)]
		ScholarIndomAetherflowFeature = 2804,

		[CustomComboInfo("Excog / Lustrate", "Change Excogitation into Lustrate when on CD or under level.", SCH.JobID)]
		ScholarExcogFallbackFeature = 2805,

		#endregion
		// ====================================================================================
		#region SUMMONER (27xx)

		[CustomComboInfo("Swiftcast Resurrection", "Resurrection turns into Swiftcast when available and reasonable.", SMN.JobID)]
		SummonerSwiftcastRaiserFeature = 2700,

		[CustomComboInfo("ED Fester", "Change Fester into Energy Drain when out of Aetherflow stacks.", SMN.JobID)]
		SummonerEDFesterCombo = 2704,

		[CustomComboInfo("ES Painflare", "Change Painflare into Energy Syphon when out of Aetherflow stacks.", SMN.JobID)]
		SummonerESPainflareCombo = 2705,

		[CustomComboInfo("Ruin Feature", "Change Ruin into Gemburst when attuned.", SMN.JobID)]
		SummonerRuinFeature = 2706,

		[CustomComboInfo("Titan's Favor Ruin Feature", "Change Ruin into Mountain Buster (oGCD) when available.", SMN.JobID)]
		SummonerRuinTitansFavorFeature = 2713,

		[CustomComboInfo("Further Ruin Feature", "Change Ruin into Ruin4 when available and appropriate.", SMN.JobID)]
		SummonerFurtherRuinFeature = 2708,

		[CustomComboInfo("Outburst Feature", "Change Outburst into Precious Brilliance when attuned.", SMN.JobID)]
		SummonerOutburstFeature = 2707,

		[CustomComboInfo("Titan's Favor Outburst Feature", "Change Outburst into Mountain Buster (oGCD) when available.", SMN.JobID)]
		SummonerOutburstTitansFavorFeature = 2714,

		[CustomComboInfo("Further Outburst Feature", "Change Outburst into Ruin4 when available and appropriate.", SMN.JobID)]
		SummonerFurtherOutburstFeature = 2709,

		[CustomComboInfo("Shiny Titan's Favour", "Change Ruin into Ruin4 when available and appropriate.", SMN.JobID)]
		SummonerShinyTitansFavorFeature = 2710,

		[CustomComboInfo("Further Shiny Feature", "Change Outburst into Ruin4 when available and appropriate.", SMN.JobID)]
		SummonerFurtherShinyFeature = 2711,

		[CustomComboInfo("Shiny Enkindle Feature", "Change Gemshine and Precious Brilliance to Enkindle when Bahamut or Phoenix are summoned.", SMN.JobID)]
		SummonerShinyEnkindleFeature = 2712,

		[CustomComboInfo("Demi Enkindle Feature", "Change Summon Bahamut and Summon Phoenix into Enkindle when Bahamut or Phoenix are summoned.", SMN.JobID)]
		SummonerDemiEnkindleFeature = 2715,

		[CustomComboInfo("Radiant Carbuncle Feature", "Change Radiant Aegis into Summon Carbuncle when no pet has been summoned.", SMN.JobID)]
		SummonerRadiantCarbuncleFeature = 2716,

		[CustomComboInfo("Searing Carbuncle Feature", "Change Searing Light into Summon Carbuncle when no pet has been summoned.", SMN.JobID)]
		SummonerSearingCarbuncleFeature = 2717,

		[CustomComboInfo("Slipstream / Swiftcast Feature", "Change Slipstream into Swiftcast when Swiftcast is available.", SMN.JobID)]
		SummonerSlipcastFeature = 2718,

		#endregion
		// ====================================================================================
		#region WARRIOR (21xx)

		[CustomComboInfo("Storm's Path Combo", "Replace Storm's Path with its combo chain.", WAR.JobID)]
		WarriorStormsPathCombo = 2100,

		[ParentPreset(WarriorStormsPathCombo)]
		[CustomComboInfo("Storm's Path Double Combo", "Replace the Storm's Path combo chain with Storm's Eye if Surging Tempest has less than 7 seconds left.", WAR.JobID)]
		WarriorSmartStormCombo = 2112,

		[CustomComboInfo("Storm's Eye Combo", "Replace Storm's Eye with its combo chain.", WAR.JobID)]
		WarriorStormsEyeCombo = 2101,

		[CustomComboInfo("Mythril Tempest Combo", "Replace Mythril Tempest with its combo chain.", WAR.JobID)]
		WarriorMythrilTempestCombo = 2102,

		[ParentPreset(WarriorStormsPathCombo)]
		[CustomComboInfo("Gauge Overcap Saver: Storm's Path", "Replace the Storm's Path combo with gauge spender if completing the combo would overcap you.", WAR.JobID)]
		WarriorGaugeOvercapPathFeature = 2103,

		[ParentPreset(WarriorStormsEyeCombo)]
		[CustomComboInfo("Gauge Overcap Saver: Storm's Eye", "Replace the Storm's Eye combo with gauge spender if completing the combo would overcap you.", WAR.JobID)]
		WarriorGaugeOvercapEyeFeature = 2110,

		[ParentPreset(WarriorMythrilTempestCombo)]
		[CustomComboInfo("Gauge Overcap Saver: Mythril Tempest", "Replace the Mythril Tempest combo with gauge spender if completing the combo would overcap you.", WAR.JobID)]
		WarriorGaugeOvercapTempestFeature = 2111,

		[CustomComboInfo("Inner Release Feature", "Replace single-target and AoE combo with Fell Cleave/Decimate during Inner Release.", WAR.JobID)]
		WarriorInnerReleaseFeature = 2104,

		[CustomComboInfo("Nascent Flash Feature", "Replace Nascent Flash with Raw Intuition when below level 76.", WAR.JobID)]
		WarriorNascentFlashFeature = 2105,

		[CustomComboInfo("Primal Beast Feature", "Replace Inner Beast and Steel Cyclone with Primal Rend when available", WAR.JobID)]
		WarriorPrimalBeastFeature = 2107,

		[CustomComboInfo("Primal Release Feature", "Replace Inner Release with Primal Rend when available", WAR.JobID)]
		WarriorPrimalReleaseFeature = 2108,

		[CustomComboInfo("Stun/Interrupt Feature", "Turn Low Blow and Interject into Interject when off CD and your target can be interrupted, Low Blow otherwise.", WAR.JobID)]
		WarriorStunInterruptFeature = 2109,

		#endregion
		// ====================================================================================
		#region WHITE MAGE (24xx)

		[CustomComboInfo("Swiftcast Raise", "Raise turns into Swiftcast when available and reasonable.", WHM.JobID)]
		WhiteMageSwiftcastRaiserFeature = 2400,

		[CustomComboInfo("Afflatus Feature", "Changes Cure 2 into Afflatus Solace, and Medica into Afflatus Rapture, when lilies are up.", WHM.JobID)]
		WhiteMageAfflatusFeature = 2404,

		[ParentPreset(WhiteMageAfflatusFeature)]
		[CustomComboInfo("Solace into Misery", "Replaces Afflatus Solace with Afflatus Misery when Misery is ready to be used.", WHM.JobID)]
		WhiteMageSolaceMiseryFeature = 2401,

		[ParentPreset(WhiteMageAfflatusFeature)]
		[CustomComboInfo("Rapture into Misery", "Replaces Afflatus Rapture with Afflatus Misery when Misery is ready to be used.", WHM.JobID)]
		WhiteMageRaptureMiseryFeature = 2402,

		[CustomComboInfo("Cure 2 to Cure Level Sync", "Changes Cure 2 to Cure when below level 30 in synced content.", WHM.JobID)]
		WhiteMageCureFeature = 2403,

		#endregion
		// ====================================================================================
		#region DoH (98xx)

		// [CustomComboInfo("Placeholder", "Placeholder.", DOH.JobID)]
		// DohPlaceholder = 9801,

		#endregion
		// ====================================================================================
		#region DoL (99xx)

		[CustomComboInfo("Eureka Feature", "Replace Ageless Words and Solid Reason with Wise to the World when available.", DOL.JobID)]
		GatherEurekaFeature = 9900,

		[CustomComboInfo("Cast / Hook Feature", "Replace Cast with Hook when fishing.", DOL.JobID)]
		FisherCastHookFeature = 9901,

		[CustomComboInfo("Cast / Gig Feature", "Replace Cast with Gig when swimming.", DOL.JobID)]
		FisherCastGigFeature = 9902,

		[CustomComboInfo("Surface Slap / Veteran Trade Feature", "Replace Surface Slap with Veteran Trade when swimming.", DOL.JobID)]
		FisherSurfaceTradeFeature = 9903,

		[CustomComboInfo("Prize Catch / Nature's Bounty Feature", "Replace Prize Catch with Nature's Bounty when swimming.", DOL.JobID)]
		FisherPrizeBountyFeature = 9904,

		[CustomComboInfo("Snagging / Salvage Feature", "Replace Snagging with Salvage when swimming.", DOL.JobID)]
		FisherSnaggingSalvageFeature = 9905,

		[CustomComboInfo("Identical Cast / Vital Sight Feature", "Replace Identical Cast with Vital Sight when swimming.", DOL.JobID)]
		FisherIdenticalSightFeature = 9906,

		[CustomComboInfo("Makeshift Bait / Baited Breath Feature", "Replace Makeshift Bait with Baited Breath when swimming.", DOL.JobID)]
		FisherMakeshiftBreathFeature = 9907,

		[CustomComboInfo("Chum / Electric Current Feature", "Replace Chum with Electric Current when swimming.", DOL.JobID)]
		FisherElectricChumFeature = 9908,

		#endregion
		// ====================================================================================
	}
	public static class CustomComboPresetExtensions {
		public static CustomComboPreset[] GetConflicts(this CustomComboPreset preset) => preset.GetAttribute<ConflictsAttribute>()?.Conflicts ?? Array.Empty<CustomComboPreset>();
		public static CustomComboPreset? GetParent(this CustomComboPreset preset) => preset.GetAttribute<ParentPresetAttribute>()?.Parent;
	}
}
