using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Interface.Windowing;
using Dalamud.Utility;

using ImGuiNET;

using XIVComboVX.Attributes;
using XIVComboVX.Combos;

// TODO: probably want to redesign this to allow for easier implementation of non-boolean settings, if I can find a good method
// I have had an idea! It's gonna involve reflecting shit, but we're already doing that, and likewise with mapping detail settings to presets.
// Basic thinking (so far) is a 1:N mapping of presets and a set of details for the sub-option, including a label, tooltip, min/max, and type.
// Included will be a reflection-accessor to allow getting and setting the value, but that raises the concern of speed.
// The existing value will have to be gotten every draw call, albeit only for presets that are visible AND enabled.
// But there'll still be SOME level of performance impact, unless I can figure out how to cache it somehow... or just find another solution.

namespace XIVComboVX.Config {
	public class ConfigWindow: Window {

		private readonly Dictionary<string, List<(CustomComboPreset preset, CustomComboInfoAttribute info)>> groupedPresets;
		private readonly Dictionary<CustomComboPreset, HashSet<(CustomComboPreset Preset, CustomComboInfoAttribute Info)>> parentToChildrenPresets = new();
		private readonly Dictionary<CustomComboPreset, (CustomComboPreset Preset, CustomComboInfoAttribute Info)> childToParentPresets = new();

		private static readonly Vector4 shadedColour = new(0.69f, 0.69f, 0.69f, 1.0f); // NICE (x3 COMBO)
		private static readonly Vector4 warningColour = new(200f / 255f, 25f / 255f, 35f / 255f, 1f);

		private const int minWidth = 900;

		public ConfigWindow() : base("Custom Combo Setup", ImGuiWindowFlags.MenuBar) {
			this.RespectCloseHotkey = true;

			List<(CustomComboPreset preset, CustomComboInfoAttribute info)> realPresets = Enum
				.GetValues<CustomComboPreset>()
				.Where(preset => (int)preset >= 100)
				.Select(preset => (
					preset,
					info: preset.GetAttribute<CustomComboInfoAttribute>()
				))
				.Where(preset => preset.info is not null)
				.OrderBy(preset => preset.info.Order)
				.ToList();

			this.groupedPresets = realPresets
				.GroupBy(data => data.info.JobName)
				.OrderBy(group => group.Key)
				.ToDictionary(
					group => group.Key,
					data => data
						.OrderBy(e => e.info.Order)
						.ToList()
				);

			foreach ((CustomComboPreset preset, CustomComboInfoAttribute info) in realPresets) {
				CustomComboPreset? parent = preset.GetParent();
				CustomComboInfoAttribute? parentInfo = parent?.GetAttribute<CustomComboInfoAttribute>();
				if (parent is not null && parentInfo is not null) {
					this.childToParentPresets.Add(preset, (parent.Value, parentInfo));
					if (!this.parentToChildrenPresets.ContainsKey(parent.Value)) {
						this.parentToChildrenPresets[parent.Value] = new();
					}
					this.parentToChildrenPresets[parent.Value].Add((preset, info));
				}
			}

			this.SizeCondition = ImGuiCond.FirstUseEver;
			this.Size = new(minWidth, 800);
			this.SizeConstraints = new() {
				MinimumSize = new(minWidth, 400),
				MaximumSize = new(int.MaxValue, int.MaxValue),
			};
		}

