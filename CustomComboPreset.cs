
using XIVCombo;

using XIVComboVeryExpandedPlugin.Combos;

namespace XIVComboVeryExpandedPlugin {
	public enum CustomComboPreset {
		// Last enum used: 108
		// ====================================================================================
		#region ASTROLOGIAN

		[Ordered]
		[CustomComboInfo("Swiftcast Ascend", "Ascend turns into Swiftcast when it's off cooldown.", AST.JobID, AST.Ascend)]
		AstrologianSwiftcastRaiserFeature = 98,

		[Ordered]
		[CustomComboInfo("Draw on Play", "Play turns into Draw when no card is drawn, as well as the usual Play behavior.", AST.JobID, AST.Play)]
		AstrologianCardsOnDrawFeature = 27,

		[Ordered]
		[CustomComboInfo("Minor Arcana to Sleeve Draw", "Changes Minor Arcana to Sleeve Draw when a card is not drawn.", AST.JobID, AST.MinorArcana)]
		AstrologianSleeveDrawFeature = 75,

		[Ordered]
		[CustomComboInfo("Benefic 2 to Benefic Level Sync", "Changes Benefic 2 to Benefic when below level 26.", AST.JobID, AST.Benefic2)]
		AstrologianBeneficFeature = 73,

		#endregion
		// ====================================================================================
		#region BLACK MAGE

		[Ordered]
		[CustomComboInfo("Enochian Stance Switcher", "Change Enochian to Fire 4 or Blizzard 4 depending on stance.", BLM.JobID, BLM.Enochian)]
		BlackEnochianFeature = 25,

		[Ordered]
		[CustomComboInfo("Umbral Soul/Transpose Switcher", "Change Transpose into Umbral Soul when Umbral Soul is usable.", BLM.JobID, BLM.Transpose)]
		BlackManaFeature = 26,

		[Ordered]
		[CustomComboInfo("Fire 1/3", "Fire 1 becomes Fire 3 outside of Astral Fire, OR when Firestarter proc is up.", BLM.JobID, BLM.Fire)]
		BlackFire13Feature = 70,

		[Ordered]
		[CustomComboInfo("Fire 4 to Fire 1/3 Feature", "Fire 1 (and 3 if procced) will replace Fire 4 on Enochian Switcher.\nOccurs when:\n- less than 3 seconds left and firestarter up (for F3), or\n- less than 6 seconds left and NO firestarter (for F1)\nEnochian Stance Switcher must be active.", BLM.JobID, BLM.Enochian)]
		BlackEnochianSmartFireSwitcherFeature = 97,

		[Ordered]
		[CustomComboInfo("Blizzard 1/3", "Blizzard 1 becomes Blizzard 3 when out of Umbral Ice.", BLM.JobID, BLM.Blizzard)]
		BlackBlizzardFeature = 71,

		[Ordered]
		[CustomComboInfo("Freeze Feature", "Freeze becomes Blizzard 2 when synced.", BLM.JobID, BLM.Freeze)]
		BlackFreezeFeature = 107,

		[Ordered]
		[CustomComboInfo("Thunder", "Thunder 1/3 replaces Enochian/Fire 4/Blizzard 4 on Enochian switcher.\n Occurs when Thundercloud is up and either\n- Thundercloud buff on you is about to run out, or\n- Thunder debuff on your CURRENT target is about to run out\nassuming it won't interrupt timer upkeep.\nEnochian Stance Switcher must be active.", BLM.JobID, BLM.Enochian)]
		BlackThunderFeature = 95,

		[Ordered]
		[CustomComboInfo("Despair", "Despair replaces Fire 4 on Enochian switcher when below 2400 MP.\nEnochian Stance Switcher must be active.", BLM.JobID, BLM.Enochian)]
		BlackDespairFeature = 96,

		[Ordered]
		[CustomComboInfo("Scathe/Xenoglossy Feature", "Scathe becomes Xenoglossy when available.", BLM.JobID, BLM.Scathe)]
		BlackScatheFeature = 104,

		[Ordered]
		[CustomComboInfo("(Between the) Ley Lines", "Change Ley Lines into BTL when Ley Lines is active.", BLM.JobID, BLM.LeyLines)]
		BlackLeyLinesFeature = 56,

