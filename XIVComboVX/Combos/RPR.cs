using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Statuses;

namespace PrincessRTFM.XIVComboVX.Combos;

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
		VoidReaping = 24395,
		CrossReaping = 24396,
		// Executioner
		Gluttony = 24393,
		ExecutionersGibbet = 36970,
		ExecutionersGallows = 36971,
		ExecutionersGuillotine = 36972,
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
		WhorlOfDeath = 24379,
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
			Threshold = 2595,
			Oblatio = 3857,
			Executioner = 3858,
			IdealHost = 3905,
			PerfectioOcculta = 3859,
			PerfectioParata = 3860;
	}

	public static class Debuffs {
		public const ushort
			DeathsDesign = 2586;
	}

	public static class Levels {
		public const byte
			WaxingSlice = 5,
			ShadowOfDeath = 10,
			HellsIngress = 20,
			HellsEgress = 20,
			SpinningScythe = 25,
			InfernalSlice = 30,
			WhorlOfDeath = 35,
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
			Communio = 90,
			Sacrificium = 92,
			Executions = 96,
			Perfectio = 100;
	}
}

internal class ReaperBloodbathReplacer: SecondBloodbathCombo {
	public override CustomComboPreset Preset => CustomComboPreset.ReaperBloodbathReplacer;
}

internal class ReaperSlice: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.RprAny;
	public override uint[] ActionIDs => [RPR.InfernalSlice];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
		RPRGauge gauge = GetJobGauge<RPRGauge>();
		bool reaving = level >= RPR.Levels.SoulReaver && SelfHasEffect(RPR.Buffs.SoulReaver);
		bool enshrouded = level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0;
		bool executing = level >= RPR.Levels.Executions && SelfHasEffect(RPR.Buffs.Executioner);

		if (IsEnabled(CustomComboPreset.ReaperSliceWeaveAssist)) {
			if (level >= RPR.Levels.BloodStalk) {
				if (!enshrouded && (!reaving || IsEnabled(CustomComboPreset.ReaperSliceWeaveAssistDoubleReaving))) {
					if (gauge.Soul >= 50) {
						if (InCombat && CanWeave(actionID)) {

							if (IsEnabled(CustomComboPreset.ReaperBloodStalkGluttonyFeature)) {
								if (level >= RPR.Levels.Gluttony && CanUse(RPR.Gluttony))
									return OriginalHook(RPR.Gluttony); // TODO need a Sacrificium check specifically, somewhere
							}

							return OriginalHook(RPR.BloodStalk);
						}
					}
				}
			}
		}

		if (IsEnabled(CustomComboPreset.ReaperSliceSoulsowFeature)) {
			if (level >= RPR.Levels.Soulsow && !InCombat && !SelfHasEffect(RPR.Buffs.Soulsow))
				return RPR.Soulsow;
		}

		if (enshrouded) {

			if (IsEnabled(CustomComboPreset.ReaperSliceLemuresFeature)) {
				if (level >= RPR.Levels.EnhancedShroud && gauge.VoidShroud >= 2)
					return RPR.LemuresSlice;
			}

			if (IsEnabled(CustomComboPreset.ReaperSliceCommunioFeature)) {
				if (level >= RPR.Levels.Communio && gauge.LemureShroud == 1 && gauge.VoidShroud == 0)
					return RPR.Communio;
			}

		}

		if (reaving || enshrouded || executing) {

			if (IsEnabled(CustomComboPreset.ReaperSliceSmart)) {

				if (enshrouded && gauge.LemureShroud > 0) {
					// Gibbet -> Void Reaping, Gallows -> Cross Reaping

					if (SelfHasEffect(RPR.Buffs.EnhancedVoidReaping))
						return RPR.VoidReaping;

					if (SelfHasEffect(RPR.Buffs.EnhancedCrossReaping))
						return RPR.CrossReaping;
				}

				// Executioner's XXXXX use the same buffs
				if (SelfHasEffect(RPR.Buffs.EnhancedGibbet))
					return OriginalHook(RPR.Gibbet);

				if (SelfHasEffect(RPR.Buffs.EnhancedGallows))
					return OriginalHook(RPR.Gallows);

			}

			if (IsEnabled(CustomComboPreset.ReaperSliceGibbetFeature))
				// Void Reaping, Executioner's Gibbet
				return OriginalHook(RPR.Gibbet);

			if (IsEnabled(CustomComboPreset.ReaperSliceGallowsFeature))
				// Cross Reaping, Executioner's Gallows
				return OriginalHook(RPR.Gallows);
		}

		if (IsEnabled(CustomComboPreset.ReaperSliceShadowFeature)) {
			if (level >= RPR.Levels.ShadowOfDeath) {
				if (HasTarget && TargetOwnEffectDuration(RPR.Debuffs.DeathsDesign) < Service.Configuration.ReaperSliceDeathDebuffTime) {
					return RPR.ShadowOfDeath;
				}
			}
		}

		if (IsEnabled(CustomComboPreset.ReaperSoulOnSliceFeature)) {
			if (level >= RPR.Levels.SoulSlice) {
				if (gauge.Soul <= 50) {
					if (CanUse(RPR.SoulSlice))
						return RPR.SoulSlice;
				}
			}
		}

		if (IsEnabled(CustomComboPreset.ReaperSliceCombo)) {

			if (level >= RPR.Levels.InfernalSlice) {
				if (lastComboMove is RPR.WaxingSlice)
					return RPR.InfernalSlice;
			}

			if (level >= RPR.Levels.WaxingSlice) {
				if (lastComboMove is RPR.Slice)
					return RPR.WaxingSlice;
			}

			return RPR.Slice;
		}

		return actionID;
	}
}

