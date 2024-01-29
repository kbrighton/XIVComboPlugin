using System;

namespace PrincessRTFM.XIVComboVX.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class ParentPresetAttribute: Attribute {
	public CustomComboPreset Parent { get; }
	internal ParentPresetAttribute(CustomComboPreset required) => this.Parent = required;
}