		#endregion
		// ====================================================================================
		#region BARD

		[Ordered]
		[CustomComboInfo("Wanderer's into Pitch Perfect", "Replaces Wanderer's Minuet with Pitch Perfect while in WM.", BRD.JobID, BRD.WanderersMinuet)]
		BardWanderersPitchPerfectFeature = 41,

		[Ordered]
		[CustomComboInfo("Heavy Shot into Straight Shot", "Replaces Heavy Shot/Burst Shot with Straight Shot/Refulgent Arrow when procced.", BRD.JobID, BRD.HeavyShot, BRD.BurstShot)]
		BardStraightShotUpgradeFeature = 42,

		[Ordered]
		[CustomComboInfo("Iron Jaws Feature", "Iron Jaws is replaced with Caustic Bite/Stormbite if one or both are not up.\nAlternates between the two if Iron Jaws isn't available.", BRD.JobID, BRD.IronJaws)]
		BardIronJawsFeature = 63,

		[Ordered]
		[CustomComboInfo("Burst Shot/Quick Nock into Apex Arrow", "Replaces Burst Shot and Quick Nock with Apex Arrow when gauge is full.", BRD.JobID, BRD.BurstShot, BRD.QuickNock)]
		BardApexFeature = 74,

		#endregion
		// ====================================================================================
		#region DANCER

		[Ordered]
		[CustomComboInfo("Single Target Multibutton", "Change Cascade into procs and combos as available.", DNC.JobID, DNC.Cascade)]
		DancerSingleTargetMultibutton = 43,

		[Ordered]
		[CustomComboInfo("AoE Multibutton", "Change Windmill into procs and combos as available.", DNC.JobID, DNC.Windmill)]
		DancerAoeMultibutton = 50,

		[Ordered]
		[CustomComboInfo("Fan Dance 1 Combo", "Change Fan Dance 1 into Fan Dance 3 while flourishing.", DNC.JobID, DNC.FanDance1)]
		DancerFanDance1Combo = 33,

		[Ordered]
		[CustomComboInfo("Fan Dance 2 Combo", "Change Fan Dance 2 into Fan Dance 3 while flourishing.", DNC.JobID, DNC.FanDance2)]
		DancerFanDance2Combo = 102,

		[Ordered]
		[CustomComboInfo("Flourish Proc Saver", "Change Flourish into any available procs before using.", DNC.JobID, DNC.Flourish)]
		DancerFlourishFeature = 34,

		[Ordered]
		[CustomComboInfo("Dance Step Combo", "Change Standard Step and Technical Step into each dance step while dancing.", DNC.JobID, DNC.StandardStep, DNC.TechnicalStep)]
		DancerDanceStepCombo = 31,

		[Ordered]
		[CustomComboInfo("Dance Step Feature", "Change custom actions into dance steps while dancing." +
			"\nThe defaults are Cascade, Flourish, Fan Dance and Fan Dance II. If set to 0, they will reset to these actions." +
			"\nYou can get Action IDs with Garland Tools by searching for the action and clicking the cog.", DNC.JobID)]
		DancerDanceComboCompatibility = 72,

		#endregion
		// ====================================================================================
		#region DRAGOON

		[Ordered]
		[CustomComboInfo("Coerthan Torment Combo", "Replace Coerthan Torment with its combo chain.", DRG.JobID, DRG.CoerthanTorment)]
		DragoonCoerthanTormentCombo = 0,

		[Ordered]
		[CustomComboInfo("Chaos Thrust Combo", "Replace Chaos Thrust with its combo chain.", DRG.JobID, DRG.ChaosThrust)]
		DragoonChaosThrustCombo = 1,

		[Ordered]
		[CustomComboInfo("Full Thrust Combo", "Replace Full Thrust with its combo chain.", DRG.JobID, DRG.FullThrust)]
		DragoonFullThrustCombo = 2,

		[Ordered]
		[CustomComboInfo("Jump + Mirage Dive", "Replace (High) Jump with Mirage Dive when Dive Ready.", DRG.JobID, DRG.Jump, DRG.HighJump)]
		DragoonJumpFeature = 44,

		[Ordered]
		[CustomComboInfo("BOTD Into Stardiver", "Replace Blood of the Dragon with Stardiver when in Life of the Dragon.", DRG.JobID, DRG.BloodOfTheDragon)]
		DragoonBOTDFeature = 46,

