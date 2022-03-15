using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Statuses;

using XIVComboVX;
using XIVComboVX.Combos;

namespace XIVComboExpandedPlugin.Combos {
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
			Gibbet = 24382,
			Gallows = 24383,
			Guillotine = 24384,
			BloodStalk = 24389,
			UnveiledGibbet = 24390,
			UnveiledGallows = 24391,
			GrimSwathe = 24392,
			Gluttony = 24393,
			VoidReaping = 24395,
			CrossReaping = 24396,
			// Generators
			SoulSlice = 24380,
			SoulScythe = 24381,
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
			Harpe = 24386,
			Soulsow = 24387,
			HarvestMoon = 24388,
			HellsIngress = 24401,
			HellsEgress = 24402,
			Regress = 24403;

		public static class Buffs {
			public const ushort
				EnhancedHarpe = 2845,
				SoulReaver = 2587,
				EnhancedGibbet = 2588,
				EnhancedGallows = 2589,
				EnhancedVoidReaping = 2590,
				EnhancedCrossReaping = 2591,
				ImmortalSacrifice = 2592,
				Enshrouded = 2593,
				Soulsow = 2594,
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
				BloodStalk = 50,
				GrimSwathe = 55,
				SoulSlice = 60,
				SoulScythe = 65,
				SoulReaver = 70,
				Regress = 74,
				Gluttony = 76,
				Enshroud = 80,
				Soulsow = 82,
				HarvestMoon = 82,
				EnhancedShroud = 86,
				LemuresScythe = 86,
				PlentifulHarvest = 88,
				Communio = 90;
		}
	}

	internal class ReaperSlice: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.RprAny;
		public override uint[] ActionIDs => new[] { RPR.InfernalSlice };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (IsEnabled(CustomComboPreset.ReaperSliceSoulsowFeature)) {
				if (level >= RPR.Levels.Soulsow && !InCombat && !SelfHasEffect(RPR.Buffs.Soulsow))
					return RPR.Soulsow;
			}

			RPRGauge gauge = GetJobGauge<RPRGauge>();

			if (level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0) {

				if (IsEnabled(CustomComboPreset.ReaperSliceLemuresFeature)) {
					if (level >= RPR.Levels.EnhancedShroud && gauge.VoidShroud >= 2)
						return RPR.LemuresSlice;
				}

				if (IsEnabled(CustomComboPreset.ReaperSliceCommunioFeature)) {
					if (level >= RPR.Levels.Communio && gauge.LemureShroud == 1 && gauge.VoidShroud == 0)
						return RPR.Communio;
				}

			}

			if ((level >= RPR.Levels.SoulReaver && SelfHasEffect(RPR.Buffs.SoulReaver))
				|| (level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0)
			) {

				if (IsEnabled(CustomComboPreset.ReaperSliceEnhancedEnshroudedFeature)) {

					if (SelfHasEffect(RPR.Buffs.EnhancedVoidReaping))
						return RPR.VoidReaping;

					if (SelfHasEffect(RPR.Buffs.EnhancedCrossReaping))
						return RPR.CrossReaping;

				}

				if (IsEnabled(CustomComboPreset.ReaperSliceEnhancedSoulReaverFeature)) {

					if (SelfHasEffect(RPR.Buffs.EnhancedGibbet))
						// Void Reaping
						return OriginalHook(RPR.Gibbet);

					if (SelfHasEffect(RPR.Buffs.EnhancedGallows))
						// Cross Reaping
						return OriginalHook(RPR.Gallows);

				}

				if (IsEnabled(CustomComboPreset.ReaperSliceGibbetFeature))
					// Void Reaping
					return OriginalHook(RPR.Gibbet);

				if (IsEnabled(CustomComboPreset.ReaperSliceGallowsFeature))
					// Cross Reaping
					return OriginalHook(RPR.Gallows);
			}

			if (IsEnabled(CustomComboPreset.ReaperSliceCombo)) {
				return SimpleChainCombo(level, lastComboMove, comboTime, (1, RPR.Slice),
					(RPR.Levels.WaxingSlice, RPR.WaxingSlice),
					(RPR.Levels.InfernalSlice, RPR.InfernalSlice)
				);
			}

			return actionID;
		}
	}

	internal class ReaperScythe: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.RprAny;
		public override uint[] ActionIDs => new[] { RPR.NightmareScythe };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			RPRGauge gauge = GetJobGauge<RPRGauge>();

			if (level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0) {

				if (IsEnabled(CustomComboPreset.ReaperScytheLemuresFeature)) {
					if (level >= RPR.Levels.LemuresScythe && gauge.VoidShroud >= 2)
						return RPR.LemuresScythe;
				}

				if (IsEnabled(CustomComboPreset.ReaperScytheCommunioFeature)) {
					if (level >= RPR.Levels.Communio && gauge.LemureShroud == 1 && gauge.VoidShroud == 0)
						return RPR.Communio;
				}

			}

			if (IsEnabled(CustomComboPreset.ReaperScytheGuillotineFeature)) {
				if ((level >= RPR.Levels.SoulReaver && SelfHasEffect(RPR.Buffs.SoulReaver))
					|| (level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0)
				) {
					// Grim Reaping
					return OriginalHook(RPR.Guillotine);
				}
			}

			if (IsEnabled(CustomComboPreset.ReaperScytheHarvestMoonFeature)) {
				if (level >= RPR.Levels.HarvestMoon && SelfHasEffect(RPR.Buffs.Soulsow) && HasTarget)
					return RPR.HarvestMoon;
			}

			if (IsEnabled(CustomComboPreset.ReaperScytheSoulsowFeature)) {
				if (level >= RPR.Levels.Soulsow && !InCombat && !SelfHasEffect(RPR.Buffs.Soulsow))
					return RPR.Soulsow;
			}

			if (IsEnabled(CustomComboPreset.ReaperScytheCombo)) {
				return SimpleChainCombo(level, lastComboMove, comboTime, (RPR.Levels.SpinningScythe, RPR.SpinningScythe),
					(RPR.Levels.NightmareScythe, RPR.NightmareScythe)
				);
			}

			return actionID;
		}
	}

	internal class ReaperShadowOfDeath: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.RprAny;
		public override uint[] ActionIDs => new[] { RPR.ShadowOfDeath };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (IsEnabled(CustomComboPreset.ReaperShadowSoulsowFeature)) {
				if (level >= RPR.Levels.Soulsow && !InCombat && !HasTarget && !SelfHasEffect(RPR.Buffs.Soulsow))
					return RPR.Soulsow;
			}

			RPRGauge gauge = GetJobGauge<RPRGauge>();

			if (level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0) {

				if (IsEnabled(CustomComboPreset.ReaperShadowLemuresFeature)) {
					if (level >= RPR.Levels.EnhancedShroud && gauge.VoidShroud >= 2)
						return RPR.LemuresSlice;
				}

				if (IsEnabled(CustomComboPreset.ReaperShadowCommunioFeature)) {
					if (level >= RPR.Levels.Communio && gauge.LemureShroud == 1 && gauge.VoidShroud == 0)
						return RPR.Communio;
				}

			}

			if ((level >= RPR.Levels.SoulReaver && SelfHasEffect(RPR.Buffs.SoulReaver))
				|| (level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0)
			) {

				if (IsEnabled(CustomComboPreset.ReaperShadowGallowsFeature))
					// Cross Reaping
					return OriginalHook(RPR.Gallows);

				if (IsEnabled(CustomComboPreset.ReaperShadowGibbetFeature))
					// Void Reaping
					return OriginalHook(RPR.Gibbet);

			}

			return actionID;
		}
	}

	internal class ReaperSoulSlice: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.RprAny;
		public override uint[] ActionIDs => new[] { RPR.SoulSlice };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			RPRGauge gauge = GetJobGauge<RPRGauge>();

			if (level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0) {

				if (IsEnabled(CustomComboPreset.ReaperSoulLemuresFeature)) {
					if (level >= RPR.Levels.EnhancedShroud && gauge.VoidShroud >= 2)
						return RPR.LemuresSlice;
				}

				if (IsEnabled(CustomComboPreset.ReaperSoulCommunioFeature)) {
					if (level >= RPR.Levels.Communio && gauge.LemureShroud == 1 && gauge.VoidShroud == 0)
						return RPR.Communio;
				}

			}

			if ((level >= RPR.Levels.SoulReaver && SelfHasEffect(RPR.Buffs.SoulReaver))
				|| (level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0)
			) {

				if (IsEnabled(CustomComboPreset.ReaperSoulGallowsFeature))
					// Cross Reaping
					return OriginalHook(RPR.Gallows);

				if (IsEnabled(CustomComboPreset.ReaperSoulGibbetFeature))
					// Void Reaping
					return OriginalHook(RPR.Gibbet);

			}

			if (IsEnabled(CustomComboPreset.ReaperSoulOvercapFeature)) {

				if (IsEnabled(CustomComboPreset.ReaperBloodStalkGluttonyFeature)) {
					if (level >= RPR.Levels.Gluttony && gauge.Soul >= 50 && gauge.EnshroudedTimeRemaining == 0 && IsOffCooldown(RPR.Gluttony))
						return RPR.Gluttony;
				}

				if (level >= RPR.Levels.BloodStalk && gauge.Soul > 50 && gauge.EnshroudedTimeRemaining == 0)
					// Unveiled Gibbet and Gallows
					return OriginalHook(RPR.BloodStalk);

			}

			return actionID;
		}
	}

	internal class ReaperSoulScythe: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperSoulScytheOvercapFeature;
		public override uint[] ActionIDs => new[] { RPR.SoulScythe };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			RPRGauge gauge = GetJobGauge<RPRGauge>();

			if (IsEnabled(CustomComboPreset.ReaperGrimSwatheGluttonyFeature)) {
				if (level >= RPR.Levels.Gluttony && gauge.Soul >= 50 && gauge.EnshroudedTimeRemaining == 0 && IsOffCooldown(RPR.Gluttony))
					return RPR.Gluttony;
			}

			if (level >= RPR.Levels.GrimSwathe && gauge.Soul > 50 && gauge.EnshroudedTimeRemaining == 0)
				return RPR.GrimSwathe;

			return actionID;
		}
	}

	internal class ReaperBloodStalk: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperBloodStalkGluttonyFeature;
		public override uint[] ActionIDs => new[] { RPR.BloodStalk };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			RPRGauge gauge = GetJobGauge<RPRGauge>();

			if (level >= RPR.Levels.Gluttony && gauge.Soul >= 50 && IsOffCooldown(RPR.Gluttony))
				return RPR.Gluttony;

			return actionID;
		}
	}

	internal class ReaperGrimSwathe: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperGrimSwatheGluttonyFeature;
		public override uint[] ActionIDs => new[] { RPR.GrimSwathe };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			RPRGauge gauge = GetJobGauge<RPRGauge>();

			if (level >= RPR.Levels.Gluttony && gauge.Soul >= 50 && IsOffCooldown(RPR.Gluttony))
				return RPR.Gluttony;

			return actionID;
		}
	}

	internal class ReaperGibbetGallows: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.RprAny;
		public override uint[] ActionIDs => new[] { RPR.Gibbet, RPR.Gallows };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			RPRGauge gauge = GetJobGauge<RPRGauge>();

			if ((level >= RPR.Levels.SoulReaver && SelfHasEffect(RPR.Buffs.SoulReaver))
				|| (level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0)
			) {

				if (IsEnabled(CustomComboPreset.ReaperLemuresSoulReaverFeature)) {
					if (level >= RPR.Levels.EnhancedShroud && gauge.VoidShroud >= 2)
						return RPR.LemuresSlice;
				}

				if (IsEnabled(CustomComboPreset.ReaperCommunioSoulReaverFeature)) {
					if (level >= RPR.Levels.Communio && gauge.LemureShroud == 1 && gauge.VoidShroud == 0)
						return RPR.Communio;
				}

				if (IsEnabled(CustomComboPreset.ReaperEnhancedEnshroudedFeature)) {

					if (SelfHasEffect(RPR.Buffs.EnhancedVoidReaping))
						return RPR.VoidReaping;

					if (SelfHasEffect(RPR.Buffs.EnhancedCrossReaping))
						return RPR.CrossReaping;

				}

				if (IsEnabled(CustomComboPreset.ReaperEnhancedSoulReaverFeature)) {

					if (SelfHasEffect(RPR.Buffs.EnhancedGibbet))
						// Void Reaping
						return OriginalHook(RPR.Gibbet);

					if (SelfHasEffect(RPR.Buffs.EnhancedGallows))
						// Cross Reaping
						return OriginalHook(RPR.Gallows);

				}
			}

			return actionID;
		}
	}

	internal class ReaperGuillotine: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.RprAny;
		public override uint[] ActionIDs => new[] { RPR.Guillotine };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			RPRGauge gauge = GetJobGauge<RPRGauge>();

			if (level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0) {

				if (IsEnabled(CustomComboPreset.ReaperCommunioSoulReaverFeature)) {
					if (level >= RPR.Levels.Communio && gauge.LemureShroud == 1 && gauge.VoidShroud == 0)
						return RPR.Communio;
				}

				if (IsEnabled(CustomComboPreset.ReaperLemuresSoulReaverFeature)) {
					if (level >= RPR.Levels.LemuresScythe && gauge.VoidShroud >= 2)
						return RPR.LemuresScythe;
				}

			}

			return actionID;
		}
	}

	internal class ReaperEnshroud: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperEnshroudCommunioFeature;
		public override uint[] ActionIDs => new[] { RPR.Enshroud };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			RPRGauge gauge = GetJobGauge<RPRGauge>();

			if (level >= RPR.Levels.Communio && gauge.EnshroudedTimeRemaining > 0)
				return RPR.Communio;

			return actionID;
		}
	}

	internal class ReaperArcaneCircle: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperHarvestFeature;
		public override uint[] ActionIDs => new[] { RPR.ArcaneCircle };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= RPR.Levels.PlentifulHarvest && SelfHasEffect(RPR.Buffs.ImmortalSacrifice))
				return RPR.PlentifulHarvest;

			return actionID;
		}
	}

	internal class ReaperHellsIngressEgress: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperRegressFeature;
		public override uint[] ActionIDs => new[] { RPR.HellsEgress, RPR.HellsIngress };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (level >= RPR.Levels.Regress) {
				Status? threshold = SelfFindEffect(RPR.Buffs.Threshold);
				if (threshold is not null) {
					if (IsEnabled(CustomComboPreset.ReaperRegressDelayed)) {
						if (threshold.RemainingTime <= Service.Configuration.ReaperThresholdBuffTime)
							return RPR.Regress;
					}
					else {
						return RPR.Regress;
					}
				}
			}

			return actionID;
		}
	}

	internal class ReaperHarpe: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.RprAny;
		public override uint[] ActionIDs => new[] { RPR.Harpe };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

			if (IsEnabled(CustomComboPreset.ReaperHarpeHarvestSoulsowFeature)) {
				if (level >= RPR.Levels.Soulsow && !SelfHasEffect(RPR.Buffs.Soulsow) && (!InCombat || !HasTarget))
					return RPR.Soulsow;
			}

			if (IsEnabled(CustomComboPreset.ReaperHarpeHarvestMoonFeature)) {
				if (level >= RPR.Levels.HarvestMoon && SelfHasEffect(RPR.Buffs.Soulsow)) {

					if (IsEnabled(CustomComboPreset.ReaperHarpeHarvestMoonEnhancedFeature)) {
						if (SelfHasEffect(RPR.Buffs.EnhancedHarpe))
							return RPR.Harpe;
					}

					if (IsEnabled(CustomComboPreset.ReaperHarpeHarvestMoonCombatFeature)) {
						if (!InCombat)
							return RPR.Harpe;
					}

					return RPR.HarvestMoon;
				}
			}

			return actionID;
		}
	}
}
