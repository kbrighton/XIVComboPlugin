using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XIVComboVX;

namespace XIVComboVX.Attributes {
	[AttributeUsage(AttributeTargets.Field)]
	internal class ParentPresetAttribute: Attribute {
		public CustomComboPreset Parent { get; }
		internal ParentPresetAttribute(CustomComboPreset required) {
			this.Parent = required;
		}
	}
}
