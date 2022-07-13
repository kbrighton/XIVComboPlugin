namespace XIVComboVX.Combos;

using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.JobGauge.Types;

internal static class NIN {
	public const byte JobID = 30;

	public const uint
		SpinningEdge = 2240,
		GustSlash = 2242,
		Hide = 2245,
		Assassinate = 8814,
		ThrowingDagger = 2247,
		Mug = 2248,
		DeathBlossom = 2254,
		AeolianEdge = 2255,
		TrickAttack = 2258,
		Ninjutsu = 2260,
		Chi = 2261,
		JinNormal = 2263,
		Kassatsu = 2264,
		ArmorCrush = 3563,
		DreamWithinADream = 3566,
		TenChiJin = 7403,
		HakkeMujinsatsu = 16488,
		Meisui = 16489,
		Jin = 18807,
		Bunshin = 16493,
		Huraijin = 25876,
		PhantomKamaitachi = 25774,
		ForkedRaiju = 25777,
		FleetingRaiju = 25778;

	public static class Buffs {
		public const ushort
			Mudra = 496,
			Kassatsu = 497,
			Suiton = 507,
			Hidden = 614,
			Bunshin = 1954,
			RaijuReady = 2690;
	}

	public static class Debuffs {
		// public const ushort placeholder = 0;
	}

	public static class Levels {
		public const byte
			GustSlash = 4,
			Hide = 10,
			ThrowingDagger = 15,
			Mug = 15,
			AeolianEdge = 26,
			Ninjitsu = 30,
			Suiton = 45,
			HakkeMujinsatsu = 52,
			ArmorCrush = 54,
			Huraijin = 60,
			TenChiJin = 70,
			Meisui = 72,
			EnhancedKassatsu = 76,
			Bunshin = 80,
			PhantomKamaitachi = 82,
			Raiju = 90;
	}
}

internal class NinjaArmorCrushCombo: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.NinjaArmorCrushCombo;
	public override uint[] ActionIDs { get; } = new[] { NIN.ArmorCrush };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= NIN.Levels.Ninjitsu) {
			if (IsEnabled(CustomComboPreset.NinjaGCDNinjutsuFeature) && SelfHasEffect(NIN.Buffs.Mudra))
				return OriginalHook(NIN.Ninjutsu);
		}

		if (level >= NIN.Levels.Raiju) {
			if (SelfHasEffect(NIN.Buffs.RaijuReady)) {

				if (IsEnabled(CustomComboPreset.NinjaArmorCrushFleetingRaijuFeature))
					return NIN.FleetingRaiju;

				if (IsEnabled(CustomComboPreset.NinjaArmorCrushForkedRaijuFeature))
					return NIN.ForkedRaiju;

			}
		}

		if (lastComboMove is NIN.GustSlash) {
			if (level >= NIN.Levels.ArmorCrush)
				return NIN.ArmorCrush;
			else if (IsEnabled(CustomComboPreset.NinjaArmorCrushFallbackFeature) && level >= NIN.Levels.AeolianEdge)
				return NIN.AeolianEdge;
		}

		if (lastComboMove is NIN.SpinningEdge) {
			if (level >= NIN.Levels.GustSlash)
				return NIN.GustSlash;
		}

		return IsEnabled(CustomComboPreset.NinjaArmorCrushThrowingDaggerFeature) && level >= NIN.Levels.ThrowingDagger && TargetDistance is > 3 and <= 20
			? NIN.ThrowingDagger
			: NIN.SpinningEdge;
	}
}

internal class NinjaAeolianEdgeCombo: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.NinjaAeolianEdgeCombo;
	public override uint[] ActionIDs { get; } = new[] { NIN.AeolianEdge };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (IsEnabled(CustomComboPreset.NinjaGCDNinjutsuFeature)) {
			if (level >= NIN.Levels.Ninjitsu) {
				if (SelfHasEffect(NIN.Buffs.Mudra))
					return OriginalHook(NIN.Ninjutsu);
			}
		}

		if (level >= NIN.Levels.Raiju) {
			if (SelfHasEffect(NIN.Buffs.RaijuReady)) {

				if (IsEnabled(CustomComboPreset.NinjaAeolianEdgeFleetingRaijuFeature))
					return NIN.FleetingRaiju;

				if (IsEnabled(CustomComboPreset.NinjaAeolianEdgeForkedRaijuFeature))
					return NIN.ForkedRaiju;

			}
		}

		if (IsEnabled(CustomComboPreset.NinjaAeolianEdgeHutonFeature)) {
			NINGauge gauge = GetJobGauge<NINGauge>();

			if (level >= NIN.Levels.Huraijin && gauge.HutonTimer <= 0)
				return NIN.Huraijin;

			if (level >= NIN.Levels.ArmorCrush) {
				if (lastComboMove is NIN.GustSlash && gauge.HutonTimer <= Service.Configuration.NinjaHutonThresholdTime * 1000)
					return NIN.ArmorCrush;
			}
		}

		if (level >= NIN.Levels.AeolianEdge) {
			if (lastComboMove is NIN.GustSlash)
				return NIN.AeolianEdge;
		}
		if (level >= NIN.Levels.GustSlash) {
			if (lastComboMove is NIN.SpinningEdge)
				return NIN.GustSlash;
		}
		return IsEnabled(CustomComboPreset.NinjaAeolianEdgeThrowingDaggerFeature) && level >= NIN.Levels.ThrowingDagger && TargetDistance is > 3 and <= 20
			? NIN.ThrowingDagger
			: NIN.SpinningEdge;
	}
}

