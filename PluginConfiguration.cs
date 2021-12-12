using System;
using System.Collections.Generic;
using System.Linq;

using Dalamud.Configuration;

using Newtonsoft.Json;

using XIVComboVX.Combos;

namespace XIVComboVX {
	[Serializable]
	public class PluginConfiguration: IPluginConfiguration {
		public int Version { get; set; } = 5;

		[JsonProperty("CrashGameOnLoadError")]
		public bool FailFastSetting = true;

		[JsonIgnore]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Instance-only when non-DEBUG")]
		public bool FailFastOnError =>
#if DEBUG
			true;
#else
			this.FailFastSetting || Service.Interface.IsDev;
#endif

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

		public void Upgrade() {
			if (this.Version == 5)
				return;
			else if (this.Version == 4) {
				this.Version = 5;
				this.EnabledActions = this.EnabledActions4
					.Select(preset => (int)preset switch {
						5 => 1900,
						6 => 1901,
						59 => 1902,
						7 => 1903,
						55 => 1904,
						86 => 1905,
						54 => 2000,
						82 => 2001,
						8 => 2100,
						9 => 2101,
						10 => 2102,
						78 => 2103,
						79 => 2104,
						67 => 2105,
						0 => 2200,
						1 => 2201,
						2 => 2202,
						44 => 2203,
						41 => 2300,
						42 => 2301,
						63 => 2302,
						74 => 2303,
						103 => 2400,
						35 => 2401,
						36 => 2402,
						76 => 2403,
						77 => 2404,
						25 => 2500,
						26 => 2501,
						56 => 2502,
						70 => 2503,
						71 => 2504,
						104 => 2506,
						101 => 2700,
						39 => 2704,
						40 => 2705,
						100 => 2800,
						29 => 2801,
						37 => 2802,
						17 => 3000,
						18 => 3001,
						19 => 3002,
						90 => 3004,
						92 => 3005,
						87 => 3006,
						88 => 3007,
						89 => 3008,
						23 => 3100,
						24 => 3101,
						47 => 3102,
						58 => 3103,
						66 => 3104,
						108 => 3105,
						3 => 3200,
						4 => 3201,
						57 => 3202,
						85 => 3203,
						98 => 3300,
						27 => 3301,
						75 => 3302,
						73 => 3303,
						11 => 3400,
						12 => 3401,
						13 => 3402,
						14 => 3403,
						15 => 3404,
						81 => 3406,
						60 => 3407,
						64 => 3408,
						61 => 3409,
						65 => 3410,
						99 => 3500,
						48 => 3501,
						49 => 3502,
						68 => 3503,
						53 => 3504,
						93 => 3505,
						94 => 3506,
						105 => 3507,
						109 => 3508,
						110 => 3509,
						20 => 3700,
						52 => 3702,
						22 => 3703,
						30 => 3704,
						83 => 3705,
						84 => 3706,
						106 => 3707,
						43 => 3800,
						50 => 3801,
						33 => 3802,
						102 => 3803,
						34 => 3804,
						31 => 3805,
						72 => 3806,
						_ => 0,
					})
					.Where(id => id is not 0)
					.SelectMany(id => new CustomComboPreset[] { (CustomComboPreset)id, ((CustomComboPreset)id).GetParent() ?? (CustomComboPreset)id })
					.ToHashSet();
				this.EnabledActions4 = new();
				this.Save();
			}
			else {
				// no.
				// in fact, why.
				// for that matter, HOW.
				this.Version = 5;
			}
		}

	}
}
