using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using Dalamud.Game.Command;
using Dalamud.Game.Text;
using Dalamud.Plugin;

using ImGuiNET;

namespace XIVComboVeryExpandedPlugin {
	public sealed class XIVComboVeryExpandedPlugin: IDalamudPlugin {
		public string Name => "XIV Combo Very Expanded Plugin";

		private readonly string Command = "/pcombo";
		internal XIVComboVeryExpandedConfiguration Configuration;
		internal const int CURRENT_CONFIG_VERSION = 4;

		internal DalamudPluginInterface Interface;
		private IconReplacer IconReplacer;

		private Dictionary<string, List<(CustomComboPreset preset, CustomComboInfoAttribute info)>> GroupedPresets;

		public void Initialize(DalamudPluginInterface pluginInterface) {
			this.Interface = pluginInterface;

			this.Interface.CommandManager.AddHandler(this.Command, new CommandInfo(this.OnCommandDebugCombo) {
				HelpMessage = "Open a window to edit custom combo settings.",
				ShowInHelp = true
			});

			this.Configuration = pluginInterface.GetPluginConfig() as XIVComboVeryExpandedConfiguration ?? new XIVComboVeryExpandedConfiguration();
			if (this.Configuration.Version < CURRENT_CONFIG_VERSION) {
				this.Configuration.Upgrade();
				this.SaveConfiguration();
			}

			this.IconReplacer = new IconReplacer(pluginInterface, this.Configuration);

			this.Interface.UiBuilder.OnOpenConfigUi += (sender, args) => this.isImguiComboSetupOpen = true;
			this.Interface.UiBuilder.OnBuildUi += this.UiBuilder_OnBuildUi;

			this.GroupedPresets = Enum
				.GetValues(typeof(CustomComboPreset))
				.Cast<CustomComboPreset>()
				.Select(preset => (preset, info: preset.GetAttribute<CustomComboInfoAttribute>()))
				.Where(presetWithInfo => presetWithInfo.info != null)
				.OrderBy(presetWithInfo => presetWithInfo.info.JobName)
				.GroupBy(presetWithInfo => presetWithInfo.info.JobName)
				.ToDictionary(presetWithInfos => presetWithInfos.Key,
							  presetWithInfos => presetWithInfos.ToList());
		}

		private bool isImguiComboSetupOpen = false;

		private void SaveConfiguration() {
			this.Interface.SavePluginConfig(this.Configuration);
		}

		private void UiBuilder_OnBuildUi() {
			if (!this.isImguiComboSetupOpen)
				return;

			ImGui.SetNextWindowSize(new Vector2(740, 490), ImGuiCond.FirstUseEver);

			ImGui.Begin("Custom Combo Setup", ref this.isImguiComboSetupOpen);

			ImGui.Text("This window allows you to enable and disable custom combos to your liking.");

			ImGui.BeginChild("scrolling", new Vector2(0, -1), true);

			ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(0, 5));

			int i = 1;
			foreach (string jobName in this.GroupedPresets.Keys) {
				if (ImGui.CollapsingHeader(jobName)) {
					foreach ((CustomComboPreset preset, CustomComboInfoAttribute info) in this.GroupedPresets[jobName]) {
						bool enabled = this.Configuration.IsEnabled(preset);

						ImGui.PushItemWidth(200);

						if (ImGui.Checkbox(info.FancyName, ref enabled)) {
							if (enabled)
								this.Configuration.EnabledActions.Add(preset);
							else
								this.Configuration.EnabledActions.Remove(preset);
							this.IconReplacer.UpdateEnabledActionIDs();
							this.SaveConfiguration();
						}

						ImGui.PopItemWidth();

						ImGui.TextColored(new Vector4(0.68f, 0.68f, 0.68f, 1.0f), $"#{i}: {info.Description}");
						ImGui.Spacing();

						if (preset == CustomComboPreset.DancerDanceComboCompatibility && enabled) {
							int[] actions = this.Configuration.DancerDanceCompatActionIDs.Select(i => (int)i).ToArray();
							if (ImGui.InputInt("Emboite (Red) ActionID", ref actions[0], 0) ||
								ImGui.InputInt("Entrechat (Blue) ActionID", ref actions[1], 0) ||
								ImGui.InputInt("Jete (Green) ActionID", ref actions[2], 0) ||
								ImGui.InputInt("Pirouette (Yellow) ActionID", ref actions[3], 0)) {
								this.Configuration.DancerDanceCompatActionIDs = actions.Select(i => (uint)i).ToArray();
								this.IconReplacer.UpdateEnabledActionIDs();
								this.SaveConfiguration();
							}
							ImGui.Spacing();
						}

						i++;
					}
				}
				else {
					i += this.GroupedPresets[jobName].Count;
				}
			}