internal class ReaperScythe: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.RprAny;
	public override uint[] ActionIDs => [RPR.NightmareScythe];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
		RPRGauge gauge = GetJobGauge<RPRGauge>();
		bool reaving = level >= RPR.Levels.SoulReaver && SelfHasEffect(RPR.Buffs.SoulReaver);
		bool enshrouded = level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0;
		bool executing = level >= RPR.Levels.Executions && SelfHasEffect(RPR.Buffs.Executioner);

		if (IsEnabled(CustomComboPreset.ReaperScytheWeaveAssist)) {
			if (level >= RPR.Levels.GrimSwathe) {
				if (!enshrouded && (!reaving || IsEnabled(CustomComboPreset.ReaperScytheWeaveAssistDoubleReaving))) {
					if (gauge.Soul >= 50) {
						if (InCombat && CanWeave(actionID)) {

							if (IsEnabled(CustomComboPreset.ReaperGrimSwatheGluttonyFeature)) {
								if (level >= RPR.Levels.Gluttony && IsOffCooldown(RPR.Gluttony))
									return RPR.Gluttony;
							}

							return OriginalHook(RPR.GrimSwathe);
						}
					}
				}
			}
		}

		if (enshrouded) {

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
			if (reaving || enshrouded || executing) {
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

		if (IsEnabled(CustomComboPreset.ReaperScytheWhorlFeature)) {
			if (level >= RPR.Levels.WhorlOfDeath) {
				if (HasTarget && TargetOwnEffectDuration(RPR.Debuffs.DeathsDesign) < Service.Configuration.ReaperScytheDeathDebuffTime) {
					return RPR.WhorlOfDeath;
				}
			}
		}

		if (IsEnabled(CustomComboPreset.ReaperSoulOnScytheFeature)) {
			if (level >= RPR.Levels.SoulScythe) {
				if (gauge.Soul <= 50) {
					if (CanUse(RPR.SoulScythe))
						return RPR.SoulScythe;
				}
			}
		}

		if (IsEnabled(CustomComboPreset.ReaperScytheCombo)) {

			if (level >= RPR.Levels.NightmareScythe) {
				if (lastComboMove is RPR.SpinningScythe)
					return RPR.NightmareScythe;
			}

			return RPR.SpinningScythe;
		}

		return actionID;
	}
}

