using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboVX.Combos {
	internal static class GNB {
		public const byte JobID = 37;

		public const uint
			KeenEdge = 16137,
			NoMercy = 16138,
			BrutalShell = 16139,
			DemonSlice = 16141,
			SolidBarrel = 16145,
			GnashingFang = 16146,
			DemonSlaughter = 16149,
			SonicBreak = 16153,
			Continuation = 16155,
			JugularRip = 16156,
			AbdomenTear = 16157,
			EyeGouge = 16158,
			BowShock = 16159,
			BurstStrike = 16162,
			FatedCircle = 16163,
			Bloodfest = 16164,
			DoubleDown = 25760,
			Hypervelocity = 25759;

		public static class Buffs {
			public const ushort
				NoMercy = 1831,
				ReadyToRip = 1842,
				ReadyToTear = 1843,
				ReadyToGouge = 1844,
				ReadyToBlast = 2686;
		}

		public static class Debuffs {
			public const ushort
				BowShock = 1838;
		}

		public static class Levels {
			public const byte
				NoMercy = 2,
				BrutalShell = 4,
				SolidBarrel = 26,
				DemonSlaughter = 40,
				SonicBreak = 54,
				BowShock = 62,
				Continuation = 70,
				FatedCircle = 72,
				Bloodfest = 76,
				EnhancedContinuation = 86,
				CartridgeCharge2 = 88,
				DoubleDown = 90;
		}
	}

	internal class GunbreakerSolidBarrelCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.GunbreakerSolidBarrelCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { GNB.SolidBarrel };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is GNB.SolidBarrel) {

				return SimpleChainCombo(level, lastComboMove, comboTime, (1, GNB.KeenEdge),
					(GNB.Levels.BrutalShell, GNB.BrutalShell),
					(GNB.Levels.SolidBarrel, GNB.SolidBarrel)
				);

			}

			return actionID;
		}
	}

	internal class GunbreakerGnashingFangContinuation: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.GunbreakerGnashingFangCont;
		protected internal override uint[] ActionIDs { get; } = new[] { GNB.GnashingFang };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is GNB.GnashingFang) {

				if (level >= GNB.Levels.Continuation) {
					if (SelfHasEffect(GNB.Buffs.ReadyToGouge))
						return GNB.EyeGouge;

					if (SelfHasEffect(GNB.Buffs.ReadyToTear))
						return GNB.AbdomenTear;

					if (SelfHasEffect(GNB.Buffs.ReadyToRip))
						return GNB.JugularRip;
				}

				return OriginalHook(GNB.GnashingFang);
			}

			return actionID;
		}
	}

	internal class GunbreakerBowShockSonicBreakFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.GunbreakerBowShockSonicBreakFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { GNB.BowShock, GNB.SonicBreak };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is GNB.BowShock or GNB.SonicBreak) {

				if (level >= GNB.Levels.BowShock)
					return PickByCooldown(actionID, GNB.SonicBreak, GNB.BowShock);

			}
			return actionID;
		}
	}

	internal class GunbreakerDemonSlaughterCombo: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.GunbreakerDemonSlaughterCombo;
		protected internal override uint[] ActionIDs { get; } = new[] { GNB.DemonSlaughter };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is GNB.DemonSlaughter) {

				if (level >= GNB.Levels.DemonSlaughter && comboTime > 0 && lastComboMove == GNB.DemonSlice) {

					if (level >= GNB.Levels.FatedCircle && IsEnabled(CustomComboPreset.GunbreakerFatedCircleFeature)) {
						GNBGauge gauge = GetJobGauge<GNBGauge>();
						int maxAmmo = level >= GNB.Levels.CartridgeCharge2 ? 3 : 2;

						if (gauge.Ammo == maxAmmo) {
							return GNB.FatedCircle;
						}
					}

					return GNB.DemonSlaughter;
				}

				return GNB.DemonSlice;
			}

			return actionID;
		}
	}

	internal class GunbreakerEmptyBloodfestFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.GunbreakerEmptyBloodfestFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { GNB.BurstStrike, GNB.FatedCircle };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is GNB.BurstStrike or GNB.FatedCircle) {

				if (level >= GNB.Levels.EnhancedContinuation && IsEnabled(CustomComboPreset.GunbreakerBurstStrikeCont) && SelfHasEffect(GNB.Buffs.ReadyToBlast))
					return GNB.Hypervelocity;

				GNBGauge gauge = GetJobGauge<GNBGauge>();

				if (level >= GNB.Levels.Bloodfest && gauge.Ammo == 0)
					return GNB.Bloodfest;

			}

			return actionID;
		}
	}

	internal class GunbreakerNoMercyFeature: CustomCombo {
		protected internal override CustomComboPreset Preset => CustomComboPreset.GunbreakerNoMercyFeature;
		protected internal override uint[] ActionIDs { get; } = new[] { GNB.NoMercy };

		protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
			if (actionID is GNB.NoMercy) {

				if (level >= GNB.Levels.NoMercy && SelfHasEffect(GNB.Buffs.NoMercy)) {

					if (level >= GNB.Levels.BowShock)
						return PickByCooldown(GNB.SonicBreak, GNB.SonicBreak, GNB.BowShock);

					if (level >= GNB.Levels.SonicBreak)
						return GNB.SonicBreak;
				}
			}

			return actionID;
		}
	}
}