			ImGui.PopStyleVar();

			ImGui.EndChild();

			ImGui.End();
		}

		public void Dispose() {
			this.IconReplacer.Dispose();

			this.Interface.CommandManager.RemoveHandler(this.Command);

			this.Interface.Dispose();
		}

		private void OnCommandDebugCombo(string command, string arguments) {
			string[] argumentsParts = arguments.Split();

			switch (argumentsParts[0]) {
				case "setall": {
					foreach (CustomComboPreset preset in Enum.GetValues(typeof(CustomComboPreset)).Cast<CustomComboPreset>())
						this.Configuration.EnabledActions.Add(preset);

					this.Interface.Framework.Gui.Chat.Print("All SET");
				}
				break;
				case "unsetall": {
					foreach (CustomComboPreset preset in Enum.GetValues(typeof(CustomComboPreset)).Cast<CustomComboPreset>())
						this.Configuration.EnabledActions.Remove(preset);

					this.Interface.Framework.Gui.Chat.Print("All UNSET");
				}
				break;
				case "set": {
					string targetPreset = argumentsParts[1].ToLower();
					foreach (CustomComboPreset preset in Enum.GetValues(typeof(CustomComboPreset)).Cast<CustomComboPreset>()) {
						if (preset.ToString().ToLower() != targetPreset)
							continue;

						this.Configuration.EnabledActions.Add(preset);
						this.Interface.Framework.Gui.Chat.Print($"{preset} SET");
					}
				}
				break;
				case "secrets":
					this.Interface.Framework.Gui.Chat.Print("Secret combos have been removed in VX; all combos are always available.");
					break;
				case "toggle": {
					string targetPreset = argumentsParts[1].ToLower();
					foreach (CustomComboPreset preset in Enum.GetValues(typeof(CustomComboPreset)).Cast<CustomComboPreset>()) {
						if (preset.ToString().ToLower() != targetPreset)
							continue;

						if (this.Configuration.EnabledActions.Contains(preset)) {
							this.Configuration.EnabledActions.Remove(preset);
							this.Interface.Framework.Gui.Chat.Print($"{preset} UNSET");
						}
						else {
							this.Configuration.EnabledActions.Add(preset);
							this.Interface.Framework.Gui.Chat.Print($"{preset} SET");
						}
					}
				}
				break;
				case "unset": {
					string targetPreset = argumentsParts[1].ToLower();
					foreach (CustomComboPreset preset in Enum.GetValues(typeof(CustomComboPreset)).Cast<CustomComboPreset>()) {
						if (preset.ToString().ToLower() != targetPreset)
							continue;

						this.Configuration.EnabledActions.Remove(preset);
						this.Interface.Framework.Gui.Chat.Print($"{preset} UNSET");
					}
				}
				break;
				case "list": {
					string filter;
					if (argumentsParts.Length == 1)
						filter = "all";
					else
						filter = argumentsParts[1].ToLower();

					foreach (CustomComboPreset preset in Enum.GetValues(typeof(CustomComboPreset)).Cast<CustomComboPreset>()) {
						if (filter == "set") {
							if (this.Configuration.EnabledActions.Contains(preset))
								this.Interface.Framework.Gui.Chat.Print(preset.ToString());
						}
						else if (filter == "unset") {
							if (!this.Configuration.EnabledActions.Contains(preset))
								this.Interface.Framework.Gui.Chat.Print(preset.ToString());
						}
						else if (filter == "all") {
							this.Interface.Framework.Gui.Chat.Print(preset.ToString());
						}
					}
				}
				break;
				default:
					this.isImguiComboSetupOpen = true;
					break;
			}

			this.Interface.SavePluginConfig(this.Configuration);
		}
	}
}