		#endregion
		// ====================================================================================
		#region DARK KNIGHT

		[Ordered]
		[CustomComboInfo("Souleater Combo", "Replace Souleater with its combo chain.", DRK.JobID, DRK.Souleater)]
		DarkSouleaterCombo = 3,

		[Ordered]
		[CustomComboInfo("Stalwart Soul Combo", "Replace Stalwart Soul with its combo chain.", DRK.JobID, DRK.StalwartSoul)]
		DarkStalwartSoulCombo = 4,

		[Ordered]
		[CustomComboInfo("Delirium Feature", "Replace Souleater and Stalwart Soul with Bloodspiller and Quietus when Delirium is active.", DRK.JobID, DRK.Souleater, DRK.StalwartSoul)]
		DeliriumFeature = 57,

		[Ordered]
		[CustomComboInfo("Dark Knight Gauge Overcap Saver", "Replace AoE combo with gauge spender if you are about to overcap.", DRK.JobID, DRK.StalwartSoul)]
		DRKOvercapFeature = 85,

		#endregion
		// ====================================================================================
		#region GUNBREAKER

		[Ordered]
		[CustomComboInfo("Solid Barrel Combo", "Replace Solid Barrel with its combo chain.", GNB.JobID, GNB.SolidBarrel)]
		GunbreakerSolidBarrelCombo = 20,

		[Ordered]
		[CustomComboInfo("Wicked Talon Combo", "Replace Wicked Talon with its combo chain.", GNB.JobID, GNB.WickedTalon)]
		GunbreakerGnashingFangCombo = 21,

		[Ordered]
		[CustomComboInfo("Wicked Talon Continuation", "In addition to the Wicked Talon combo chain, put Continuation moves on Wicked Talon when appropriate.", GNB.JobID, GNB.WickedTalon)]
		GunbreakerGnashingFangCont = 52,

		[Ordered]
		[CustomComboInfo("Demon Slaughter Combo", "Replace Demon Slaughter with its combo chain.", GNB.JobID, GNB.DemonSlaughter)]
		GunbreakerDemonSlaughterCombo = 22,

		[Ordered]
		[CustomComboInfo("Fated Circle Feature", "In addition to the Demon Slaughter combo, add Fated Circle when charges are full.", GNB.JobID, GNB.DemonSlaughter)]
		GunbreakerFatedCircleFeature = 30,

		[Ordered]
		[CustomComboInfo("Burst Strike to Bloodfest Feature", "Replace Burst Strike with Bloodfest if you have no powder gauge.", GNB.JobID, GNB.BurstStrike)]
		GunbreakerBloodfestOvercapFeature = 83,

		[Ordered]
		[CustomComboInfo("No Mercy Feature", "Replace No Mercy with Bow Shock, and then Sonic Break, while No Mercy is active.", GNB.JobID, GNB.NoMercy)]
		GunbreakerNoMercyFeature = 84,

		[Ordered]
		[CustomComboInfo("Bow Shock / Sonic Break Swap", "Replace Bow Shock and Sonic Break with one or the other, depending on which is on cooldown.", GNB.JobID, GNB.BowShock, GNB.SonicBreak)]
		GunbreakerBowShockSonicBreakFeature = 106,

		#endregion
		// ====================================================================================
		#region MACHINIST

		[Ordered]
		[CustomComboInfo("(Heated) Shot Combo", "Replace either form of Clean Shot with its combo chain.", MCH.JobID, MCH.CleanShot, MCH.HeatedCleanShot)]
		MachinistMainCombo = 23,

		[Ordered]
		[CustomComboInfo("Spread Shot Heat", "Replace Spread Shot with Auto Crossbow when overheated.", MCH.JobID, MCH.SpreadShot)]
		MachinistSpreadShotFeature = 24,

		[Ordered]
		[CustomComboInfo("Hypercharge Feature", "Replace Heat Blast and Auto Crossbow with Hypercharge when not overheated.", MCH.JobID, MCH.HeatBlast, MCH.AutoCrossbow)]
		MachinistOverheatFeature = 47,

