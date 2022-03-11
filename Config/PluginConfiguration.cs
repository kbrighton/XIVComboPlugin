using System;
using System.Collections.Generic;

using Dalamud.Configuration;

using Newtonsoft.Json;

using XIVComboVX.Combos;

namespace XIVComboVX.Config {
	[Serializable]
	public class PluginConfiguration: IPluginConfiguration {
		public int Version { get; set; } = 5;

		[JsonProperty("HideDisabledFeaturesChildren")]
		public bool HideDisabledFeaturesChildren { get; set; } = false;

		[JsonProperty("RegisterCommonCommandName")]
		public bool RegisterCommonCommand { get; set; } = true;

		[JsonProperty("LastLoadedVersion")]
		public Version LastVersion { get; set; } = new("3.32.5"); // The last version released before the format change

		[JsonProperty("DisplayUpdateMessage")]
		public bool ShowUpdateMessage { get; set; } = true;

		[JsonProperty("EnabledActionsV5")]
		public HashSet<CustomComboPreset> EnabledActions = new();

		public uint[] DancerDanceCompatActionIDs = new[] {
			DNC.Cascade,
			DNC.Flourish,
			DNC.FanDance1,
			DNC.FanDance2,
		};

		public bool IsEnabled(CustomComboPreset preset) => this.EnabledActions.Contains(preset);

		public void Save() => Service.Interface.SavePluginConfig(this);

	}
}
