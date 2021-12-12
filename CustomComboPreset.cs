using System;

using Dalamud.Utility;

using XIVComboVX.Attributes;
using XIVComboVX.Combos;

namespace XIVComboVX {
	public enum CustomComboPreset {
		// ====================================================================================
		#region ASTROLOGIAN (33xx)

		[CustomComboInfo("Swiftcast Ascend", "Ascend turns into Swiftcast when it's off cooldown.", AST.JobID)]
		AstrologianSwiftcastRaiserFeature = 3300,

		[CustomComboInfo("Draw on Play", "Play turns into Draw when no card is drawn, as well as the usual Play behavior.", AST.JobID)]
		AstrologianCardsOnDrawFeature = 3301,

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

		[CustomComboInfo("Fire 1/3 Feature", "Fire 1 becomes Fire 3 outside of Astral Fire, and when Firestarter proc is up.", BLM.JobID)]
		BlackFireFeature = 2503,

		[CustomComboInfo("Blizzard 1/3 Feature", "Blizzard 1 becomes Blizzard 3 when out of Umbral Ice.", BLM.JobID)]
		BlackBlizzardFeature = 2504,

		[CustomComboInfo("Freeze/Flare Feature", "Freeze and Flare become whichever action you can currently use.", BLM.JobID)]
		BlackFreezeFlareFeature = 2505,

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

		[Conflicts(DancerDanceComboCompatibility)]
		[CustomComboInfo("Dance Step Combo", "Change Standard Step and Technical Step into each dance step while dancing.", DNC.JobID)]
		DancerDanceStepCombo = 3805,

		[Conflicts(DancerDanceStepCombo)]
		[CustomComboInfo("Dance Step Feature", "Change custom actions into dance steps while dancing." +
			"\nThe defaults are Cascade, Flourish, Fan Dance and Fan Dance II. If set to 0, they will reset to these actions." +
			"\nYou can get Action IDs with Garland Tools by searching for the action and clicking the cog.", DNC.JobID)]
		DancerDanceComboCompatibility = 3806,

		[CustomComboInfo("Devilment Feature", "Change Devilment into Starfall Dance after use.", DNC.JobID)]
		DancerDevilmentFeature = 3807,

		[CustomComboInfo("Fan Dance Switchers", "Change Fan Dance 1/2 into Fan Dance 3/4 based on the below combos.", DNC.JobID)]
		DancerFanDanceSwitcher = 3810,

		[ParentPreset(DancerFanDanceSwitcher)]
		[CustomComboInfo("Fan Dance 1/3 Combo", "Change Fan Dance 1 into Fan Dance 3 when available.", DNC.JobID)]
		DancerFanDance13Combo = 3802,

		[ParentPreset(DancerFanDanceSwitcher)]
		[CustomComboInfo("Fan Dance 1/4 Combo", "Change Fan Dance 1 into Fan Dance 4 when available.", DNC.JobID)]
		DancerFanDance14Combo = 3808,

		[ParentPreset(DancerFanDanceSwitcher)]
		[CustomComboInfo("Fan Dance 2/3 Combo", "Change Fan Dance 2 into Fan Dance 3 when available.", DNC.JobID)]
		DancerFanDance23Combo = 3803,

		[ParentPreset(DancerFanDanceSwitcher)]
		[CustomComboInfo("Fan Dance 2/4 Combo", "Change Fan Dance 2 into Fan Dance 4 when available.", DNC.JobID)]
		DancerFanDance24Combo = 3809,

		#endregion
		// ====================================================================================
		#region DRAGOON (22xx)

		[CustomComboInfo("Coerthan Torment Combo", "Replace Coerthan Torment with its combo chain.", DRG.JobID)]
		DragoonCoerthanTormentCombo = 2200,

		[CustomComboInfo("Chaos Thrust Combo", "Replace Chaos Thrust with its combo chain.", DRG.JobID)]
		DragoonChaosThrustCombo = 2201,

		[CustomComboInfo("Full Thrust Combo", "Replace Full Thrust with its combo chain.", DRG.JobID)]
		DragoonFullThrustCombo = 2202,

		[CustomComboInfo("Jump + Mirage Dive", "Replace (High) Jump with Mirage Dive when Dive Ready.", DRG.JobID)]
		DragoonJumpFeature = 2203,

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
		[CustomComboInfo("Salty Shadowbringer", "Replace Cave and Spit and Abyssal Drain with Salted Earth and Shadowbringer depending on cooldown.", DRK.JobID)]
		DarkShadowbringerFeature = 3204,

