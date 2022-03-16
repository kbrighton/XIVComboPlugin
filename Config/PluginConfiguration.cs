using System;
using System.Collections.Generic;

using Dalamud.Configuration;

using Newtonsoft.Json;

using XIVComboVX.Attributes;
using XIVComboVX.Combos;

namespace XIVComboVX.Config {
	[Serializable]
	public class PluginConfiguration: IPluginConfiguration {
		public int Version { get; set; } = 6;

		#region Auto-linked combo detail settings

		[ComboDetailSetting(
			CustomComboPreset.WarriorSmartStormCombo,
			"Surging Tempest buff threshold",
			"When the Surging Tempest buff only has this many seconds left, switch to the Storm's Eye combo chain",
			0,
			30
		)]
		public float WarriorStormBuffSaverBuffTime { get; set; } = 7;

		[ComboDetailSetting(
			CustomComboPreset.MonkTwinSnakesFeature,
			"Disciplined Fist buff threshold",
			"When the Disciplined Fist buff only has this many seconds left, switch to True Strike",
			0,
			10
		)]
		public float MonkTwinSnakesBuffTime { get; set; } = 6;

		[ComboDetailSetting(
			CustomComboPreset.ReaperRegressDelayed,
			"Threshold buff threshold",
			"When the Threshold buff only has this many seconds left, switch to Regress",
			0,
			10
		)]
		public float ReaperThresholdBuffTime { get; set; } = 8.5F;

		[ComboDetailSetting(
			CustomComboPreset.DancerDanceComboCompatibility,
			"Emboite (Red) Action ID",
			"Defaults to Cascade (15989)"
		)]
		public uint DancerEmboiteRedActionID { get; set; } = DNC.Cascade;

		[ComboDetailSetting(
			CustomComboPreset.DancerDanceComboCompatibility,
			"Entrechat (Blue) Action ID",
			"Defaults to Fountain (15990)"
		)]
		public uint DancerEntrechatBlueActionID { get; set; } = DNC.Fountain;

		[ComboDetailSetting(
			CustomComboPreset.DancerDanceComboCompatibility,
			"Jete (Green) Action ID",
			"Defaults to Reverse Cascade (15991)"
		)]
		public uint DancerJeteGreenActionID { get; set; } = DNC.ReverseCascade;

		[ComboDetailSetting(
			CustomComboPreset.DancerDanceComboCompatibility,
			"Pirouette (Yellow) Action ID",
			"Defaults to Fountainfall (15992)"
		)]
		public uint DancerPirouetteYellowActionID { get; set; } = DNC.Fountainfall;

		#endregion

		[JsonProperty("HideDisabledFeaturesChildren")]
		public bool HideDisabledFeaturesChildren { get; set; } = false;

		[JsonProperty("RegisterCommonCommandName")]
		public bool RegisterCommonCommand { get; set; } = true;

		[JsonProperty("LastLoadedVersion")]
		public Version LastVersion { get; set; } = new("3.32.5"); // The last version released before the format change

		[JsonProperty("FirstRun")]
		public bool IsFirstRun { get; set; } = true;

		[JsonProperty("DisplayUpdateMessage")]
		public bool ShowUpdateMessage { get; set; } = true;

		[JsonProperty("EnabledActionsV5")]
		public HashSet<CustomComboPreset> EnabledActions = new();

		[Obsolete]
		public uint[] DancerDanceCompatActionIDs = Array.Empty<uint>();

		public bool IsEnabled(CustomComboPreset preset) => this.EnabledActions.Contains(preset);

		public void Save() => Service.Interface.SavePluginConfig(this);

		public void UpgradeIfNeeded() {
#pragma warning disable CS0612 // Type or member is obsolete
			if (this.Version == 5) {
				this.DancerEmboiteRedActionID = this.DancerDanceCompatActionIDs[0];
				this.DancerEntrechatBlueActionID = this.DancerDanceCompatActionIDs[1];
				this.DancerJeteGreenActionID = this.DancerDanceCompatActionIDs[2];
				this.DancerPirouetteYellowActionID = this.DancerDanceCompatActionIDs[3];
				this.DancerDanceCompatActionIDs = Array.Empty<uint>();
				this.Version++;
			}
#pragma warning restore CS0612 // Type or member is obsolete
		}

	}
}
