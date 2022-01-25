/*
 * All credit to daemitus (this was literally edited only enough to compile because I don't play or understand reaper (YET))
 * Original is on dae's repo at https://github.com/daemitus/XIVComboPlugin/blob/master/XIVComboExpanded/Combos/RPR.cs
 * 
 * Someday™ I'll write proper reaper combos for VX, but not today.
 * 
 * UPDATE: The Gluttony feature is mine, at the request of AwesomeJames2120 on github. The rest isn't.
 */

using Dalamud.Game.ClientState.JobGauge.Types;

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
			GrimSwathe = 24392,
			Gluttony = 24393,
			// Soul Reaver
			BloodStalk = 24389,
			Gibbet = 24382,
			Gallows = 24383,
			Guillotine = 24384,
			UnveiledGibbet = 24390,
			UnveiledGallows = 24391,
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
				GrimSwathe = 55,
				SoulReaver = 70,
				Regress = 74,
				Gluttony = 76,
				Enshroud = 80,
				EnhancedShroud = 86,
				LemuresScythe = 86,
				PlentifulHarvest = 88,
				Communio = 90;
		}
	}

	internal abstract class ReaperCustomCombo: CustomCombo {
		public override CustomComboPreset Preset { get; } = CustomComboPreset.RprAny;
	}

	internal class ReaperGluttony: ReaperCustomCombo {
		public override uint[] ActionIDs { get; } = new[] { RPR.GrimSwathe, RPR.BloodStalk, RPR.UnveiledGallows, RPR.UnveiledGibbet };

		protected override uint Invoke(uint actionID, uint lastComboActionId, float comboTime, byte level) {

			if (level >= RPR.Levels.Gluttony) {

				if (actionID is RPR.GrimSwathe && IsEnabled(CustomComboPreset.ReaperGluttonyOnGrimSwatheFeature) && IsOffCooldown(RPR.Gluttony))
					return RPR.Gluttony;

				if (actionID is RPR.BloodStalk or RPR.UnveiledGallows or RPR.UnveiledGibbet) {

					if (IsEnabled(CustomComboPreset.ReaperGluttonyOnUnveiledGallowsFeature)
						&& SelfHasEffect(RPR.Buffs.EnhancedGallows)
						&& IsOffCooldown(RPR.Gluttony))
						return RPR.Gluttony;

					if (IsEnabled(CustomComboPreset.ReaperGluttonyOnUnveiledGibbetFeature)
						&& SelfHasEffect(RPR.Buffs.EnhancedGibbet)
						&& IsOffCooldown(RPR.Gluttony))
						return RPR.Gluttony;

					if (IsEnabled(CustomComboPreset.ReaperGluttonyOnBloodStalkFeature)
						&& !SelfHasEffect(RPR.Buffs.EnhancedGallows)
						&& !SelfHasEffect(RPR.Buffs.EnhancedGibbet)
						&& IsOffCooldown(RPR.Gluttony))
						return RPR.Gluttony;

				}

			}

			return actionID;
		}
	}

	internal class ReaperSlice: ReaperCustomCombo {
		public override uint[] ActionIDs { get; } = new[] { RPR.InfernalSlice };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID == RPR.InfernalSlice) {
				RPRGauge? gauge = GetJobGauge<RPRGauge>();

				if (level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0) {
					if (IsEnabled(CustomComboPreset.ReaperSliceCommunioFeature)) {
						if (level >= RPR.Levels.Communio && gauge.LemureShroud == 1 && gauge.VoidShroud == 0)
							return RPR.Communio;
					}

					if (IsEnabled(CustomComboPreset.ReaperSliceLemuresFeature)) {
						if (level >= RPR.Levels.EnhancedShroud && gauge.VoidShroud >= 2)
							return RPR.LemuresSlice;
					}
				}

				if ((level >= RPR.Levels.SoulReaver && SelfHasEffect(RPR.Buffs.SoulReaver)) ||
					(level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0)) {
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
					if (comboTime > 0) {
						if (lastComboMove == RPR.WaxingSlice && level >= RPR.Levels.InfernalSlice)
							return RPR.InfernalSlice;

						if (lastComboMove == RPR.Slice && level >= RPR.Levels.WaxingSlice)
							return RPR.WaxingSlice;
					}

					return RPR.Slice;
				}
			}

			return actionID;
		}
	}

	internal class ReaperScythe: ReaperCustomCombo {
		public override uint[] ActionIDs { get; } = new[] { RPR.NightmareScythe };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID == RPR.NightmareScythe) {
				RPRGauge? gauge = GetJobGauge<RPRGauge>();

				if (level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0) {
					if (IsEnabled(CustomComboPreset.ReaperScytheCommunioFeature)) {
						if (level >= RPR.Levels.Communio && gauge.LemureShroud == 1 && gauge.VoidShroud == 0)
							return RPR.Communio;
					}

					if (IsEnabled(CustomComboPreset.ReaperScytheLemuresFeature)) {
						if (level >= RPR.Levels.LemuresScythe && gauge.VoidShroud >= 2)
							return RPR.LemuresScythe;
					}
				}

				if (IsEnabled(CustomComboPreset.ReaperScytheGuillotineFeature)) {
					if ((level >= RPR.Levels.SoulReaver && SelfHasEffect(RPR.Buffs.SoulReaver)) ||
						(level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0))
						// Grim Reaping
						return OriginalHook(RPR.Guillotine);
				}

				if (IsEnabled(CustomComboPreset.ReaperScytheCombo)) {
					if (comboTime > 0) {
						if (lastComboMove == RPR.SpinningScythe && level >= RPR.Levels.NightmareScythe)
							return RPR.NightmareScythe;
					}

					return RPR.SpinningScythe;
				}
			}

			return actionID;
		}
	}

	internal class ReaperShadowOfDeath: ReaperCustomCombo {
		public override uint[] ActionIDs { get; } = new[] { RPR.ShadowOfDeath };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID == RPR.ShadowOfDeath) {
				RPRGauge? gauge = GetJobGauge<RPRGauge>();

				if (level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0) {
					if (IsEnabled(CustomComboPreset.ReaperShadowCommunioFeature)) {
						if (level >= RPR.Levels.Communio && gauge.LemureShroud == 1 && gauge.VoidShroud == 0)
							return RPR.Communio;
					}

					if (IsEnabled(CustomComboPreset.ReaperShadowLemuresFeature)) {
						if (level >= RPR.Levels.EnhancedShroud && gauge.VoidShroud >= 2)
							return RPR.LemuresSlice;
					}
				}

				if ((level >= RPR.Levels.SoulReaver && SelfHasEffect(RPR.Buffs.SoulReaver)) ||
					(level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0)) {
					if (IsEnabled(CustomComboPreset.ReaperShadowGallowsFeature))
						// Cross Reaping
						return OriginalHook(RPR.Gallows);

					if (IsEnabled(CustomComboPreset.ReaperShadowGibbetFeature))
						// Void Reaping
						return OriginalHook(RPR.Gibbet);
				}
			}

			return actionID;
		}
	}

	internal class ReaperSoulSlice: ReaperCustomCombo {
		public override uint[] ActionIDs { get; } = new[] { RPR.SoulSlice };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID == RPR.SoulSlice) {
				RPRGauge? gauge = GetJobGauge<RPRGauge>();

				if (level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0) {
					if (IsEnabled(CustomComboPreset.ReaperSoulCommunioFeature)) {
						if (level >= RPR.Levels.Communio && gauge.LemureShroud == 1 && gauge.VoidShroud == 0)
							return RPR.Communio;
					}

					if (IsEnabled(CustomComboPreset.ReaperSoulLemuresFeature)) {
						if (level >= RPR.Levels.EnhancedShroud && gauge.VoidShroud >= 2)
							return RPR.LemuresSlice;
					}
				}

				if ((level >= RPR.Levels.SoulReaver && SelfHasEffect(RPR.Buffs.SoulReaver)) ||
					(level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0)) {
					if (IsEnabled(CustomComboPreset.ReaperSoulGallowsFeature))
						// Cross Reaping
						return OriginalHook(RPR.Gallows);

					if (IsEnabled(CustomComboPreset.ReaperSoulGibbetFeature))
						// Void Reaping
						return OriginalHook(RPR.Gibbet);
				}
			}

			return actionID;
		}
	}

	internal class ReaperGibbetGallowsGuillotine: ReaperCustomCombo {
		public override uint[] ActionIDs { get; } = new[] { RPR.Gibbet, RPR.Gallows, RPR.Guillotine };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is RPR.Gibbet or RPR.Gallows) {
				RPRGauge? gauge = GetJobGauge<RPRGauge>();

				if ((level >= RPR.Levels.SoulReaver && SelfHasEffect(RPR.Buffs.SoulReaver)) ||
					(level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0)) {
					if (IsEnabled(CustomComboPreset.ReaperCommunioSoulReaverFeature)) {
						if (level >= RPR.Levels.Communio && gauge.LemureShroud == 1 && gauge.VoidShroud == 0)
							return RPR.Communio;
					}

					if (IsEnabled(CustomComboPreset.ReaperLemuresSoulReaverFeature)) {
						if (level >= RPR.Levels.EnhancedShroud && gauge.VoidShroud >= 2)
							return RPR.LemuresSlice;
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
			}

			if (actionID == RPR.Guillotine) {
				RPRGauge? gauge = GetJobGauge<RPRGauge>();

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
			}

			return actionID;
		}
	}

	internal class ReaperEnshroud: ReaperCustomCombo {
		public override uint[] ActionIDs { get; } = new[] { RPR.Enshroud };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID == RPR.Enshroud) {
				RPRGauge? gauge = GetJobGauge<RPRGauge>();

				if (IsEnabled(CustomComboPreset.ReaperEnshroudCommunioFeature)) {
					if (level >= RPR.Levels.Communio && gauge.EnshroudedTimeRemaining > 0)
						return RPR.Communio;
				}
			}

			return actionID;
		}
	}

	internal class ReaperArcaneCircle: ReaperCustomCombo {
		public override uint[] ActionIDs { get; } = new[] { RPR.ArcaneCircle };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID == RPR.ArcaneCircle) {
				if (IsEnabled(CustomComboPreset.ReaperHarvestFeature)) {
					if (level >= RPR.Levels.PlentifulHarvest && SelfHasEffect(RPR.Buffs.ImmortalSacrifice))
						return RPR.PlentifulHarvest;
				}
			}

			return actionID;
		}
	}

	internal class ReaperHellsBigress: ReaperCustomCombo {
		public override uint[] ActionIDs { get; } = new[] { RPR.HellsIngress, RPR.HellsEgress };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is RPR.HellsEgress or RPR.HellsIngress) {
				if (IsEnabled(CustomComboPreset.ReaperRegressFeature)) {
					if (level >= RPR.Levels.Regress && SelfHasEffect(RPR.Buffs.Threshold))
						return RPR.Regress;
				}
			}

			return actionID;
		}
	}
}