		[Ordered]
		[CustomComboInfo("Overdrive Feature", "Replace Rook Autoturret and Automaton Queen with Overdrive while active.", MCH.JobID, MCH.RookAutoturret, MCH.AutomatonQueen)]
		MachinistOverdriveFeature = 58,

		[Ordered]
		[CustomComboInfo("Gauss Round / Ricochet Feature", "Replace Gauss Round and Ricochet with one or the other depending on which has less recharge time left.", MCH.JobID, MCH.GaussRound, MCH.Ricochet)]
		MachinistGaussRoundRicochetFeature = 66,

		[Ordered]
		[CustomComboInfo("Drill / Air Anchor (Hot Shot) Feature", "Replace Drill and Air Anchor (Hot Shot) with one or the other depending on which is on cooldown.", MCH.JobID, MCH.Drill, MCH.AirAnchor, MCH.HotShot)]
		MachinistDrillAirAnchorFeature = 108,

		#endregion
		// ====================================================================================
		#region MONK

		[Ordered]
		[CustomComboInfo("Monk AoE Combo", "Replaces Rockbreaker with the AoE combo chain, or Rockbreaker when Perfect Balance is active.", MNK.JobID, MNK.Rockbreaker)]
		MnkAoECombo = 54,

		[Ordered]
		[CustomComboInfo("Monk Bootshine Feature", "Replaces Dragon Kick with Bootshine if both a form and Leaden Fist are up.", MNK.JobID, MNK.DragonKick)]
		MnkBootshineFeature = 82,

		#endregion
		// ====================================================================================
		#region NINJA

		[Ordered]
		[CustomComboInfo("Armor Crush Combo", "Replace Armor Crush with its combo chain.", NIN.JobID, NIN.ArmorCrush)]
		NinjaArmorCrushCombo = 17,

		[Ordered]
		[CustomComboInfo("Aeolian Edge Combo", "Replace Aeolian Edge with its combo chain.", NIN.JobID, NIN.AeolianEdge)]
		NinjaAeolianEdgeCombo = 18,

		[Ordered]
		[CustomComboInfo("Hakke Mujinsatsu Combo", "Replace Hakke Mujinsatsu with its combo chain.", NIN.JobID, NIN.HakkeMujinsatsu)]
		NinjaHakkeMujinsatsuCombo = 19,

		[Ordered]
		[CustomComboInfo("Dream to Assassinate", "Replace Dream Within a Dream with Assassinate when Assassinate Ready.", NIN.JobID, NIN.DreamWithinADream)]
		NinjaAssassinateFeature = 45,

		[Ordered]
		[CustomComboInfo("Smart Hide", "Replaces Hide with Trick Attack while under the effect of Suiton or Hidden, or else Mug if in combat.", NIN.JobID, NIN.Hide)]
		NinjaHideMugFeature = 90,

		[Ordered]
		[CustomComboInfo("GCDs to Ninjutsu Feature", "Every GCD combo becomes Ninjutsu while Mudras are being used.\nONLY affects GCDs that are in COMBOS!\nIf you turn off the Armor Crush combo, AC will NOT be changed, for instance.", NIN.JobID, NIN.AeolianEdge, NIN.ArmorCrush, NIN.HakkeMujinsatsu)]
		NinjaGCDNinjutsuFeature = 92,

		[Ordered]
		[CustomComboInfo("Kassatsu to Trick", "Replaces Kassatsu with Trick Attack while Suiton or Hidden is up.\nCooldown tracking plugin recommended.", NIN.JobID, NIN.Kassatsu)]
		NinjaKassatsuTrickFeature = 87,

		[Ordered]
		[CustomComboInfo("Ten Chi Jin to Meisui", "Replaces Ten Chi Jin (the move) with Meisui while Suiton is up.\nCooldown tracking plugin recommended.", NIN.JobID, NIN.TenChiJin)]
		NinjaTCJMeisuiFeature = 88,

		[Ordered]
		[CustomComboInfo("Kassatsu Chi/Jin Feature", "Replaces Chi with Jin while Kassatsu is up if you have Enhanced Kassatsu.", NIN.JobID, NIN.Chi)]
		NinjaKassatsuChiJinFeature = 89,

		#endregion
		// ====================================================================================
		#region PALADIN

		[Ordered]
		[CustomComboInfo("Goring Blade Combo", "Replace Goring Blade with its combo chain.", PLD.JobID, PLD.GoringBlade)]
		PaladinGoringBladeCombo = 5,