		public override void Draw() {

			bool hideChildren = Service.Configuration.HideDisabledFeaturesChildren;
			bool registerNormalCommand = Service.Configuration.RegisterCommonCommand;
			bool showUpdateMessage = Service.Configuration.ShowUpdateMessage;

			if (ImGui.BeginMenuBar()) {

				if (ImGui.BeginMenu("Settings")) {

					bool clickCollapse = ImGui.MenuItem("Collapse disabled features", "", ref hideChildren);
					if (ImGui.IsItemHovered()) {
						ImGui.BeginTooltip();
						ImGui.Text("If enabled, children of disabled features will be hidden.");
						ImGui.Text("A message will be shown under any disabled feature that");
						ImGui.Text("has child features, so you can still tell that there are more");
						ImGui.Text("features available dependent on the disabled one.");
						ImGui.EndTooltip();
					}
					if (clickCollapse) {
						Service.Configuration.HideDisabledFeaturesChildren = hideChildren;
						Service.Configuration.Save();
					}

					bool clickRegister = ImGui.MenuItem($"Register {XIVComboVX.command}", "", ref registerNormalCommand);
					if (ImGui.IsItemHovered()) {
						ImGui.BeginTooltip();
						ImGui.Text($"If enabled, {Service.Plugin.Name} will attempt to register the {XIVComboVX.command} command.");
						ImGui.Text("This is the command generally used by all forks of XIVCombo, which");
						ImGui.Text("means it will conflict if you have multiple forks installed. This");
						ImGui.Text("isn't advised to begin with, but this option will allow for slightly");
						ImGui.Text("better compatibility than would otherwise be available, at least.");
						ImGui.Text("");
						ImGui.Text("This plugin always registers its own (separate) command to open the");
						ImGui.Text("settings window, regardless of whether the default one is also used.");
						ImGui.Text("");
						ImGui.Text("This options only takes effect after a restart.");
						ImGui.EndTooltip();
					}
					if (clickRegister) {
						Service.Configuration.RegisterCommonCommand = registerNormalCommand;
						Service.Configuration.Save();
					}

					bool clickUpdates = ImGui.MenuItem("Show update messages", "", ref showUpdateMessage);
					if (ImGui.IsItemHovered()) {
						ImGui.BeginTooltip();
						ImGui.Text("If enabled, an alert will be shown in your chatlog whenever the plugin updates.");
						ImGui.Text("The message includes the old version, the new version, and a clickable 'link' to");
						ImGui.Text("open the plugin configuration window.");
						ImGui.EndTooltip();
					}
					if (clickUpdates) {
						Service.Configuration.ShowUpdateMessage = showUpdateMessage;
						Service.Configuration.Save();
					}

					ImGui.EndMenu();
				}

				if (ImGui.BeginMenu("Utilities")) {

					bool clickReset = ImGui.MenuItem("Reset configuration");
					if (ImGui.IsItemHovered()) {
						ImGui.BeginTooltip();
						ImGui.Text("This will clear ALL enabled action replacers, as well as");
						ImGui.Text("resetting DNC's Dance Step Feature to the default actions.");
						ImGui.TextColored(warningColour, "THIS CANNOT BE UNDONE!");
						ImGui.EndTooltip();
					}
					if (clickReset) {
						Service.Configuration.EnabledActions.Clear();
						Service.Configuration.DancerDanceCompatActionIDs = new[] {
							DNC.Cascade,
							DNC.Flourish,
							DNC.FanDance1,
							DNC.FanDance2,
						};
						Service.Configuration.Save();
					}

					ImGui.EndMenu();
				}

#if DEBUG
				if (ImGui.BeginMenu("Debugging")) {

					bool clickDebug = ImGui.MenuItem("Snapshot debug messages");
					if (ImGui.IsItemHovered()) {
						ImGui.BeginTooltip();
						ImGui.Text("This enables a snapshot of debug messages in the dalamud log.");
						ImGui.Text("They will appear in your log file and also in the /xllog window.");
						ImGui.EndTooltip();
					}
					if (clickDebug) {
						Service.Logger.EnableNextTick();
					}

					PlayerCharacter? player = Service.Client.LocalPlayer;
					if (player is null) {
						ImGui.MenuItem("Not logged in", false);
					}
					else {
						ImGui.MenuItem($"{player.Name}: {player.ClassJob.GameData!.Abbreviation.ToString().ToUpper()} ({player.ClassJob.Id})", false);
					}

					ImGui.EndMenu();
				}
#endif

				ImGui.EndMenuBar();
			}

			int i = 1;
			foreach (string jobName in this.groupedPresets.Keys) {
				if (ImGui.CollapsingHeader(jobName)) {

					ImGui.PushID($"settings-{jobName}");

					foreach ((CustomComboPreset preset, CustomComboInfoAttribute info) in this.groupedPresets[jobName]) {
						if (this.childToParentPresets.ContainsKey(preset))
							continue;

						this.drawPreset(preset, info, ref i);
					}

					ImGui.PopID();

				}
				else {
					i += this.groupedPresets[jobName].Count;
				}
			}

		}

