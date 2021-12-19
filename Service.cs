
using Dalamud.Data;
using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Buddy;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.JobGauge;
using Dalamud.Game.ClientState.Objects;
using Dalamud.Game.Command;
using Dalamud.Game.Gui;
using Dalamud.IoC;
using Dalamud.Plugin;

namespace XIVComboVX {
	internal class Service {
		public static PluginConfiguration Configuration { get; set; } = null!;

		public static IconReplacer IconReplacer { get; set; } = null!;

		public static PluginAddressResolver Address { get; set; } = null!;

		[PluginService]
		public static DalamudPluginInterface Interface { get; private set; } = null!;

		[PluginService]
		public static BuddyList BuddyList { get; private set; } = null!;

		[PluginService]
		public static ChatGui Chat { get; private set; } = null!;

		[PluginService]
		public static ClientState Client { get; private set; } = null!;

		[PluginService]
		public static CommandManager Commands { get; private set; } = null!;

		[PluginService]
		public static Condition Conditions { get; private set; } = null!;

		[PluginService]
		public static DataManager Data { get; private set; } = null!;

		[PluginService]
		public static JobGauges JobGauge { get; private set; } = null!;

		[PluginService]
		public static TargetManager Targets { get; private set; } = null!;
	}
}