		[Ordered]
		[CustomComboInfo("Royal Authority Combo", "Replace Royal Authority/Rage of Halone with its combo chain.", PLD.JobID, PLD.RoyalAuthority, PLD.RageOfHalone)]
		PaladinRoyalAuthorityCombo = 6,

		[Ordered]
		[CustomComboInfo("Atonement Feature", "Replace Royal Authority with Atonement when under the effect of Sword Oath.", PLD.JobID, PLD.RoyalAuthority)]
		PaladinAtonementFeature = 59,

		[Ordered]
		[CustomComboInfo("Prominence Combo", "Replace Prominence with its combo chain.", PLD.JobID, PLD.Prominence)]
		PaladinProminenceCombo = 7,

		[Ordered]
		[CustomComboInfo("Requiescat Confiteor", "Replace Requiescat with Confiteor while under the effect of Requiescat.", PLD.JobID, PLD.Requiescat)]
		PaladinRequiescatCombo = 55,

		[Ordered]
		[CustomComboInfo("Requiescat Feature", "Replace Royal Authority/Goring Blade combo with Holy Spirit, and Prominence combo with Holy Circle, while Requiescat is active.\nRequires said combos to be activated to work.", PLD.JobID, PLD.RoyalAuthority, PLD.GoringBlade, PLD.Prominence)]
		PaladinRequiescatFeature = 86,

		[Ordered]
		[CustomComboInfo("Confiteor Feature", "Replace Holy Spirit/Circle with Confiteor while MP is under 4000 and Requiescat is up.", PLD.JobID, PLD.HolySpirit, PLD.HolyCircle)]
		PaladinConfiteorFeature = 69,

		#endregion
		// ====================================================================================
		#region RED MAGE

		[Ordered]
		[CustomComboInfo("Swiftcast Verraise", "Verraise turns into Swiftcast when it's off cooldown and Dualcast isn't up.", RDM.JobID, RDM.Verraise)]
		RedMageSwiftcastRaiserFeature = 99,

		[Ordered]
		[CustomComboInfo("Red Mage AoE Combo", "Replaces Veraero/Verthunder 2 with Impact when Dualcast or Swiftcast are active.", RDM.JobID, RDM.Veraero2, RDM.Verthunder2)]
		RedMageAoECombo = 48,

		[Ordered]
		[CustomComboInfo("Redoublement combo", "Replaces Redoublement with its combo chain, following enchantment rules.", RDM.JobID, RDM.Redoublement)]
		RedMageMeleeCombo = 49,

		[Ordered]
		[CustomComboInfo("Redoublement Combo Plus", "Replaces Redoublement with Verflare/Verholy after Enchanted Redoublement, whichever is more appropriate. Also replaces Redoublement with Scorch after Verflare/Verholy.\nRequires Redoublement Combo.", RDM.JobID, RDM.Redoublement)]
		RedMageMeleeComboPlus = 68,

		[Ordered]
		[CustomComboInfo("Verproc into Jolt", "Replaces Verstone/Verfire with Jolt/Scorch when no proc is available.", RDM.JobID, RDM.Verstone, RDM.Verfire)]
		RedMageVerprocCombo = 53,

		[Ordered]
		[CustomComboInfo("Verproc into Jolt Plus", "Additionally replaces Verstone/Verfire with Veraero/Verthunder if dualcast/swiftcast are up.\nRequires Verproc into Jolt.", RDM.JobID, RDM.Verstone, RDM.Verfire)]
		RedMageVerprocComboPlus = 93,

		[Ordered]
		[CustomComboInfo("Verproc into Jolt Plus Veraero Opener", "Turns Verstone into Veraero when out of combat.\nRequires Verproc into Jolt Plus.", RDM.JobID, RDM.Verfire)]
		RedMageVeraeroOpenerFeature = 94,

		[Ordered]
		[CustomComboInfo("Verproc into Jolt Plus Verthunder Opener", "Turns Verfire into Verthunder when out of combat.\nRequires Verproc into Jolt Plus.", RDM.JobID, RDM.Verfire)]
		RedMageVerthunderOpenerFeature = 105,

		#endregion
		// ====================================================================================
		#region SAMURAI