		#endregion
		// ====================================================================================
		#region GUNBREAKER (37xx)

		[CustomComboInfo("Solid Barrel Combo", "Replace Solid Barrel with its combo chain.", GNB.JobID)]
		GunbreakerSolidBarrelCombo = 3700,

		[CustomComboInfo("Wicked Talon Continuation", "Replace Gnashing Fang with Continuation moves when appropriate.", GNB.JobID)]
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

		[CustomComboInfo("Hot Shot / Air Anchor / Drill / Chainsaw Feature", "Replace Hot Shot (Air Anchor), Drill, and Chainsaw with whichever is available.", MCH.JobID)]
		MachinistDrillAirAnchorFeature = 3105,

		#endregion
		// ====================================================================================
		#region MONK (20xx)

		// [CustomComboInfo("Monk AoE Combo", "Replaces Rockbreaker and Four Point Fury with the AoE combo chain.\nWhen Perfect Balance or Formless Fist is active, Rockbreaker does not change.", MNK.JobID)]
		[CustomComboInfo("Monk AoE Combo", "Replaces Rockbreaker with the AoE combo chain,\nor Rockbreaker when Perfect Balance or Formless Fist is active.", MNK.JobID)]
		MnkAoECombo = 2000,

		[CustomComboInfo("Monk Bootshine Feature", "Replaces Dragon Kick with Bootshine if both Opo-opo form and Leaden Fist are up.", MNK.JobID)]
		MnkBootshineFeature = 2001,

		[Dangerous]
		[Experimental]
		[CustomComboInfo("Howling Fist / Meditation Feature", "Replaces Howling Fist with Meditation when the Fifth Chakra is not open.", MNK.JobID)]
		MonkHowlingFistMeditationFeature = 2002,

		#endregion
		// ====================================================================================
		#region NINJA (30xx)

		[CustomComboInfo("Armor Crush Combo", "Replace Armor Crush with its combo chain.", NIN.JobID)]
		NinjaArmorCrushCombo = 3000,

		[CustomComboInfo("Aeolian Edge Combo", "Replace Aeolian Edge with its combo chain.", NIN.JobID)]
		NinjaAeolianEdgeCombo = 3001,

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

		[CustomComboInfo("Bunshin / Kamaitachi Feature", "Replaces Bunshin with Phantom Kamaitachi after usage.", NIN.JobID)]
		NinjaBunshinKamaitachiFeature = 3009,

		[CustomComboInfo("Armor Crush / Raiju Feature", "Replaces the Armor Crush combo with Forked and Fleeting Raiju when available.", NIN.JobID)]
		NinjaArmorCrushRaijuFeature = 3010,

		[CustomComboInfo("Aeolian Edge / Raiju Feature", "Replaces the Aeolian Edge combo with Forked and Fleeting Raiju when available.", NIN.JobID)]
		NinjaAeolianEdgeRaijuFeature = 3011,

		[CustomComboInfo("Huraijin / Raiju Feature", "Replaces Huraijin with Forked and Fleeting Raiju when available.", NIN.JobID)]
		NinjaHuraijinRaijuFeature = 3012,

		#endregion
		// ====================================================================================
		#region PALADIN (19xx)

		[CustomComboInfo("Goring Blade Combo", "Replace Goring Blade with its combo chain.", PLD.JobID)]
		PaladinGoringBladeCombo = 1900,

		[CustomComboInfo("Royal Authority Combo", "Replace Royal Authority/Rage of Halone with its combo chain.", PLD.JobID)]
		PaladinRoyalAuthorityCombo = 1901,

		[CustomComboInfo("Atonement Feature", "Replace Royal Authority with Atonement when under the effect of Sword Oath.", PLD.JobID)]
		PaladinAtonementFeature = 1902,

		[CustomComboInfo("Prominence Combo", "Replace Prominence with its combo chain.", PLD.JobID)]
		PaladinProminenceCombo = 1903,

		[CustomComboInfo("Requiescat Confiteor", "Replace Requiescat with Confiteor while under the effect of Requiescat.\nReplace Requiescat with Confiteor's combos while chaining.", PLD.JobID)]
		PaladinRequiescatConfiteorCombo = 1904,

		[CustomComboInfo("Requiescat Feature", "Replace Royal Authority/Goring Blade combo with Holy Spirit, and Prominence combo with Holy Circle, while Requiescat is active.\nRequires said combos to be activated to work.", PLD.JobID)]
		PaladinRequiescatFeature = 1905,

