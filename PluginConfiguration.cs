using System;
using System.Collections.Generic;

using Dalamud.Configuration;

using Newtonsoft.Json;

using XIVComboVX.Combos;

namespace XIVComboVX {
	[Serializable]
	public class PluginConfiguration: IPluginConfiguration {
		public int Version { get; set; } = 5;

		[JsonProperty("CrashGameOnLoadError")]
		public bool FailFastSetting = true;

		[JsonProperty("EnabledActionsV5")]
		public HashSet<CustomComboPreset> EnabledActions = new();

		[JsonProperty("EnabledActionsV4")]
		public HashSet<CustomComboPreset> EnabledActions4 = new();

		public uint[] DancerDanceCompatActionIDs = new uint[]
		{
			DNC.Cascade,
			DNC.Flourish,
			DNC.FanDance1,
			DNC.FanDance2,
		};

		public bool IsEnabled(CustomComboPreset preset) => this.EnabledActions.Contains(preset);

		public void Save() => Service.Interface.SavePluginConfig(this);

	}
}
