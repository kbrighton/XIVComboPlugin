using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboVX.Combos {
	internal static class RDM {
		public const byte JobID = 35;

		public const uint
			Verraise = 7523,
			Verthunder = 7505,
			Veraero = 7507,
			Veraero2 = 16525,
			Verthunder2 = 16524,
			Impact = 16526,
			Redoublement = 7516,
			EnchantedRedoublement = 7529,
			Zwerchhau = 7512,
			EnchantedZwerchhau = 7528,
			Riposte = 7504,
			EnchantedRiposte = 7527,
			Scatter = 7509,
			Verstone = 7511,
			Verfire = 7510,
			Jolt = 7503,
			Jolt2 = 7524,
			Verholy = 7526,
			Verflare = 7525,
			Scorch = 16530;

		public static class Buffs {
			public const short
				Swiftcast = 167,
				VerfireReady = 1234,
				VerstoneReady = 1235,
				Dualcast = 1249,
				LostChainspell = 2560;
		}

		public static class Debuffs {
			// public const short placeholder = 0;
		}

		public static class Levels {
			public const byte
				Jolt = 2,
				Verthunder = 4,
				Veraero = 10,
				Verraise = 64,
				Zwerchhau = 35,
				Redoublement = 50,
				Vercure = 54,
				Jolt2 = 62,
				Impact = 66,
				Verflare = 68,
				Verholy = 70,
				Scorch = 80;
		}
	}

	internal class RedMageSwiftcastRaiserFeature: CustomCombo {
		protected override CustomComboPreset Preset => CustomComboPreset.RedMageSwiftcastRaiserFeature;

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID == RDM.Verraise
				&& GetCooldown(CommonSkills.Swiftcast).CooldownRemaining == 0
				&& !SelfHasEffect(RDM.Buffs.Dualcast)
				&& !SelfHasEffect(RDM.Buffs.LostChainspell)
			)
				return CommonSkills.Swiftcast;

			return actionID;
		}
	}

	internal class RedMageAoECombo: CustomCombo {
		protected override CustomComboPreset Preset => CustomComboPreset.RedMageAoECombo;

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (
				(actionID is RDM.Veraero2 or RDM.Verthunder2)
				&& (SelfHasEffect(RDM.Buffs.Swiftcast) || SelfHasEffect(RDM.Buffs.Dualcast) || SelfHasEffect(RDM.Buffs.LostChainspell))
			)
					return OriginalHook(RDM.Impact);

			return actionID;
		}
	}

	internal class RedMageMeleeCombo: CustomCombo {
		protected override CustomComboPreset Preset => CustomComboPreset.RedMageMeleeCombo;

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID == RDM.Redoublement) {

				if (IsEnabled(CustomComboPreset.RedMageMeleeComboPlus) && lastComboMove == RDM.EnchantedRedoublement) {
					RDMGauge gauge = GetJobGauge<RDMGauge>();
					if (gauge.BlackMana >= gauge.WhiteMana && level >= RDM.Levels.Verholy) {
						if (SelfHasEffect(RDM.Buffs.VerstoneReady) && !SelfHasEffect(RDM.Buffs.VerfireReady) && (gauge.BlackMana - gauge.WhiteMana <= 9))
							return RDM.Verflare;

						return RDM.Verholy;
					}
					else if (level >= RDM.Levels.Verflare) {
						if (!SelfHasEffect(RDM.Buffs.VerstoneReady) && SelfHasEffect(RDM.Buffs.VerfireReady) && level >= RDM.Levels.Verholy && (gauge.WhiteMana - gauge.BlackMana <= 9))
							return RDM.Verholy;

						return RDM.Verflare;
					}
				}

				if ((lastComboMove == RDM.Riposte || lastComboMove == RDM.EnchantedRiposte) && level >= RDM.Levels.Zwerchhau)
					return OriginalHook(RDM.Zwerchhau);

				if ((lastComboMove == RDM.Zwerchhau || lastComboMove == RDM.EnchantedZwerchhau) && level >= RDM.Levels.Redoublement)
					return OriginalHook(RDM.Redoublement);

				if (IsEnabled(CustomComboPreset.RedMageMeleeComboPlus)) {
					if ((lastComboMove == RDM.Verflare || lastComboMove == RDM.Verholy) && level >= RDM.Levels.Scorch)
						return RDM.Scorch;
				}

				return OriginalHook(RDM.Riposte);
			}

			return actionID;
		}
	}

	internal class RedMageVerprocCombo: CustomCombo {
		protected override CustomComboPreset Preset => CustomComboPreset.RedMageVerprocCombo;

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID == RDM.Verstone) {
				if (level >= RDM.Levels.Scorch && (lastComboMove == RDM.Verflare || lastComboMove == RDM.Verholy))
					return RDM.Scorch;

				if (lastComboMove == RDM.EnchantedRedoublement && level >= RDM.Levels.Verholy)
					return RDM.Verholy;

				if (IsEnabled(CustomComboPreset.RedMageVerprocComboPlus)) {
					if (
						(SelfHasEffect(RDM.Buffs.Dualcast) || SelfHasEffect(RDM.Buffs.Swiftcast) || SelfHasEffect(RDM.Buffs.LostChainspell))
						&& level >= RDM.Levels.Veraero
					)
						return RDM.Veraero;
				}

				if (IsEnabled(CustomComboPreset.RedMageVeraeroOpenerFeature)) {
					if (!SelfHasEffect(RDM.Buffs.VerstoneReady) && !HasCondition(ConditionFlag.InCombat) && level >= RDM.Levels.Veraero)
						return RDM.Veraero;
				}

				if (SelfHasEffect(RDM.Buffs.VerstoneReady))
					return RDM.Verstone;

				return OriginalHook(RDM.Jolt2);
			}

			if (actionID == RDM.Verfire) {
				if (level >= RDM.Levels.Scorch && (lastComboMove == RDM.Verflare || lastComboMove == RDM.Verholy))
					return RDM.Scorch;

				if (lastComboMove == RDM.EnchantedRedoublement && level >= RDM.Levels.Verflare)
					return RDM.Verflare;

				if (IsEnabled(CustomComboPreset.RedMageVerprocComboPlus)) {
					if (
						(SelfHasEffect(RDM.Buffs.Dualcast) || SelfHasEffect(RDM.Buffs.Swiftcast) || SelfHasEffect(RDM.Buffs.LostChainspell))
						&& level >= RDM.Levels.Verthunder
					)
						return RDM.Verthunder;
				}

				if (IsEnabled(CustomComboPreset.RedMageVerthunderOpenerFeature)) {
					if (!SelfHasEffect(RDM.Buffs.VerfireReady) && !HasCondition(ConditionFlag.InCombat) && level >= RDM.Levels.Verthunder)
						return RDM.Verthunder;
				}

				if (SelfHasEffect(RDM.Buffs.VerfireReady))
					return RDM.Verfire;

				return OriginalHook(RDM.Jolt2);
			}

			return actionID;
		}
	}

	internal class RedmageSmartcastSingleCombo: CustomCombo {
		protected override CustomComboPreset Preset => CustomComboPreset.RedMageSmartcastSingleFeature;

		protected override uint Invoke(uint actionID, uint lastComboActionId, float comboTime, byte level) {
			const int
				LONG_DELTA = 11,
				PROC_DELTA = 9,
				FINISHER_DELTA = 21,
				IMBALANCE_DIFF_MAX = 30;
			if (actionID is RDM.Veraero or RDM.Verthunder or RDM.Verstone or RDM.Verfire) {
				bool verfireUp = SelfHasEffect(RDM.Buffs.VerfireReady);
				bool verstoneUp = SelfHasEffect(RDM.Buffs.VerstoneReady);
				RDMGauge gauge = GetJobGauge<RDMGauge>();
				int black = gauge.BlackMana;
				int white = gauge.WhiteMana;
				// TODO: need to handle levels being too low for certain actions
				if (actionID is RDM.Veraero or RDM.Verthunder) {
					// This is for the long opener only, so we're not bothered about fast casting or finishers or anything like that
					if (black < white)
						return RDM.Verthunder;
					if (white < black)
						return RDM.Veraero;
					return actionID;
				}
				if (actionID is RDM.Verstone or RDM.Verfire) {
					bool fastCasting = SelfHasEffect(RDM.Buffs.Dualcast) || SelfHasEffect(RDM.Buffs.Swiftcast) || SelfHasEffect(RDM.Buffs.LostChainspell);
					bool isFinishing1 = comboTime > 0 && lastComboActionId is RDM.EnchantedRedoublement;
					bool isFinishing2 = comboTime > 0 && lastComboActionId is RDM.Verholy or RDM.Verflare;
					bool canFinishWhite = level >= RDM.Levels.Verholy;
					bool canFinishBlack = level >= RDM.Levels.Verflare;
					int blackThreshold = white + IMBALANCE_DIFF_MAX;
					int whiteThreshold = black + IMBALANCE_DIFF_MAX;
					// If we're ready to Scorch, just do that. Nice and simple. Sadly, that's where the simple ends.
					if (isFinishing2 && level >= RDM.Levels.Scorch)
						return RDM.Scorch;
					if (isFinishing1 && canFinishBlack) {
						if (black >= white && canFinishWhite) {
							// If we can already Verstone, but we can't Verfire, and Verflare WON'T imbalance us, use Verflare
							if (SelfHasEffect(RDM.Buffs.VerstoneReady) && !SelfHasEffect(RDM.Buffs.VerfireReady) && (black + FINISHER_DELTA <= blackThreshold))
								return RDM.Verflare;
							return RDM.Verholy;
						}
						// If we can already Verfire, but we can't Verstone, and we can use Verholy, and it WON'T imbalance us, use Verholy
						if (SelfHasEffect(RDM.Buffs.VerfireReady) && !SelfHasEffect(RDM.Buffs.VerstoneReady) && canFinishWhite && (white + FINISHER_DELTA <= whiteThreshold))
							return RDM.Verholy;
						return RDM.Verflare;
					}
					if (fastCasting) {
						if (verfireUp == verstoneUp) {
							// Either both procs are already up or neither is - use whatever gives us the mana we need
							if (black < white)
								return RDM.Verthunder;
							if (white < black)
								return RDM.Veraero;
							// If mana levels are equal, prioritise the colour that the original button was
							return actionID is RDM.Verstone
								? RDM.Veraero
								: RDM.Verthunder;
						}
						if (verfireUp) {
							// If Veraero is feasible, use it
							if (white + LONG_DELTA <= whiteThreshold)
								return RDM.Veraero;
							return RDM.Verthunder;
						}
						if (verstoneUp) {
							// If Verthunder is feasible, use it
							if (black + LONG_DELTA <= blackThreshold)
								return RDM.Verthunder;
							return RDM.Veraero;
						}
					}
					if (verfireUp && verstoneUp) {
						// Decide by mana levels
						if (black < white)
							return RDM.Verfire;
						if (white < black)
							return RDM.Verstone;
						// If mana levels are equal, prioritise the original button
						return actionID;
					}
					if (verfireUp) {
						// Only use Verfire if it won't imbalance us
						if (black + PROC_DELTA <= blackThreshold)
							return RDM.Verfire;
						return OriginalHook(RDM.Jolt2);
					}
					if (verstoneUp) {
						// Only use Verstone if it won't imbalance us
						if (white + PROC_DELTA <= whiteThreshold)
							return RDM.Verstone;
						return OriginalHook(RDM.Jolt2);
					}
					// If neither's up, just use Jolt
					return OriginalHook(RDM.Jolt2);
				}
			}
			return actionID;
		}
	}

	internal class RedMageSmartcastAoECombo: CustomCombo {
		protected override CustomComboPreset Preset => CustomComboPreset.RedMageSmartcastAoEFeature;

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is RDM.Veraero2 or RDM.Verthunder2) {
				if (SelfHasEffect(RDM.Buffs.Swiftcast) || SelfHasEffect(RDM.Buffs.Dualcast) || SelfHasEffect(RDM.Buffs.LostChainspell))
					return OriginalHook(RDM.Impact);
				RDMGauge gauge = GetJobGauge<RDMGauge>();
				if (gauge.BlackMana > gauge.WhiteMana)
					return RDM.Veraero2;
				if (gauge.WhiteMana > gauge.BlackMana)
					return RDM.Verthunder2;
			}

			return actionID;
		}
	}
}
