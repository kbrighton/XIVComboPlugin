using System;
using System.Linq;

using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;

namespace XIVComboVX {
	public sealed class XIVComboVeryExpandedPlugin: IDalamudPlugin {
		public string Name => "XIV Combo Very Expanded Plugin";

		private const string command = "/pcombo";

		private readonly WindowSystem windowSystem;
		private readonly ConfigWindow configWindow;

		public XIVComboVeryExpandedPlugin(DalamudPluginInterface pluginInterface) {
			pluginInterface.Create<Service>();

			Service.configuration = pluginInterface.GetPluginConfig() as PluginConfiguration ?? new PluginConfiguration();
			Service.configuration.CheckVersion();

			Service.address = new PluginAddressResolver();
			Service.address.Setup();

			Service.iconReplacer = new IconReplacer();

			this.configWindow = new();
			this.windowSystem = new("XIVComboVX");
			this.windowSystem.AddWindow(this.configWindow);

			Service.pluginInterface.UiBuilder.OpenConfigUi += this.toggleConfigUi;
			Service.pluginInterface.UiBuilder.Draw += this.windowSystem.Draw;

			Service.commandManager.AddHandler(command, new CommandInfo(this.onPluginCommand) {
				HelpMessage = "Open a window to edit custom combo settings.",
				ShowInHelp = true
			});
		}

		public void Dispose() {
			Service.commandManager.RemoveHandler(command);

			Service.pluginInterface.UiBuilder.OpenConfigUi -= this.toggleConfigUi;
			Service.pluginInterface.UiBuilder.Draw -= this.windowSystem.Draw;

			Service.iconReplacer.Dispose();
		}

		private void toggleConfigUi() => this.configWindow.IsOpen = !this.configWindow.IsOpen;

		private void onPluginCommand(string command, string arguments) {
			string[] argumentsParts = arguments.Split();

			switch (argumentsParts[0]) {
				case "reset": {
					foreach (CustomComboPreset preset in Enum.GetValues(typeof(CustomComboPreset)).Cast<CustomComboPreset>())
						Service.configuration.EnabledActions.Remove(preset);

					Service.chatGui.Print("Unset all combos");
				}
				break;
				case "set": {
					string targetPreset = argumentsParts[1].ToLower();
					foreach (CustomComboPreset preset in Enum.GetValues(typeof(CustomComboPreset)).Cast<CustomComboPreset>()) {
						if (preset.ToString().ToLower() != targetPreset)
							continue;

						Service.configuration.EnabledActions.Add(preset);
						Service.chatGui.Print($"{preset} SET");
					}
				}
				break;
				case "toggle": {
					string targetPreset = argumentsParts[1].ToLower();
					foreach (CustomComboPreset preset in Enum.GetValues(typeof(CustomComboPreset)).Cast<CustomComboPreset>()) {
						if (preset.ToString().ToLower() != targetPreset)
							continue;

						if (Service.configuration.EnabledActions.Contains(preset)) {
							Service.configuration.EnabledActions.Remove(preset);
							Service.chatGui.Print($"{preset} UNSET");
						}
						else {
							Service.configuration.EnabledActions.Add(preset);
							Service.chatGui.Print($"{preset} SET");
						}
					}
				}
				break;
				case "unset": {
					string targetPreset = argumentsParts[1].ToLower();
					foreach (CustomComboPreset preset in Enum.GetValues(typeof(CustomComboPreset)).Cast<CustomComboPreset>()) {
						if (preset.ToString().ToLower() != targetPreset)
							continue;

						Service.configuration.EnabledActions.Remove(preset);
						Service.chatGui.Print($"{preset} UNSET");
					}
				}
				break;
				case "list": {
					string filter = argumentsParts.Length == 1 ? "all" : argumentsParts[1].ToLower();

					foreach (CustomComboPreset preset in Enum.GetValues(typeof(CustomComboPreset)).Cast<CustomComboPreset>()) {
						if (filter == "set") {
							if (Service.configuration.EnabledActions.Contains(preset))
								Service.chatGui.Print(preset.ToString());
						}
						else if (filter == "unset") {
							if (!Service.configuration.EnabledActions.Contains(preset))
								Service.chatGui.Print(preset.ToString());
						}
						else if (filter == "all") {
							Service.chatGui.Print(preset.ToString());
						}
					}
				}
				break;
				default:
					this.toggleConfigUi();
					break;
			}

			Service.pluginInterface.SavePluginConfig(Service.configuration);
		}
	}
}
