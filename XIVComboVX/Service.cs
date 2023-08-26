namespace PrincessRTFM.XIVComboVX;

using Dalamud.Game;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects;
using Dalamud.Game.Gui;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

using XIVComboVX.Config;
using XIVComboVX.GameData;

internal class Service {
	public static Plugin Plugin { get; set; } = null!;

	public static PluginConfiguration Configuration { get; set; } = null!;

	public static IconReplacer IconReplacer { get; set; } = null!;

	public static PluginAddressResolver Address { get; set; } = null!;

	public static ComboDataCache DataCache { get; set; } = null!;

	public static LogUtil Logger { get; set; } = null!;

	public static GameState GameState { get; set; } = null!;

	public static UpdateAlerter? UpdateAlert { get; set; } = null;

	public static ChatUtil ChatUtils { get; set; } = null!;

	public static Ipc Ipc { get; set; } = null!;

	[PluginService]
	public static DalamudPluginInterface Interface { get; private set; } = null!;

	[PluginService]
	public static IBuddyList BuddyList { get; private set; } = null!;

	[PluginService]
	public static ChatGui ChatGui { get; private set; } = null!;

	[PluginService]
	public static IClientState Client { get; private set; } = null!;

	[PluginService]
	public static ICommandManager Commands { get; private set; } = null!;

	[PluginService]
	public static Condition Conditions { get; private set; } = null!;

	[PluginService]
	public static IDataManager GameData { get; private set; } = null!;

	[PluginService]
	public static Framework Framework { get; private set; } = null!;

	[PluginService]
	public static IJobGauges JobGauge { get; private set; } = null!;

	[PluginService]
	public static ITargetManager Targets { get; private set; } = null!;

	[PluginService]
	public static IGameGui GameGui { get; private set; } = null!;

}
