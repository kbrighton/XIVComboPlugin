using Dalamud.Game.ClientState.Statuses;

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
				SwordOath = 1902,
				BladeOfFaithReady = 3019;
		}

		public static class Debuffs {
			public const ushort
				GoringBlade = 725;
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

	internal class PaladinStunInterruptFeature: StunInterruptCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.PaladinStunInterruptFeature;
	}

	internal class PaladinGoringBlade: CustomCombo {
		public override CustomComboPreset Preset => CustomComboPreset.PaladinGoringBladeCombo;
		public override uint[] ActionIDs { get; } = new[] { PLD.GoringBlade };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= PLD.Levels.HolySpirit && IsEnabled(CustomComboPreset.PaladinRequiescatFeature) && SelfHasEffect(PLD.Buffs.Requiescat))
				return PLD.HolySpirit;

			uint nextMainCombo = 0;
			bool inMainCombo = comboTime > 0
				&& PartialChainCombo(level, lastComboMove, out nextMainCombo, (1, PLD.FastBlade),
					(PLD.Levels.RiotBlade, PLD.RiotBlade),
					(PLD.Levels.GoringBlade, PLD.GoringBlade)
				);

			if (level >= PLD.Levels.Atonement && !inMainCombo && IsEnabled(CustomComboPreset.PaladinAtonementFeature) && SelfHasEffect(PLD.Buffs.SwordOath))
				return PLD.Atonement;

			return inMainCombo
				? nextMainCombo
				: PLD.FastBlade;
		}
	}

	internal class PaladinRoyalAuthorityCombo: CustomCombo {
		public override CustomComboPreset Preset => CustomComboPreset.PaladinRoyalAuthorityCombo;
		public override uint[] ActionIDs { get; } = new[] { PLD.RageOfHalone, PLD.RoyalAuthority };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= PLD.Levels.HolySpirit && IsEnabled(CustomComboPreset.PaladinRequiescatFeature)) {
				Status? requiescat = SelfFindEffect(PLD.Buffs.Requiescat);

				if (requiescat is not null) {
					if (level >= PLD.Levels.Confiteor && IsEnabled(CustomComboPreset.PaladinConfiteorFeature) && (requiescat?.StackCount == 1 || LocalPlayer?.CurrentMp < 2000))
						return PLD.Confiteor;
					return PLD.HolySpirit;
				}
			}

			if (comboTime > 0) {

				if (lastComboMove == PLD.RiotBlade && level >= PLD.Levels.RageOfHalone) {

					if (IsEnabled(CustomComboPreset.PaladinRoyalAuthorityDoTSaver)) {
						if (TargetOwnEffectDuration(PLD.Debuffs.GoringBlade) < Service.Configuration.PaladinGoringBladeDoTSaverDebuffTime)
							return PLD.GoringBlade;
					}

					return OriginalHook(PLD.RageOfHalone);
				}

				if (lastComboMove == PLD.FastBlade && level >= PLD.Levels.RiotBlade)
					return PLD.RiotBlade;

			}

			if (level >= PLD.Levels.Atonement && IsEnabled(CustomComboPreset.PaladinAtonementFeature) && SelfHasEffect(PLD.Buffs.SwordOath))
				return PLD.Atonement;

			return PLD.FastBlade;
		}
	}

	internal class PaladinProminenceCombo: CustomCombo {
		public override CustomComboPreset Preset => CustomComboPreset.PaladinProminenceCombo;
		public override uint[] ActionIDs { get; } = new[] { PLD.Prominence };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= PLD.Levels.HolyCircle && IsEnabled(CustomComboPreset.PaladinRequiescatFeature)) {
				Status? requiescat = SelfFindEffect(PLD.Buffs.Requiescat);

				if (requiescat is not null) {
					if (level >= PLD.Levels.Confiteor && IsEnabled(CustomComboPreset.PaladinConfiteorFeature) && (requiescat?.StackCount == 1 || LocalPlayer?.CurrentMp < 2000))
						return PLD.Confiteor;
					return PLD.HolyCircle;
				}
			}

			return SimpleChainCombo(level, lastComboMove, comboTime, (PLD.Levels.TotalEclipse, PLD.TotalEclipse),
				(PLD.Levels.Prominence, PLD.Prominence)
			);
		}
	}

	internal class PaladinHolySpiritHolyCircle: CustomCombo {
		public override CustomComboPreset Preset => CustomComboPreset.PaladinConfiteorFeature;
		public override uint[] ActionIDs { get; } = new[] { PLD.HolySpirit, PLD.HolyCircle };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= PLD.Levels.Confiteor && (SelfFindEffect(PLD.Buffs.Requiescat)?.StackCount == 1 || LocalPlayer?.CurrentMp < 2000))
				return PLD.Confiteor;

			return actionID;
		}
	}

	internal class PaladinRequiescatCombo: CustomCombo {
		public override CustomComboPreset Preset => CustomComboPreset.PaladinRequiescatConfiteorCombo;
		public override uint[] ActionIDs { get; } = new[] { PLD.Requiescat };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= PLD.Levels.Confiteor && SelfHasEffect(PLD.Buffs.Requiescat))
				return PLD.Confiteor;

			return actionID;
		}
	}

	internal class PaladinInterveneSyncFeature: CustomCombo {
		public override CustomComboPreset Preset => CustomComboPreset.PaladinInterveneSyncFeature;
		public override uint[] ActionIDs { get; } = new[] { PLD.Intervene };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level < PLD.Levels.Intervene)
				return PLD.ShieldLob;

			return actionID;
		}
	}
}
