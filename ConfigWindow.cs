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
		private const int minWidth = 800;

		public ConfigWindow() : base("Custom Combo Setup") {
			this.RespectCloseHotkey = true;

			this.groupedPresets = Enum
				.GetValues<CustomComboPreset>()
				.Select(preset => (
					preset,
					info: preset.GetAttribute<CustomComboInfoAttribute>()
				))
				.Where(data => data.info is not null)
				.GroupBy(data => data.info.JobName)
				.OrderBy(group => group.Key)
				.ToDictionary(
					group => group.Key,
					data => data
						.OrderBy(e => e.info.Order)
						.ToList()
				);

			this.SizeCondition = ImGuiCond.FirstUseEver;
			this.Size = new(800, 800);
			this.SizeConstraints = new() {
				MinimumSize = new(800, 400),
				MaximumSize = new(int.MaxValue, int.MaxValue),
			};
		}

		public override void Draw() {
			ImGui.Text("This window allows you to enable and disable custom combos to your liking.");

			ImGui.BeginChild("scrolling", new Vector2(0, -1), true);

			ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(0, 5));

			int i = 1;
			foreach (string jobName in this.groupedPresets.Keys) {
				if (ImGui.CollapsingHeader(jobName)) {

					foreach ((CustomComboPreset preset, CustomComboInfoAttribute info) in this.groupedPresets[jobName]) {

						bool enabled = Service.Configuration.IsEnabled(preset);
						CustomComboPreset[]? conflicts = preset.GetConflicts();
						CustomComboPreset? parent = preset.GetParent();
						bool dangerous = preset.GetAttribute<DangerousAttribute>() is not null;
						bool experimental = preset.GetAttribute<ExperimentalAttribute>() is not null;

						ImGui.PushItemWidth(200);

						if (ImGui.Checkbox(info.FancyName, ref enabled)) {
							if (enabled) {
								Service.Configuration.EnabledActions.Add(preset);
								foreach (CustomComboPreset conflict in conflicts) {
									Service.Configuration.EnabledActions.Remove(conflict);
								}
							}
							else
								Service.Configuration.EnabledActions.Remove(preset);

							Service.IconReplacer.UpdateEnabledActionIDs();
							Service.Configuration.Save();
						}

						ImGui.PopItemWidth();

						string description = $"#{i}: {info.Description}";
						if (parent is not null)
							description += $"\nRequires {parent.GetAttribute<CustomComboInfoAttribute>().FancyName}";

						if (dangerous)
							ImGui.TextColored(warningColour, "UNSAFE - may potentially crash, use at your own risk!\n");
						else if (experimental)
							ImGui.TextColored(warningColour, "EXPERIMENTAL - not yet fully tested, may cause unwanted behaviour!");

						ImGui.PushTextWrapPos((this.Size?.X ?? minWidth) - 20);
						ImGui.TextColored(shadedColour, description);
						ImGui.PopTextWrapPos();
						ImGui.Spacing();

						if (conflicts.Length > 0) {
							string? conflictText = conflicts
								.Select(preset => $"\n - {preset.GetAttribute<CustomComboInfoAttribute>().FancyName}")
								.Aggregate((t1, t2) => $"{t1}{t2}");

							ImGui.TextColored(warningColour, $"Conflicts with:{conflictText}");
							if (ImGui.IsItemHovered()) {
								ImGui.BeginTooltip();
								ImGui.TextUnformatted("All conflicting features will be automatically disabled if this one is turned on.");
								ImGui.EndTooltip();
							}
							ImGui.Spacing();
						}

						if (preset == CustomComboPreset.DancerDanceComboCompatibility && enabled) {
							int[] actions = Service.Configuration.DancerDanceCompatActionIDs.Cast<int>().ToArray();
							bool changed = false;

							changed |= ImGui.InputInt("Emboite (Red) ActionID", ref actions[0], 0);
							changed |= ImGui.InputInt("Entrechat (Blue) ActionID", ref actions[1], 0);
							changed |= ImGui.InputInt("Jete (Green) ActionID", ref actions[2], 0);
							changed |= ImGui.InputInt("Pirouette (Yellow) ActionID", ref actions[3], 0);

							if (changed) {
								Service.Configuration.DancerDanceCompatActionIDs = actions.Cast<uint>().ToArray();
								Service.IconReplacer.UpdateEnabledActionIDs();
								Service.Configuration.Save();
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
