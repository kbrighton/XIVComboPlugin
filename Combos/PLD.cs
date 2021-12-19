namespace XIVComboVX.Combos {
	internal static class PLD {
		public const byte JobID = 19;

		public const uint
			FastBlade = 9,
			RiotBlade = 15,
			RageOfHalone = 21,
			CircleOfScorn = 23,
			ShieldLob = 24,
			SpiritsWithin = 29,
			GoringBlade = 3538,
			RoyalAuthority = 3539,
			TotalEclipse = 7381,
			Requiescat = 7383,
			HolySpirit = 7384,
			Prominence = 16457,
			HolyCircle = 16458,
			Confiteor = 16459,
			Atonement = 16460,
			Intervene = 16461,
			Expiacion = 25747,
			BladeOfFaith = 25748,
			BladeOfTruth = 25749,
			BladeOfValor = 25750;

		public static class Buffs {
			public const ushort
				Requiescat = 1368,
				SwordOath = 1902;
		}

		public static class Debuffs {
			// public const ushort placeholder = 0;
		}

		public static class Levels {
			public const byte
				RiotBlade = 4,
				TotalEclipse = 6,
				SpiritsWithin = 30,
				CircleOfScorn = 50,
				RageOfHalone = 26,
				Prominence = 40,
				GoringBlade = 54,
				RoyalAuthority = 60,
				HolySpirit = 64,
				HolyCircle = 72,
				Intervene = 74,
				Atonement = 76,
				Confiteor = 80,
				Expiacion = 86,
				BladeOfFaith = 90,
				BladeOfTruth = 90,
				BladeOfValor = 90;
		}
	}

	internal class PaladinGoringBladeCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.PldAny;
		protected internal override uint[] ActionIDs { get; } = new[] { PLD.GoringBlade };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is PLD.GoringBlade) {

				if (level >= PLD.Levels.HolySpirit && IsEnabled(CustomComboPreset.PaladinRequiescatFeature) && SelfHasEffect(PLD.Buffs.Requiescat))
					return PLD.HolySpirit;

				bool doMainCombo = IsEnabled(CustomComboPreset.PaladinGoringBladeCombo);

				if (comboTime > 0 && doMainCombo) {

					if (lastComboMove is PLD.RiotBlade && level >= PLD.Levels.GoringBlade)
						return PLD.GoringBlade;

					if (lastComboMove == PLD.FastBlade && level >= PLD.Levels.RiotBlade)
						return PLD.RiotBlade;

				}

				if (level >= PLD.Levels.Atonement && IsEnabled(CustomComboPreset.PaladinAtonementFeature) && SelfHasEffect(PLD.Buffs.SwordOath))
					return PLD.Atonement;

				if (doMainCombo)
					return PLD.FastBlade;
			}

			return actionID;
		}
	}

	internal class PaladinRoyalAuthorityCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.PldAny;
		protected internal override uint[] ActionIDs { get; } = new[] { PLD.RageOfHalone, PLD.RoyalAuthority };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is PLD.RoyalAuthority or PLD.RageOfHalone) {

				if (level >= PLD.Levels.HolySpirit && IsEnabled(CustomComboPreset.PaladinRequiescatFeature) && SelfHasEffect(PLD.Buffs.Requiescat))
					return PLD.HolySpirit;

				bool doMainCombo = IsEnabled(CustomComboPreset.PaladinRoyalAuthorityCombo);

				if (comboTime > 0 && doMainCombo) {

					if (lastComboMove == PLD.RiotBlade && level >= PLD.Levels.RageOfHalone)
						return OriginalHook(PLD.RageOfHalone);

					if (lastComboMove == PLD.FastBlade && level >= PLD.Levels.RiotBlade)
						return PLD.RiotBlade;

				}

				if (level >= PLD.Levels.Atonement && IsEnabled(CustomComboPreset.PaladinAtonementFeature) && SelfHasEffect(PLD.Buffs.SwordOath))
					return PLD.Atonement;

				if (doMainCombo)
					return PLD.FastBlade;
			}

			return actionID;
		}
	}

	internal class PaladinProminenceCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.PldAny;
		protected internal override uint[] ActionIDs { get; } = new[] { PLD.Prominence };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is PLD.Prominence) {

				if (level >= PLD.Levels.HolyCircle && IsEnabled(CustomComboPreset.PaladinRequiescatFeature) && SelfHasEffect(PLD.Buffs.Requiescat))
					return PLD.HolyCircle;

				if (IsEnabled(CustomComboPreset.PaladinProminenceCombo))
					return SimpleChainCombo(level, lastComboMove, comboTime, (PLD.Levels.TotalEclipse, PLD.TotalEclipse),
						(PLD.Levels.Prominence, PLD.Prominence)
					);

			}

			return actionID;
		}
	}

	internal class PaladinRequiescatCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.PaladinRequiescatConfiteorCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { PLD.Requiescat };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is PLD.Requiescat) {

				if (comboTime > 0) {

					if (lastComboMove is PLD.BladeOfTruth && level >= PLD.Levels.BladeOfValor)
						return PLD.BladeOfValor;

					if (lastComboMove is PLD.BladeOfFaith && level >= PLD.Levels.BladeOfTruth)
						return PLD.BladeOfTruth;

					if (lastComboMove is PLD.Confiteor && level >= PLD.Levels.BladeOfFaith)
						return PLD.BladeOfFaith;

				}

				if (level >= PLD.Levels.Confiteor && SelfHasEffect(PLD.Buffs.Requiescat))
					return PLD.Confiteor;

			}

			return actionID;
		}
	}

	internal class PaladinInterveneSyncFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.PaladinInterveneSyncFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { PLD.Intervene };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is PLD.Intervene && level < PLD.Levels.Intervene)
				return PLD.ShieldLob;

			return actionID;
		}
	}
}