internal class ReaperShadowOfDeath: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.RprAny;
	public override uint[] ActionIDs => [RPR.ShadowOfDeath];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (IsEnabled(CustomComboPreset.ReaperShadowSoulsowFeature)) {
			if (level >= RPR.Levels.Soulsow && !InCombat && !HasTarget && !SelfHasEffect(RPR.Buffs.Soulsow))
				return RPR.Soulsow;
		}

		RPRGauge gauge = GetJobGauge<RPRGauge>();
		bool reaving = level >= RPR.Levels.SoulReaver && SelfHasEffect(RPR.Buffs.SoulReaver);
		bool enshrouded = level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0;
		bool executing = level >= RPR.Levels.Executions && SelfHasEffect(RPR.Buffs.Executioner);

		if (enshrouded) {

			if (IsEnabled(CustomComboPreset.ReaperShadowLemuresFeature)) {
				if (level >= RPR.Levels.EnhancedShroud && gauge.VoidShroud >= 2)
					return RPR.LemuresSlice;
			}

			if (IsEnabled(CustomComboPreset.ReaperShadowCommunioFeature)) {
				if (level >= RPR.Levels.Communio && gauge.LemureShroud == 1 && gauge.VoidShroud == 0)
					return RPR.Communio;
			}

		}

		if (reaving || enshrouded || executing) {

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
	public override uint[] ActionIDs => [RPR.SoulSlice];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
		RPRGauge gauge = GetJobGauge<RPRGauge>();

		if (IsEnabled(CustomComboPreset.ReaperSoulSliceWeaveAssist)) {
			if (level >= RPR.Levels.BloodStalk) {
				if (gauge.Soul >= 50 && gauge.EnshroudedTimeRemaining == 0) {
					if (InCombat && CanWeave(actionID)) {

						if (IsEnabled(CustomComboPreset.ReaperBloodStalkGluttonyFeature)) {
							if (level >= RPR.Levels.Gluttony && IsOffCooldown(RPR.Gluttony))
								return RPR.Gluttony;
						}

						return OriginalHook(RPR.BloodStalk);
					}
				}
			}
		}

		bool reaving = level >= RPR.Levels.SoulReaver && SelfHasEffect(RPR.Buffs.SoulReaver);
		bool enshrouded = level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0;
		bool executing = level >= RPR.Levels.Executions && SelfHasEffect(RPR.Buffs.Executioner);

		if (enshrouded) {

			if (IsEnabled(CustomComboPreset.ReaperSoulLemuresFeature)) {
				if (level >= RPR.Levels.EnhancedShroud && gauge.VoidShroud >= 2)
					return RPR.LemuresSlice;
			}

			if (IsEnabled(CustomComboPreset.ReaperSoulCommunioFeature)) {
				if (level >= RPR.Levels.Communio && gauge.LemureShroud == 1 && gauge.VoidShroud == 0)
					return RPR.Communio;
			}

		}

		if (reaving || enshrouded || executing) {

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
	public override uint[] ActionIDs => [RPR.SoulScythe];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
		RPRGauge gauge = GetJobGauge<RPRGauge>();

		if (IsEnabled(CustomComboPreset.ReaperSoulScytheWeaveAssist)) {
			if (level >= RPR.Levels.GrimSwathe) {
				if (gauge.Soul >= 50) {
					if (InCombat && CanWeave(actionID)) {

						if (IsEnabled(CustomComboPreset.ReaperGrimSwatheGluttonyFeature)) {
							if (level >= RPR.Levels.Gluttony && IsOffCooldown(RPR.Gluttony))
								return RPR.Gluttony;
						}

						return OriginalHook(RPR.GrimSwathe);
					}
				}
			}
		}

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
	public override uint[] ActionIDs => [RPR.BloodStalk];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
		RPRGauge gauge = GetJobGauge<RPRGauge>();

		if (level >= RPR.Levels.Gluttony && gauge.Soul >= 50 && IsOffCooldown(RPR.Gluttony))
			return RPR.Gluttony;

		return actionID;
	}
}

internal class ReaperGrimSwathe: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperGrimSwatheGluttonyFeature;
	public override uint[] ActionIDs => [RPR.GrimSwathe];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
		RPRGauge gauge = GetJobGauge<RPRGauge>();

		if (level >= RPR.Levels.Gluttony && gauge.Soul >= 50 && IsOffCooldown(RPR.Gluttony))
			return RPR.Gluttony;

		return actionID;
	}
}

internal class ReaperGibbetGallows: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.RprAny;
	public override uint[] ActionIDs => [RPR.Gibbet, RPR.Gallows];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
		RPRGauge gauge = GetJobGauge<RPRGauge>();
		bool reaving = level >= RPR.Levels.SoulReaver && SelfHasEffect(RPR.Buffs.SoulReaver);
		bool enshrouded = level >= RPR.Levels.Enshroud && gauge.EnshroudedTimeRemaining > 0;
		bool executing = level >= RPR.Levels.Executions && SelfHasEffect(RPR.Buffs.Executioner);

		if (reaving || enshrouded || executing) {

			if (IsEnabled(CustomComboPreset.ReaperLemuresSoulReaverFeature)) {
				if (level >= RPR.Levels.EnhancedShroud && gauge.VoidShroud >= 2)
					return RPR.LemuresSlice;
			}

			if (IsEnabled(CustomComboPreset.ReaperCommunioSoulReaverFeature)) {
				if (level >= RPR.Levels.Communio && gauge.LemureShroud == 1 && gauge.VoidShroud == 0)
					return RPR.Communio;
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
	public override uint[] ActionIDs => [RPR.Guillotine];

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
	public override uint[] ActionIDs => [RPR.Enshroud];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
		RPRGauge gauge = GetJobGauge<RPRGauge>();

		if (level >= RPR.Levels.Communio && gauge.EnshroudedTimeRemaining > 0)
			return RPR.Communio;

		return actionID;
	}
}

internal class ReaperArcaneCircle: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperHarvestFeature;
	public override uint[] ActionIDs => [RPR.ArcaneCircle];

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= RPR.Levels.PlentifulHarvest && SelfEffectStacks(RPR.Buffs.ImmortalSacrifice) >= Service.Configuration.ReaperArcaneHarvestStackThreshold)
			return RPR.PlentifulHarvest;

		return actionID;
	}
}

internal class ReaperHellsIngressEgress: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.ReaperRegressFeature;
	public override uint[] ActionIDs => [RPR.HellsEgress, RPR.HellsIngress];

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
	public override uint[] ActionIDs => [RPR.Harpe];

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
