
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboVX.Combos {
	internal static class RDM {
		public const byte JobID = 35;

		public const uint
			Verraise = 7523,
			Jolt = 7503,
			Jolt2 = 7524,
			Verthunder = 7505,
			Verthunder2 = 16524,
			Verfire = 7510,
			Verflare = 7525,
			Veraero = 7507,
			Veraero2 = 16525,
			Verstone = 7511,
			Verholy = 7526,
			Impact = 16526,
			Scatter = 7509,
			Redoublement = 7516,
			EnchantedRedoublement = 7529,
			Zwerchhau = 7512,
			EnchantedZwerchhau = 7528,
			Riposte = 7504,
			EnchantedRiposte = 7527,
			Moulinet = 7513,
			EnchantedMoulinet = 7530,
			Fleche = 7517,
			ContreSixte = 7519,
			Scorch = 16530,
			Resolution = 25858;

		public static class Buffs {
			public const ushort
				VerfireReady = 1234,
				VerstoneReady = 1235,
				Acceleration = 1238,
				Dualcast = 1249,
				LostChainspell = 2560;
		}

		public static class Debuffs {
			// public const ushort placeholder = 0;
		}

		public static class Levels {
			public const byte
				Jolt = 2,
				Verthunder = 4,
				Veraero = 10,
				Scatter = 15,
				Verthunder2 = 18,
				Veraero2 = 22,
				Zwerchhau = 35,
				Fleche = 45,
				Redoublement = 50,
				Vercure = 54,
				ContreSixte = 56,
				Jolt2 = 62,
				Verraise = 64,
				Impact = 66,
				Verflare = 68,
				Verholy = 70,
				Scorch = 80,
				Resolution = 90;
		}
	}

	internal class RedMageSwiftcastRaiserFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.RedMageSwiftcastRaiserFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { RDM.Verraise };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is RDM.Verraise && CommonUtil.shouldSwiftcast)
				return CommonSkills.Swiftcast;

			return actionID;
		}
	}

	internal class RedMageAoECombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.RedMageAoECombo;
		protected internal override uint[] ActionIDs { get; } = new[] { RDM.Veraero2, RDM.Verthunder2 };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is RDM.Veraero2 or RDM.Verthunder2) {

				if (lastComboMove is RDM.Scorch && level >= RDM.Levels.Resolution)
					return RDM.Resolution;

				if (lastComboMove is RDM.Verflare or RDM.Verholy && level >= RDM.Levels.Scorch)
					return RDM.Scorch;

				if (level >= RDM.Levels.Scatter && CommonUtil.isFastcasting)
					return OriginalHook(RDM.Scatter);

			}

			return actionID;
		}
	}

	internal class RedMageMeleeCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.RedMageMeleeCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { RDM.Redoublement, RDM.Moulinet };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			const int
				FINISHER_DELTA = 11,
				IMBALANCE_DIFF_MAX = 30;

			if (actionID is RDM.Redoublement or RDM.Moulinet) {
				RDMGauge gauge = GetJobGauge<RDMGauge>();
				int black = gauge.BlackMana;
				int white = gauge.WhiteMana;
				bool isFinishing1 = gauge.ManaStacks == 3;
				bool isFinishing2 = comboTime > 0 && lastComboMove is RDM.Verholy or RDM.Verflare;
				bool isFinishing3 = comboTime > 0 && lastComboMove is RDM.Scorch;
				bool canFinishWhite = level >= RDM.Levels.Verholy;
				bool canFinishBlack = level >= RDM.Levels.Verflare;
				int blackThreshold = white + IMBALANCE_DIFF_MAX;
				int whiteThreshold = black + IMBALANCE_DIFF_MAX;
				bool verfireUp = SelfHasEffect(RDM.Buffs.VerfireReady);
				bool verstoneUp = SelfHasEffect(RDM.Buffs.VerstoneReady);

				if (IsEnabled(CustomComboPreset.RedMageMeleeComboPlus)) {

					if (isFinishing3 && level >= RDM.Levels.Resolution)
						return RDM.Resolution;
					if (isFinishing2 && level >= RDM.Levels.Scorch)
						return RDM.Scorch;

					if (isFinishing1 && canFinishBlack) {

						if (black >= white && canFinishWhite) {

							if (verstoneUp && !verfireUp && (black + FINISHER_DELTA <= blackThreshold))
								return RDM.Verflare;

							return RDM.Verholy;
						}

						if (verfireUp && !verstoneUp && canFinishWhite && (white + FINISHER_DELTA <= whiteThreshold))
							return RDM.Verholy;

						return RDM.Verflare;
					}
				}

				if (actionID is RDM.Redoublement) {
					if (lastComboMove is RDM.Zwerchhau or RDM.EnchantedZwerchhau && level >= RDM.Levels.Redoublement)
						return OriginalHook(RDM.Redoublement);

					if (lastComboMove is RDM.Riposte or RDM.EnchantedRiposte && level >= RDM.Levels.Zwerchhau)
						return OriginalHook(RDM.Zwerchhau);

					return OriginalHook(RDM.Riposte);
				}

			}

			return actionID;
		}
	}

	internal class RedMageVerprocCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.RedMageVerprocCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { RDM.Verstone, RDM.Verfire };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is RDM.Verstone or RDM.Verfire) {

				if (lastComboMove == RDM.Scorch && level >= RDM.Levels.Resolution)
					return RDM.Resolution;
				if (lastComboMove is RDM.Verflare or RDM.Verholy && level >= RDM.Levels.Scorch)
					return RDM.Scorch;

				RDMGauge gauge = GetJobGauge<RDMGauge>();

				if (actionID is RDM.Verstone) {

					if (gauge.ManaStacks == 3 && level >= RDM.Levels.Verholy)
						return RDM.Verholy;

					if (IsEnabled(CustomComboPreset.RedMageVerprocComboPlus)
						&& level >= RDM.Levels.Veraero
						&& (CommonUtil.isFastcasting || SelfHasEffect(RDM.Buffs.Acceleration))
					)
						return RDM.Veraero;

					if (IsEnabled(CustomComboPreset.RedMageVeraeroOpenerFeature)
						&& level >= RDM.Levels.Veraero
						&& !HasCondition(ConditionFlag.InCombat)
						&& !SelfHasEffect(RDM.Buffs.VerstoneReady)
					)
						return RDM.Veraero;

					if (SelfHasEffect(RDM.Buffs.VerstoneReady))
						return RDM.Verstone;

				}
				else if (actionID is RDM.Verfire) {

					if (gauge.ManaStacks == 3 && level >= RDM.Levels.Verflare)
						return RDM.Verflare;

					if (IsEnabled(CustomComboPreset.RedMageVerprocComboPlus)
						&& level >= RDM.Levels.Verthunder
						&& (CommonUtil.isFastcasting || SelfHasEffect(RDM.Buffs.Acceleration))
					)
						return RDM.Verthunder;

					if (IsEnabled(CustomComboPreset.RedMageVerthunderOpenerFeature)
						&& level >= RDM.Levels.Verthunder
						&& !HasCondition(ConditionFlag.InCombat)
						&& !SelfHasEffect(RDM.Buffs.VerfireReady)
					)
						return RDM.Verthunder;

					if (SelfHasEffect(RDM.Buffs.VerfireReady))
						return RDM.Verfire;

				}

				return OriginalHook(RDM.Jolt2);
			}

			return actionID;
		}
	}

	internal class RedMageContreFlecheFeature: CustomCombo {
		protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.RedMageContreFlecheFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { RDM.Fleche, RDM.ContreSixte };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is RDM.Fleche or RDM.ContreSixte) {

				if (level >= RDM.Levels.ContreSixte)
					return PickByCooldown(actionID, RDM.Fleche, RDM.ContreSixte);

				if (level >= RDM.Levels.Fleche)
					return RDM.Fleche;

			}

			return actionID;
		}
	}

	internal class RedMageSmartcastAoECombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.RedMageSmartcastAoEFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { RDM.Veraero2, RDM.Verthunder2 };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is RDM.Veraero2 or RDM.Verthunder2) {

				if (CommonUtil.isFastcasting || SelfHasEffect(RDM.Buffs.Acceleration) || level < RDM.Levels.Verthunder2)
					return RDM.Scatter;

				if (level < RDM.Levels.Veraero2)
					return RDM.Verthunder2;

				RDMGauge gauge = GetJobGauge<RDMGauge>();

				if (gauge.BlackMana > gauge.WhiteMana)
					return RDM.Veraero2;

				if (gauge.WhiteMana > gauge.BlackMana)
					return RDM.Verthunder2;

			}

			return actionID;
		}
	}

	internal class RedmageSmartcastSingleCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.RedMageSmartcastSingleFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { RDM.Veraero, RDM.Verthunder, RDM.Verstone, RDM.Verfire };

		protected override uint Invoke(uint actionID, uint lastComboActionId, float comboTime, byte level) {
			const int
				LONG_DELTA = 6,
				PROC_DELTA = 5,
				FINISHER_DELTA = 11,
				IMBALANCE_DIFF_MAX = 30;

			if (actionID is RDM.Veraero or RDM.Verthunder or RDM.Verstone or RDM.Verfire) {

				bool verfireUp = SelfHasEffect(RDM.Buffs.VerfireReady);
				bool verstoneUp = SelfHasEffect(RDM.Buffs.VerstoneReady);
				RDMGauge gauge = GetJobGauge<RDMGauge>();
				int black = gauge.BlackMana;
				int white = gauge.WhiteMana;

				if (actionID is RDM.Veraero or RDM.Verthunder) {

					if (level < RDM.Levels.Verthunder)
						return RDM.Jolt;

					if (level is < RDM.Levels.Veraero and >= RDM.Levels.Verthunder)
						return OriginalHook(RDM.Verthunder);

					// This is for the long opener only, so we're not bothered about fast casting or finishers or anything like that
					if (black < white)
						return OriginalHook(RDM.Verthunder);

					if (white < black)
						return OriginalHook(RDM.Veraero);

					return actionID;
				}

				if (actionID is RDM.Verstone or RDM.Verfire) {

					bool fastCasting = CommonUtil.isFastcasting;
					bool accelerated = SelfHasEffect(RDM.Buffs.Acceleration);
					bool isFinishing1 = gauge.ManaStacks == 3;
					bool isFinishing2 = comboTime > 0 && lastComboActionId is RDM.Verholy or RDM.Verflare;
					bool isFinishing3 = comboTime > 0 && lastComboActionId is RDM.Scorch;
					bool canFinishWhite = level >= RDM.Levels.Verholy;
					bool canFinishBlack = level >= RDM.Levels.Verflare;
					int blackThreshold = white + IMBALANCE_DIFF_MAX;
					int whiteThreshold = black + IMBALANCE_DIFF_MAX;

					// If we're ready to Scorch or Resolution, just do that. Nice and simple. Sadly, that's where the simple ends.
					if (isFinishing3 && level >= RDM.Levels.Resolution)
						return RDM.Resolution;
					if (isFinishing2 && level >= RDM.Levels.Scorch)
						return RDM.Scorch;

					if (isFinishing1 && canFinishBlack) {

						if (black >= white && canFinishWhite) {

							// If we can already Verstone, but we can't Verfire, and Verflare WON'T imbalance us, use Verflare
							if (verstoneUp && !verfireUp && (black + FINISHER_DELTA <= blackThreshold))
								return RDM.Verflare;

							return RDM.Verholy;
						}

						// If we can already Verfire, but we can't Verstone, and we can use Verholy, and it WON'T imbalance us, use Verholy
						if (verfireUp && !verstoneUp && canFinishWhite && (white + FINISHER_DELTA <= whiteThreshold))
							return RDM.Verholy;

						return RDM.Verflare;
					}

					if (fastCasting || accelerated) {

						if (level is < RDM.Levels.Veraero and >= RDM.Levels.Verthunder)
							return RDM.Verthunder;

						if (verfireUp == verstoneUp) {

							// Either both procs are already up or neither is - use whatever gives us the mana we need
							if (black < white)
								return OriginalHook(RDM.Verthunder);

							if (white < black)
								return OriginalHook(RDM.Veraero);

							// If mana levels are equal, prioritise the colour that the original button was
							return actionID is RDM.Verstone
								? OriginalHook(RDM.Veraero)
								: OriginalHook(RDM.Verthunder);
						}

						if (verfireUp) {

							// If Veraero is feasible, use it
							if (white + LONG_DELTA <= whiteThreshold)
								return OriginalHook(RDM.Veraero);

							return OriginalHook(RDM.Verthunder);
						}

						if (verstoneUp) {

							// If Verthunder is feasible, use it
							if (black + LONG_DELTA <= blackThreshold)
								return OriginalHook(RDM.Verthunder);

							return OriginalHook(RDM.Veraero);
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

					// Only use Verfire if it won't imbalance us
					if (verfireUp && black + PROC_DELTA <= blackThreshold)
						return RDM.Verfire;

					// Only use Verstone if it won't imbalance us
					if (verstoneUp && white + PROC_DELTA <= whiteThreshold)
						return RDM.Verstone;

					// If neither's up or the one that is would imbalance us, just use Jolt
					return OriginalHook(RDM.Jolt2);
				}
			}
			return actionID;
		}
	}
}
