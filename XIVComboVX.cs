using System;
using System.Linq;

using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using Dalamud.Plugin;

using XIVComboVX.Combos;

namespace XIVComboVX {
	public sealed class XIVComboVX: IDalamudPlugin {
		public string Name => "XIV Combo Very Expanded Plugin";

		private const string command = "/pcombo";

		private readonly WindowSystem? windowSystem;
		private readonly ConfigWindow? configWindow;

		public XIVComboVX(DalamudPluginInterface pluginInterface) {
			FFXIVClientStructs.Resolver.Initialize();
			pluginInterface.Create<Service>();
			Service.Logger = new();

			Service.Configuration = pluginInterface.GetPluginConfig() as PluginConfiguration ?? new PluginConfiguration();

			Service.Address = new PluginAddressResolver();
			Service.Address.Setup();

#if DEBUG
			try {
				if (!pluginInterface.IsDebugging)
					Service.Commands.ProcessCommand("/xldev");
			}
			catch (Exception) { // this SEEMS to only happen occasionally and on initial load; reloading the plugin at the title screen works fine
				Service.Commands.ProcessCommand("/xldev");
			}
#endif

			if (!Service.Address.LoadSuccessful) {
				Service.Commands.ProcessCommand("/xllog");
			}
			else {
				Service.DataCache = new();
				Service.IconReplacer = new();

				this.configWindow = new();
				this.windowSystem = new("XIVComboVX");
				this.windowSystem.AddWindow(this.configWindow);

				Service.Interface.UiBuilder.OpenConfigUi += this.toggleConfigUi;
				Service.Interface.UiBuilder.Draw += this.windowSystem.Draw;
			}

			Service.Commands.AddHandler(command, new CommandInfo(this.onPluginCommand) {
				HelpMessage = Service.Address.LoadSuccessful ? "Open a window to edit custom combo settings." : "Do nothing, because the plugin failed to initialise.",
				ShowInHelp = true
			});
		}

		public void Dispose() {
			Service.Commands.RemoveHandler(command);

			Service.Interface.UiBuilder.OpenConfigUi -= this.toggleConfigUi;
			if (this.windowSystem is not null)
				Service.Interface.UiBuilder.Draw -= this.windowSystem.Draw;

			Service.IconReplacer.Dispose();
			Service.Logger.Dispose();
		}

		private void toggleConfigUi() {
			if (this.configWindow is not null) {
				this.configWindow.IsOpen = !this.configWindow.IsOpen;
			}
			else {
				PluginLog.Error("Cannot toggle configuration window, reference does not exist");
			}
		}

		private void onPluginCommand(string command, string arguments) {
			if (!Service.Address.LoadSuccessful) {
				Service.Chat.PrintError($"The plugin failed to initialise and cannot run:\n{Service.Address.LoadFailReason!.Message}");
				return;
			}

			string[] argumentsParts = arguments.Split();

			switch (argumentsParts[0]) {
				case "debug": {
						Service.Logger.EnableNextTick();
						Service.Chat.Print("Enabled debug message snapshot");
					}
					break;
				case "reset": {
						Service.Configuration.EnabledActions.Clear();
						Service.Configuration.DancerDanceCompatActionIDs = new[] {
						DNC.Cascade,
						DNC.Flourish,
						DNC.FanDance1,
						DNC.FanDance2,
					};
						Service.Configuration.Save();

						Service.Chat.Print("Reset configuration");
					}
					break;
				case "set": {
						string targetPreset = argumentsParts[1].ToLower();
						foreach (CustomComboPreset preset in Enum.GetValues(typeof(CustomComboPreset)).Cast<CustomComboPreset>()) {
							if (preset.ToString().ToLower() != targetPreset)
								continue;

							Service.Configuration.EnabledActions.Add(preset);
							Service.Chat.Print($"{preset} SET");
						}
					}
					break;
				case "toggle": {
						string targetPreset = argumentsParts[1].ToLower();
						foreach (CustomComboPreset preset in Enum.GetValues(typeof(CustomComboPreset)).Cast<CustomComboPreset>()) {
							if (preset.ToString().ToLower() != targetPreset)
								continue;

							if (Service.Configuration.EnabledActions.Contains(preset)) {
								Service.Configuration.EnabledActions.Remove(preset);
								Service.Chat.Print($"{preset} UNSET");
							}
							else {
								Service.Configuration.EnabledActions.Add(preset);
								Service.Chat.Print($"{preset} SET");
							}
						}
					}
					break;
				case "unset": {
						string targetPreset = argumentsParts[1].ToLower();
						foreach (CustomComboPreset preset in Enum.GetValues(typeof(CustomComboPreset)).Cast<CustomComboPreset>()) {
							if (preset.ToString().ToLower() != targetPreset)
								continue;

							Service.Configuration.EnabledActions.Remove(preset);
							Service.Chat.Print($"{preset} UNSET");
						}
					}
					break;
				case "list": {
						string filter = argumentsParts.Length == 1 ? "all" : argumentsParts[1].ToLower();

						foreach (CustomComboPreset preset in Enum.GetValues(typeof(CustomComboPreset)).Cast<CustomComboPreset>()) {
							if (filter == "set") {
								if (Service.Configuration.EnabledActions.Contains(preset))
									Service.Chat.Print(preset.ToString());
							}
							else if (filter == "unset") {
								if (!Service.Configuration.EnabledActions.Contains(preset))
									Service.Chat.Print(preset.ToString());
							}
							else if (filter == "all") {
								Service.Chat.Print(preset.ToString());
							}
						}
					}
					break;
				default:
					this.toggleConfigUi();
					break;
			}

			Service.Interface.SavePluginConfig(Service.Configuration);
		}
	}
}
