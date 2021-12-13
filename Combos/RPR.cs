namespace XIVComboVX.Combos {
	internal static class RPR {
		public const byte JobID = 39;

		public const uint
			// Single Target
			Slice = 24373,
			WaxingSlice = 24374,
			InfernalSlice = 24375,
			// AoE
			SpinningScythe = 24376,
			NightmareScythe = 24377,
			// Soul Reaver
			BloodStalk = 24389,
			Gibbet = 24382,
			Gallows = 24383,
			Guillotine = 24384,
			// Sacrifice
			ArcaneCircle = 24405,
			PlentifulHarvest = 24385,
			// Shroud
			Enshroud = 24394,
			Communio = 24398,
			LemuresSlice = 24399,
			LemuresScythe = 24400,
			// Misc
			ShadowOfDeath = 24378,
			HellsIngress = 24401,
			HellsEgress = 24402,
			Regress = 24403;

		public static class Buffs {
			public const ushort
				SoulReaver = 2587,
				ImmortalSacrifice = 2592,
				EnhancedGibbet = 2588,
				EnhancedGallows = 2589,
				EnhancedVoidReaping = 2590,
				EnhancedCrossReaping = 2591,
				Enshrouded = 2593,
				Threshold = 2595;
		}

		public static class Debuffs {
			public const ushort
				Placeholder = 0;
		}

		public static class Levels {
			public const byte
				WaxingSlice = 5,
				HellsIngress = 20,
				HellsEgress = 20,
				SpinningScythe = 25,
				InfernalSlice = 30,
				NightmareScythe = 45,
				SoulReaver = 70,
				Regress = 74,
				Enshroud = 80,
				PlentifulHarvest = 88,
				Communio = 90;
		}
	}

	internal class ReaperSliceCombo: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperSliceCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { RPR.InfernalSlice };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is RPR.InfernalSlice) {

				if (IsEnabled(CustomComboPreset.ReaperSoulReaverGibbetFeature)) {

					if (level >= RPR.Levels.SoulReaver && (SelfHasEffect(RPR.Buffs.SoulReaver) || SelfHasEffect(RPR.Buffs.Enshrouded))) {

						if (IsEnabled(CustomComboPreset.ReaperSoulReaverGibbetOption))
							return OriginalHook(RPR.Gallows);

						return OriginalHook(RPR.Gibbet);
					}
				}

				return SimpleChainCombo(level, lastComboMove, comboTime, (1, RPR.Slice),
					(RPR.Levels.WaxingSlice, RPR.WaxingSlice),
					(RPR.Levels.InfernalSlice, RPR.InfernalSlice)
				);
			}

			return actionID;
		}
	}

	internal class ReaperScytheCombo: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperScytheCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { RPR.NightmareScythe };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is RPR.NightmareScythe) {

				if (IsEnabled(CustomComboPreset.ReaperSoulReaverGuillotineFeature)) {

					if (level >= RPR.Levels.SoulReaver && (SelfHasEffect(RPR.Buffs.SoulReaver) || SelfHasEffect(RPR.Buffs.Enshrouded)))
						return OriginalHook(RPR.Guillotine);
				}

				return SimpleChainCombo(level, lastComboMove, comboTime, (RPR.Levels.SpinningScythe, RPR.SpinningScythe),
					(RPR.Levels.NightmareScythe, RPR.NightmareScythe)
				);
			}

			return actionID;
		}
	}

	internal class ReaperSoulReaverFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperSoulReaverGallowsFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { RPR.ShadowOfDeath };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is RPR.ShadowOfDeath && level >= RPR.Levels.SoulReaver && (SelfHasEffect(RPR.Buffs.SoulReaver) || SelfHasEffect(RPR.Buffs.Enshrouded))) {

				if (IsEnabled(CustomComboPreset.ReaperSoulReaverGallowsOption))
					return OriginalHook(RPR.Gibbet);

				return OriginalHook(RPR.Gallows);
			}

			return actionID;
		}
	}

	internal class ReaperHarvestFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperHarvestFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { RPR.ArcaneCircle };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is RPR.ArcaneCircle && level >= RPR.Levels.PlentifulHarvest && SelfHasEffect(RPR.Buffs.ImmortalSacrifice))
				return RPR.PlentifulHarvest;

			return actionID;
		}
	}

	internal class EnshroudCommunioFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperEnshroudCommunioFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { RPR.Enshroud };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is RPR.Enshroud && level >= RPR.Levels.Communio && SelfHasEffect(RPR.Buffs.Enshrouded))
				return RPR.Communio;

			return actionID;
		}
	}

	internal class ReaperRegressFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperRegressFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { RPR.HellsIngress, RPR.HellsEgress };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is RPR.HellsEgress or RPR.HellsIngress && level >= RPR.Levels.Regress && SelfHasEffect(RPR.Buffs.Threshold))
				return RPR.Regress;

			return actionID;
		}
	}
}