		private void drawPreset(CustomComboPreset preset, CustomComboInfoAttribute info, ref int i) {

			bool enabled = Service.Configuration.IsEnabled(preset);
			CustomComboPreset[] conflicts = preset.GetConflicts();
			CustomComboPreset? parent = preset.GetParent();
			bool dangerous = preset.GetAttribute<DangerousAttribute>() is not null;
			bool experimental = preset.GetAttribute<ExperimentalAttribute>() is not null;
			bool hideChildren = Service.Configuration.HideDisabledFeaturesChildren;
			bool hasChildren = this.parentToChildrenPresets.TryGetValue(preset, out HashSet<(CustomComboPreset Preset, CustomComboInfoAttribute Info)>? children)
				&& children is not null;

			ImGui.PushItemWidth(200);

			if (ImGui.Checkbox(info.FancyName, ref enabled)) {
				if (enabled) {

					Service.Configuration.EnabledActions.Add(preset);
					this.enableParentPresets(preset);

					foreach (CustomComboPreset conflict in conflicts) {
						Service.Configuration.EnabledActions.Remove(conflict);
					}

				}
				else {
					Service.Configuration.EnabledActions.Remove(preset);
				}

				Service.Configuration.Save();
			}

			ImGui.PopItemWidth();

			if (dangerous)
				ImGui.TextColored(warningColour, "UNSAFE - may potentially crash, use at your own risk!\n");
			else if (experimental)
				ImGui.TextColored(warningColour, "EXPERIMENTAL - not yet fully tested, may cause unwanted behaviour!");

			string description = $"#{i}: {info.Description}";

			if (conflicts.Length > 0) {
				string[] conflictNames = conflicts
					.Select(p => p.GetAttribute<CustomComboInfoAttribute>().FancyName)
					.ToArray();
				description += $"\nConflicts with: {string.Join(", ", conflictNames)}";
			}
			if (hasChildren && hideChildren && !enabled)
				description += "\nThis preset has one or more children.";

			ImGui.PushTextWrapPos((this.Size?.Y ?? minWidth) - 20);
			ImGui.TextColored(shadedColour, description);
			ImGui.PopTextWrapPos();
			ImGui.Spacing();

			if (preset is CustomComboPreset.DancerDanceComboCompatibility && enabled) {
				int[] actions = Service.Configuration.DancerDanceCompatActionIDs.Cast<int>().ToArray();
				bool changed = false;

				changed |= ImGui.InputInt("Emboite (Red) ActionID", ref actions[0], 0);
				changed |= ImGui.InputInt("Entrechat (Blue) ActionID", ref actions[1], 0);
				changed |= ImGui.InputInt("Jete (Green) ActionID", ref actions[2], 0);
				changed |= ImGui.InputInt("Pirouette (Yellow) ActionID", ref actions[3], 0);

				if (changed) {
					Service.Configuration.DancerDanceCompatActionIDs = actions.Cast<uint>().ToArray();
					Service.Configuration.Save();
				}

				ImGui.Spacing();
			}

			i++;

			if (hasChildren && (!hideChildren || enabled)) {
				ImGui.Indent();
				ImGui.Indent();
				foreach ((CustomComboPreset childPreset, CustomComboInfoAttribute childInfo) in children!) {
					this.drawPreset(childPreset, childInfo, ref i);
				}
				ImGui.Unindent();
				ImGui.Unindent();
			}

		}

		private void enableParentPresets(CustomComboPreset original) {
			CustomComboPreset preset = original;

			while (this.childToParentPresets.TryGetValue(preset, out (CustomComboPreset Preset, CustomComboInfoAttribute Info) parent)) {

				if (!Service.Configuration.EnabledActions.Contains(parent.Preset)) {
					Service.Configuration.EnabledActions.Add(parent.Preset);

					foreach (CustomComboPreset conflict in parent.Preset.GetConflicts()) {
						Service.Configuration.EnabledActions.Remove(conflict);
					}
				}

				preset = parent.Preset;
			}
		}

	}
}