		[Ordered]
		[CustomComboInfo("Yukikaze Combo", "Replace Yukikaze with its combo chain.", SAM.JobID, SAM.Yukikaze)]
		SamuraiYukikazeCombo = 11,

		[Ordered]
		[CustomComboInfo("Gekko Combo", "Replace Gekko with its combo chain.", SAM.JobID, SAM.Gekko)]
		SamuraiGekkoCombo = 12,

		[Ordered]
		[CustomComboInfo("Kasha Combo", "Replace Kasha with its combo chain.", SAM.JobID, SAM.Kasha)]
		SamuraiKashaCombo = 13,

		[Ordered]
		[CustomComboInfo("Mangetsu Combo", "Replace Mangetsu with its combo chain.", SAM.JobID, SAM.Mangetsu)]
		SamuraiMangetsuCombo = 14,

		[Ordered]
		[CustomComboInfo("Oka Combo", "Replace Oka with its combo chain.", SAM.JobID, SAM.Oka)]
		SamuraiOkaCombo = 15,

		[Ordered]
		[CustomComboInfo("Seigan to Third Eye", "Replace Seigan with Third Eye when not procced.", SAM.JobID, SAM.Seigan)]
		SamuraiThirdEyeFeature = 51,

		[Ordered]
		[CustomComboInfo("Jinpu/Shifu Feature", "Replace Meikyo Shisui with Jinpu or Shifu depending on what is needed.", SAM.JobID, SAM.MeikyoShisui)]
		SamuraiJinpuShifuFeature = 81,

		[Ordered]
		[CustomComboInfo("Tsubame-gaeshi to Iaijutsu", "Replace Tsubame-gaeshi with Iaijutsu when Sen is empty.", SAM.JobID, SAM.TsubameGaeshi)]
		SamuraiTsubameGaeshiIaijutsuFeature = 60,

		[Ordered]
		[CustomComboInfo("Tsubame-gaeshi to Shoha", "Replace Tsubame-gaeshi with Shoha when meditation is 3.", SAM.JobID, SAM.TsubameGaeshi)]
		SamuraiTsubameGaeshiShohaFeature = 61,

		[Ordered]
		[CustomComboInfo("Iaijutsu to Tsubame-gaeshi", "Replace Iaijutsu with Tsubame-gaeshi when Sen is not empty.\n(Use either the Tsubame-gaeshi version or this)", SAM.JobID, SAM.Iaijutsu)]
		SamuraiIaijutsuTsubameGaeshiFeature = 64,

		[Ordered]
		[CustomComboInfo("Iaijutsu to Shoha", "Replace Iaijutsu with Shoha when meditation is 3.\n(Use either the Tsubame-gaeshi version or this)", SAM.JobID, SAM.Iaijutsu)]
		SamuraiIaijutsuShohaFeature = 65,

		#endregion
		// ====================================================================================
		#region SCHOLAR

		[Ordered]
		[CustomComboInfo("Swiftcast Resurrection", "Resurrection turns into Swiftcast when it's off cooldown.", SCH.JobID, SCH.Resurrection)]
		ScholarSwiftcastRaiserFeature = 100,

		[Ordered]
		[CustomComboInfo("Seraph Fey Blessing/Consolation", "Change Fey Blessing into Consolation when Seraph is out.", SCH.JobID, SCH.FeyBless)]
		ScholarSeraphConsolationFeature = 29,

		[Ordered]
		[CustomComboInfo("ED Aetherflow", "Change Energy Drain into Aetherflow when you have no more Aetherflow stacks.", SCH.JobID, SCH.EnergyDrain)]
		ScholarEnergyDrainFeature = 37,

		#endregion
		// ====================================================================================
		#region SUMMONER

		[Ordered]
		[CustomComboInfo("Swiftcast Resurrection", "Resurrection turns into Swiftcast when it's off cooldown.", SMN.JobID, SMN.Resurrection)]
		SummonerSwiftcastRaiserFeature = 101,

		[Ordered]
		[CustomComboInfo("Demi-summon combiners", "Dreadwyrm Trance, Summon Bahamut, and Firebird Trance are now one button.\nDeathflare, Enkindle Bahamut, and Enkindle Phoenix are now one button", SMN.JobID, SMN.DreadwyrmTrance, SMN.Deathflare)]
		SummonerDemiCombo = 28,