		[CustomComboInfo("Intervene Level Sync", "Replace Intervene with Shield Lob when under level.", PLD.JobID)]
		PaladinInterveneSyncFeature = 1906,

		#endregion
		// ====================================================================================
		#region REAPER (39xx)

		[CustomComboInfo("Slice Combo", "Replace Infernal Slice with its combo chain.", RPR.JobID)]
		ReaperSliceCombo = 3900,

		[CustomComboInfo("Scythe Combo", "Replace Nightmare Scythe with its combo chain.", RPR.JobID)]
		ReaperScytheCombo = 3901,

		[CustomComboInfo("Soul Reaver Gibbet Feature", "Replace Infernal Slice with Gibbet while Reaving or Enshrouded.", RPR.JobID)]
		ReaperSoulReaverGibbetFeature = 3902,

		[ParentPreset(ReaperSoulReaverGibbetFeature)]
		[CustomComboInfo("Soul Reaver Gibbet Option", "Replace Infernal Slice with Gallows instead while Reaving or Enshrouded.", RPR.JobID)]
		ReaperSoulReaverGibbetOption = 3903,

		[CustomComboInfo("Soul Reaver Gallows Feature", "Replace Shadow of Death with Gallows while Reaving or Enshrouded.", RPR.JobID)]
		ReaperSoulReaverGallowsFeature = 3904,

		[ParentPreset(ReaperSoulReaverGallowsFeature)]
		[CustomComboInfo("Soul Reaver Gallows Option", "Replace Shadow of Death with Gibbet instead while Reaving or Enshrouded.", RPR.JobID)]
		ReaperSoulReaverGallowsOption = 3905,

		[CustomComboInfo("Soul Reaver Guillotine Option", "Replace Nightmare Scythe with Guillotine while Reaving or Enshrouded.", RPR.JobID)]
		ReaperSoulReaverGuillotineFeature = 3906,

		[CustomComboInfo("Arcane Harvest Feature", "Replace Arcane Circle with Plentiful Harvest when you have stacks of Immortal Sacrifice.", RPR.JobID)]
		ReaperHarvestFeature = 3907,

		[CustomComboInfo("Enshroud Communio Feature", "Replace Enshroud with Communio when Enshrouded.", RPR.JobID)]
		ReaperEnshroudCommunioFeature = 3908,

		[CustomComboInfo("Regress Feature", "Both Hell's Ingress and Egress turn into Regress when Threshold is active, instead of just the opposite of the one used.", RPR.JobID)]
		ReaperRegressFeature = 3909,

		#endregion
		// ====================================================================================
		#region RED MAGE (35xx)

		[CustomComboInfo("Swiftcast Verraise", "Verraise turns into Swiftcast when it's off cooldown and you don't have a cast speeder.", RDM.JobID)]
		RedMageSwiftcastRaiserFeature = 3500,

		[CustomComboInfo("Redoublement combo", "Replaces Redoublement with its combo chain, following enchantment rules.", RDM.JobID)]
		RedMageMeleeCombo = 3502,

		[ParentPreset(RedMageMeleeCombo)]
		[CustomComboInfo("Redoublement Combo Plus", "Replaces Redoublement with Verflare/Verholy (and then Scorch and Resolution) after Enchanted Redoublement, whichever is more appropriate.", RDM.JobID)]
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
		[Experimental]
		[CustomComboInfo("Smartcast AoE", "Dynamically replaces Veraero/Verthunder 2 with the appropriate spell based on your job gauge.\nIncludes Impact/Scatter when fastcasting.", RDM.JobID)]
		RedMageSmartcastAoEFeature = 3508,

		[Conflicts(RedMageVerprocCombo)]
		[Experimental]
		[CustomComboInfo("Smartcast Single Target", "Dynamically replaces Verstone/Verfire with the appropriate spell based on your job gauge.\nVeraero and Verthunder are replaced with one or the other accordingly, for openers.", RDM.JobID)]
		RedMageSmartcastSingleFeature = 3509,

		#endregion
		// ====================================================================================
		#region SAGE (40xx)

		// Nobody here but us chickens

		#endregion
		// ====================================================================================
		#region SAMURAI (34xx)

		[CustomComboInfo("Yukikaze Combo", "Replace Yukikaze with its combo chain.", SAM.JobID)]
		SamuraiYukikazeCombo = 3400,

