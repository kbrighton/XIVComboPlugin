using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using Dalamud.Interface.Windowing;
using Dalamud.Utility;

using ImGuiNET;

using XIVComboVX.Attributes;

namespace XIVComboVX {
	public class ConfigWindow: Window {

		private readonly Dictionary<string, List<(CustomComboPreset preset, CustomComboInfoAttribute info)>> groupedPresets;
		private static readonly Vector4 shadedColour = new(0.69f, 0.69f, 0.69f, 1.0f); // NICE (x3 COMBO)
		private static readonly Vector4 warningColour = new(200f / 255f, 25f / 255f, 35f / 255f, 1f);

		public ConfigWindow() : base("Custom Combo Setup") {
			this.RespectCloseHotkey = true;

			this.groupedPresets = Enum
				.GetValues<CustomComboPreset>()
				.Select(preset => (
					preset,
					info: preset.GetAttribute<CustomComboInfoAttribute>(),
					order: preset.GetAttribute<OrderedAttribute>()
				))
				.Where(data => data.info is not null)
				.GroupBy(data => data.info.JobName)
				.OrderBy(group => group.Key)
				.ToDictionary(
					group => group.Key,
					data => data.OrderBy(e => e.order?.Order ?? int.MaxValue).Select(e => (e.preset, e.info)).ToList()
				);

			this.SizeCondition = ImGuiCond.FirstUseEver;
			this.Size = new Vector2(740, 490);
		}

		public override void Draw() {
			ImGui.Text("This window allows you to enable and disable custom combos to your liking.");

			ImGui.BeginChild("scrolling", new Vector2(0, -1), true);

			ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(0, 5));

			int i = 1;
			foreach (string jobName in this.groupedPresets.Keys) {
				if (ImGui.CollapsingHeader(jobName)) {
					foreach ((CustomComboPreset preset, CustomComboInfoAttribute info) in this.groupedPresets[jobName]) {
						bool enabled = Service.configuration.IsEnabled(preset);
						CustomComboPreset[]? conflicts = preset.GetConflicts();

						ImGui.PushItemWidth(200);

						if (ImGui.Checkbox(info.FancyName, ref enabled)) {
							if (enabled) {
								Service.configuration.EnabledActions.Add(preset);
								foreach (CustomComboPreset conflict in conflicts) {
									Service.configuration.EnabledActions.Remove(conflict);
								}
							}
							else
								Service.configuration.EnabledActions.Remove(preset);

							Service.iconReplacer.UpdateEnabledActionIDs();
							Service.configuration.Save();
						}

						ImGui.PopItemWidth();

						if (preset.GetAttribute<ExperimentalAttribute>() is not null)
							ImGui.TextColored(warningColour, "EXPERIMENTAL - use at your own risk!");
						ImGui.TextColored(shadedColour, $"#{i}: {info.Description}");
						ImGui.Spacing();

						if (conflicts.Length > 0) {
							string? conflictText = conflicts.Select(preset =>
							{
								CustomComboInfoAttribute? info = preset.GetAttribute<CustomComboInfoAttribute>();
								return $"\n - {info.FancyName}";
							}).Aggregate((t1, t2) => $"{t1}{t2}");

							ImGui.TextColored(warningColour, $"Conflicts with:{conflictText}");
							if (ImGui.IsItemHovered()) {
								ImGui.BeginTooltip();
								ImGui.TextUnformatted("All conflicting features will be automatically disabled if this one is turned on");
								ImGui.EndTooltip();
							}
							ImGui.Spacing();
						}

						if (preset == CustomComboPreset.DancerDanceComboCompatibility && enabled) {
							int[] actions = Service.configuration.DancerDanceCompatActionIDs.Select(i => (int)i).ToArray();
							if (ImGui.InputInt("Emboite (Red) ActionID", ref actions[0], 0) ||
								ImGui.InputInt("Entrechat (Blue) ActionID", ref actions[1], 0) ||
								ImGui.InputInt("Jete (Green) ActionID", ref actions[2], 0) ||
								ImGui.InputInt("Pirouette (Yellow) ActionID", ref actions[3], 0)) {
								Service.configuration.DancerDanceCompatActionIDs = actions.Cast<uint>().ToArray();
								Service.iconReplacer.UpdateEnabledActionIDs();
								Service.configuration.Save();
							}
							ImGui.Spacing();
						}

						i++;
					}
				}
				else
					i += this.groupedPresets[jobName].Count;
			}

			ImGui.PopStyleVar();

			ImGui.EndChild();
		}
	}
}