		[Ordered]
		[CustomComboInfo("Demi-Summon Combiners Ultra", "Dreadwyrm Trance, Summon Bahamut, Firebird Trance, Deathflare, Enkindle Bahamut, and Enkindle Phoenix are now one button.\nRequires Demi-Summon Combiners feature.", SMN.JobID, SMN.DreadwyrmTrance)]
		SummonerDemiComboUltra = 80,

		[Ordered]
		[CustomComboInfo("Brand of Purgatory Combo", "Replaces Fountain of Fire with Brand of Purgatory when under the affect of Hellish Conduit.", SMN.JobID, SMN.Ruin1, SMN.Ruin3)]
		SummonerBoPCombo = 38,

		[Ordered]
		[CustomComboInfo("ED Fester", "Change Fester into Energy Drain when out of Aetherflow stacks.", SMN.JobID, SMN.Fester)]
		SummonerEDFesterCombo = 39,

		[Ordered]
		[CustomComboInfo("ES Painflare", "Change Painflare into Energy Syphon when out of Aetherflow stacks.", SMN.JobID, SMN.Painflare)]
		SummonerESPainflareCombo = 40,

		#endregion
		// ====================================================================================
		#region WARRIOR

		[Ordered]
		[CustomComboInfo("Storms Path Combo", "Replace Storms Path with its combo chain.", WAR.JobID, WAR.StormsPath)]
		WarriorStormsPathCombo = 8,

		[Ordered]
		[CustomComboInfo("Storms Eye Combo", "Replace Storms Eye with its combo chain.", WAR.JobID, WAR.StormsEye)]
		WarriorStormsEyeCombo = 9,

		[Ordered]
		[CustomComboInfo("Mythril Tempest Combo", "Replace Mythril Tempest with its combo chain.", WAR.JobID, WAR.MythrilTempest)]
		WarriorMythrilTempestCombo = 10,

		[Ordered]
		[CustomComboInfo("Warrior Gauge Overcap Saver", "Replace Single-target or AoE combo with gauge spender if you are about to overcap and are before a step of a combo that would generate beast gauge.", WAR.JobID, WAR.MythrilTempest, WAR.StormsEye, WAR.StormsPath)]
		WarriorGaugeOvercapFeature = 78,

		[Ordered]
		[CustomComboInfo("Inner Release Feature", "Replace Single-target and AoE combo with Fell Cleave/Decimate during Inner Release.", WAR.JobID, WAR.MythrilTempest, WAR.StormsPath)]
		WarriorInnerReleaseFeature = 79,

		[Ordered]
		[CustomComboInfo("Nascent Flash Feature", "Replace Nascent Flash with Raw intuition when below level 76.", WAR.JobID, WAR.NascentFlash)]
		WarriorNascentFlashFeature = 67,

		#endregion
		// ====================================================================================
		#region WHITE MAGE

		[Ordered]
		[CustomComboInfo("Swiftcast Raise", "Raise turns into Swiftcast when it's off cooldown.", WHM.JobID, WHM.Raise)]
		WhiteMageSwiftcastRaiserFeature = 103,

		[Ordered]
		[CustomComboInfo("Solace into Misery", "Replaces Afflatus Solace with Afflatus Misery when Misery is ready to be used.", WHM.JobID, WHM.AfflatusSolace)]
		WhiteMageSolaceMiseryFeature = 35,

		[Ordered]
		[CustomComboInfo("Rapture into Misery", "Replaces Afflatus Rapture with Afflatus Misery when Misery is ready to be used.", WHM.JobID, WHM.AfflatusRapture)]
		WhiteMageRaptureMiseryFeature = 36,

		[Ordered]
		[CustomComboInfo("Cure 2 to Cure Level Sync", "Changes Cure 2 to Cure when below level 30 in synced content.", WHM.JobID, WHM.Cure2)]
		WhiteMageCureFeature = 76,

		[Ordered]
		[CustomComboInfo("Afflatus Feature", "Changes Cure 2 into Afflatus Solace, and Medica into Afflatus Rapture, when lilies are up.", WHM.JobID, WHM.Cure2, WHM.Medica)]
		WhiteMageAfflatusFeature = 77,

		#endregion
		// ====================================================================================
	}
}