		[CustomComboInfo("Gekko Combo", "Replace Gekko with its combo chain.", SAM.JobID)]
		SamuraiGekkoCombo = 3401,

		[CustomComboInfo("Kasha Combo", "Replace Kasha with its combo chain.", SAM.JobID)]
		SamuraiKashaCombo = 3402,

		[CustomComboInfo("Mangetsu Combo", "Replace Mangetsu with its combo chain.", SAM.JobID)]
		SamuraiMangetsuCombo = 3403,

		[CustomComboInfo("Oka Combo", "Replace Oka with its combo chain.", SAM.JobID)]
		SamuraiOkaCombo = 3404,

		[CustomComboInfo("Jinpu/Shifu Feature", "Replace Meikyo Shisui with Jinpu or Shifu depending on what is needed.", SAM.JobID)]
		SamuraiJinpuShifuFeature = 3406,

		[Conflicts(SamuraiIaijutsuTsubameGaeshiFeature)]
		[CustomComboInfo("Tsubame-gaeshi to Iaijutsu", "Replace Tsubame-gaeshi with Iaijutsu when Sen is empty.", SAM.JobID)]
		SamuraiTsubameGaeshiIaijutsuFeature = 3407,

		[Conflicts(SamuraiTsubameGaeshiIaijutsuFeature)]
		[CustomComboInfo("Iaijutsu to Tsubame-gaeshi", "Replace Iaijutsu with Tsubame-gaeshi when Sen is not empty.", SAM.JobID)]
		SamuraiIaijutsuTsubameGaeshiFeature = 3408,

		[Conflicts(SamuraiIaijutsuShohaFeature)]
		[CustomComboInfo("Tsubame-gaeshi to Shoha", "Replace Tsubame-gaeshi with Shoha when meditation is 3.", SAM.JobID)]
		SamuraiTsubameGaeshiShohaFeature = 3409,

		[Conflicts(SamuraiTsubameGaeshiShohaFeature)]
		[CustomComboInfo("Iaijutsu to Shoha", "Replace Iaijutsu with Shoha when meditation is 3.", SAM.JobID)]
		SamuraiIaijutsuShohaFeature = 3410,

		[CustomComboInfo("Kyuten to Shoha 2", "Replace Hissatsu: Kyuten with Shoha 2 when Meditation is full.", SAM.JobID)]
		SamuraiShoha2Feature = 3412,

		[CustomComboInfo("Ikishoten Namikiri Feature", "Replace Ikishoten with Shoha, Kaeshi Namikiri, and then Ogi Namikiri when available.", SAM.JobID)]
		SamuraiIkishotenNamikiriFeature = 3411,

		#endregion
		// ====================================================================================
		#region SCHOLAR (28xx)

		[CustomComboInfo("Swiftcast Resurrection", "Resurrection turns into Swiftcast when it's off cooldown.", SCH.JobID)]
		ScholarSwiftcastRaiserFeature = 2800,

		[CustomComboInfo("Seraph Fey Blessing/Consolation", "Change Fey Blessing into Consolation when Seraph is out.", SCH.JobID)]
		ScholarSeraphConsolationFeature = 2801,

		[CustomComboInfo("ED Aetherflow", "Change Energy Drain into Aetherflow when you have no more Aetherflow stacks.", SCH.JobID)]
		ScholarEnergyDrainFeature = 2802,

		#endregion
		// ====================================================================================
		#region SUMMONER (27xx)

		[CustomComboInfo("Swiftcast Resurrection", "Resurrection turns into Swiftcast when it's off cooldown.", SMN.JobID)]
		SummonerSwiftcastRaiserFeature = 2700,

		[CustomComboInfo("ED Fester", "Change Fester into Energy Drain when out of Aetherflow stacks.", SMN.JobID)]
		SummonerEDFesterCombo = 2704,

		[CustomComboInfo("ES Painflare", "Change Painflare into Energy Syphon when out of Aetherflow stacks.", SMN.JobID)]
		SummonerESPainflareCombo = 2705,

		[Conflicts(SummonerFurtherRuinFeature)]
		[CustomComboInfo("Shiny Ruin Feature", "Change Ruin into Gemburst when attuned.", SMN.JobID)]
		SummonerShinyRuinFeature = 2706,

		[ParentPreset(SummonerShinyRuinFeature)]
		[CustomComboInfo("Further Shiny Ruin Feature", "Change Ruin into Ruin4 when available and appropriate.", SMN.JobID)]
		SummoneFurtherShinyRuinFeature = 2708,

