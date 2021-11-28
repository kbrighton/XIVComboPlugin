
using Dalamud.Data;
using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.JobGauge;
using Dalamud.Game.ClientState.Objects;
using Dalamud.Game.Command;
using Dalamud.Game.Gui;
using Dalamud.IoC;
using Dalamud.Plugin;

namespace XIVComboVX {
	internal class Service {
		internal static PluginConfiguration configuration { get; set; } = null!;

		internal static IconReplacer iconReplacer { get; set; } = null!;

		internal static PluginAddressResolver address { get; set; } = null!;

		[PluginService]
		internal static DalamudPluginInterface pluginInterface { get; private set; } = null!;

		[PluginService]
		internal static ChatGui chatGui { get; private set; } = null!;

		[PluginService]
		internal static ClientState client { get; private set; } = null!;

		[PluginService]
		internal static CommandManager commandManager { get; private set; } = null!;

		[PluginService]
		internal static Condition conditions { get; private set; } = null!;

		[PluginService]
		internal static DataManager data { get; private set; } = null!;

		[PluginService]
		internal static JobGauges jobGauges { get; private set; } = null!;

		[PluginService]
		internal static TargetManager targets { get; private set; } = null!;
	}
}
