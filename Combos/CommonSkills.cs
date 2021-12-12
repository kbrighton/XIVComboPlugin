using System.Linq;

namespace XIVComboVX.Combos {
	internal class CommonSkills {
		public const uint
			Swiftcast = 7561;
	}
	internal class CommonBuffs {
		public const ushort
			Swiftcast1 = 167,
			Swiftcast2 = 1325,
			Swiftcast3 = 1987,
			LostChainspell = 2560;
	}
	internal class CommonUtil {
		internal static bool shouldSwiftcast
			=> CustomCombo.GetCooldown(CommonSkills.Swiftcast).CooldownRemaining == 0
				&& !CustomCombo.SelfHasEffect(CommonBuffs.LostChainspell)
				&& !CustomCombo.SelfHasEffect(RDM.Buffs.Dualcast);
		internal static bool isFastcasting
			=> CustomCombo.SelfHasEffect(CommonBuffs.Swiftcast1)
				|| CustomCombo.SelfHasEffect(CommonBuffs.Swiftcast2)
				|| CustomCombo.SelfHasEffect(CommonBuffs.Swiftcast3)
				|| CustomCombo.SelfHasEffect(RDM.Buffs.Dualcast)
				|| CustomCombo.SelfHasEffect(CommonBuffs.LostChainspell);
	}
}