		[Conflicts(SummonerFurtherOutburstFeature)]
		[CustomComboInfo("Shiny Outburst Feature", "Change Outburst into Precious Brilliance when attuned.", SMN.JobID)]
		SummonerShinyOutburstFeature = 2707,

		[ParentPreset(SummonerShinyOutburstFeature)]
		[CustomComboInfo("Further Shiny Outburst Feature", "Change Outburst into Ruin4 when available and appropriate.", SMN.JobID)]
		SummonerFurtherShinyOutburstFeature = 2709,

		[Conflicts(SummonerShinyRuinFeature)]
		[CustomComboInfo("Further Ruin Feature", "Change Ruin into Ruin4 when available and appropriate.", SMN.JobID)]
		SummonerFurtherRuinFeature = 2710,

		[Conflicts(SummonerShinyOutburstFeature)]
		[CustomComboInfo("Further Outburst Feature", "Change Outburst into Ruin4 when available and appropriate.", SMN.JobID)]
		SummonerFurtherOutburstFeature = 2711,

		[CustomComboInfo("Enkindle Feature", "When not attuned, changes Gemshine and Precious Brilliance with Enkindle.", SMN.JobID)]
		SummonerDemiFeature = 2712,

		#endregion
		// ====================================================================================
		#region WARRIOR (21xx)

		[CustomComboInfo("Storms Path Combo", "Replace Storms Path with its combo chain.", WAR.JobID)]
		WarriorStormsPathCombo = 2100,

		[CustomComboInfo("Storms Eye Combo", "Replace Storms Eye with its combo chain.", WAR.JobID)]
		WarriorStormsEyeCombo = 2101,

		[CustomComboInfo("Mythril Tempest Combo", "Replace Mythril Tempest with its combo chain.", WAR.JobID)]
		WarriorMythrilTempestCombo = 2102,

		[CustomComboInfo("Warrior Gauge Overcap Saver", "Replace Single-target or AoE combo with gauge spender if you are about to overcap and are before a step of a combo that would generate beast gauge.", WAR.JobID)]
		WarriorGaugeOvercapFeature = 2103,

		[CustomComboInfo("Inner Release Feature", "Replace Single-target and AoE combo with Fell Cleave/Decimate during Inner Release.", WAR.JobID)]
		WarriorInnerReleaseFeature = 2104,

		[CustomComboInfo("Nascent Flash Feature", "Replace Nascent Flash with Raw intuition when below level 76.", WAR.JobID)]
		WarriorNascentFlashFeature = 2105,

		[CustomComboInfo("Primal Rend Feature", "Replace Inner Beast and Steel Cyclone with Primal Rend when available", WAR.JobID)]
		WarriorPrimalRendFeature = 2107,

		#endregion
		// ====================================================================================
		#region WHITE MAGE (24xx)

		[CustomComboInfo("Swiftcast Raise", "Raise turns into Swiftcast when it's off cooldown.", WHM.JobID)]
		WhiteMageSwiftcastRaiserFeature = 2400,

		[CustomComboInfo("Solace into Misery", "Replaces Afflatus Solace with Afflatus Misery when Misery is ready to be used.", WHM.JobID)]
		WhiteMageSolaceMiseryFeature = 2401,

		[CustomComboInfo("Rapture into Misery", "Replaces Afflatus Rapture with Afflatus Misery when Misery is ready to be used.", WHM.JobID)]
		WhiteMageRaptureMiseryFeature = 2402,

		[CustomComboInfo("Cure 2 to Cure Level Sync", "Changes Cure 2 to Cure when below level 30 in synced content.", WHM.JobID)]
		WhiteMageCureFeature = 2403,

		[CustomComboInfo("Afflatus Feature", "Changes Cure 2 into Afflatus Solace, and Medica into Afflatus Rapture, when lilies are up.", WHM.JobID)]
		WhiteMageAfflatusFeature = 2404,

		#endregion
		// ====================================================================================
	}
	public static class CustomComboPresetExtensions {
		public static CustomComboPreset[] GetConflicts(this CustomComboPreset preset) => preset.GetAttribute<ConflictsAttribute>()?.Conflicts ?? Array.Empty<CustomComboPreset>();
		public static CustomComboPreset? GetParent(this CustomComboPreset preset) => preset.GetAttribute<ParentPresetAttribute>()?.Parent;
	}
}