internal class NinjaHakkeMujinsatsuCombo: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.NinjaHakkeMujinsatsuCombo;
	public override uint[] ActionIDs { get; } = new[] { NIN.HakkeMujinsatsu };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (IsEnabled(CustomComboPreset.NinjaGCDNinjutsuFeature)) {
			if (SelfHasEffect(NIN.Buffs.Mudra))
				return OriginalHook(NIN.Ninjutsu);
		}

		if (level >= NIN.Levels.HakkeMujinsatsu) {
			if (lastComboMove is NIN.DeathBlossom)
				return NIN.HakkeMujinsatsu;
		}

		return NIN.DeathBlossom;
	}
}

internal class NinjaKassatsuTrickFeature: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.NinjaKassatsuTrickFeature;
	public override uint[] ActionIDs { get; } = new[] { NIN.Kassatsu };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= NIN.Levels.Hide) {
			if (SelfHasEffect(NIN.Buffs.Hidden))
				return NIN.TrickAttack;
		}

		if (level >= NIN.Levels.Suiton) {
			if (SelfHasEffect(NIN.Buffs.Suiton))
				return NIN.TrickAttack;
		}

		return actionID;
	}
}

internal class NinjaHideMugFeature: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.NinjaSmartHideFeature;
	public override uint[] ActionIDs { get; } = new[] { NIN.Hide };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= NIN.Levels.Hide) {
			if (SelfHasEffect(NIN.Buffs.Hidden) && HasTarget)
				return NIN.TrickAttack;
		}

		if (level >= NIN.Levels.Suiton) {
			if (SelfHasEffect(NIN.Buffs.Suiton))
				return NIN.TrickAttack;
		}

		if (level >= NIN.Levels.Mug) {
			if (HasCondition(ConditionFlag.InCombat))
				return NIN.Mug;
		}

		return actionID;
	}
}

internal class NinjaKassatsuChiJinFeature: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.NinjaKassatsuChiJinFeature;
	public override uint[] ActionIDs { get; } = new[] { NIN.Chi };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= NIN.Levels.EnhancedKassatsu) {
			if (SelfHasEffect(NIN.Buffs.Kassatsu))
				return NIN.Jin;
		}

		return actionID;
	}
}

internal class NinjaTCJMeisuiFeature: CustomCombo {
	public override CustomComboPreset Preset => CustomComboPreset.NinjaTCJMeisuiFeature;
	public override uint[] ActionIDs { get; } = new[] { NIN.TenChiJin };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= NIN.Levels.Meisui) {
			if (SelfHasEffect(NIN.Buffs.Suiton))
				return NIN.Meisui;
		}

		return actionID;
	}
}

internal class NinjaHuraijinFeatures: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.NinAny;
	public override uint[] ActionIDs { get; } = new[] { NIN.Huraijin };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (IsEnabled(CustomComboPreset.NinjaGCDNinjutsuFeature)) {
			if (SelfHasEffect(NIN.Buffs.Mudra))
				return OriginalHook(NIN.Ninjutsu);
		}

		if (level >= NIN.Levels.Raiju) {
			if (SelfHasEffect(NIN.Buffs.RaijuReady)) {

				if (IsEnabled(CustomComboPreset.NinjaHuraijinForkedRaijuFeature))
					return NIN.ForkedRaiju;

				if (IsEnabled(CustomComboPreset.NinjaHuraijinFleetingRaijuFeature))
					return NIN.FleetingRaiju;

			}
		}

		if (level >= NIN.Levels.ArmorCrush) {
			if (IsEnabled(CustomComboPreset.NinjaHuraijinCrushFeature) && lastComboMove is NIN.GustSlash)
				return NIN.ArmorCrush;
		}

		return actionID;
	}
}
