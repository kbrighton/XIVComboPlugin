namespace XIVComboVX.Attributes;

using System;

[AttributeUsage(AttributeTargets.Property)]
internal class ComboDetailSettingAttribute: Attribute {
	public CustomComboPreset Combo { get; }
	public float Min { get; } = float.MinValue;
	public float Max { get; } = float.MaxValue;
	public int Precision { get; } = 0;

	public string Label { get; }
	public string? Description { get; }

	public ComboDetailSettingAttribute(CustomComboPreset combo, string lbl, string? tooltip, float minimum, float maximum, int decimals) {
		this.Combo = combo;
		this.Label = lbl;
		this.Description = tooltip;
		if (decimals >= 0)
			this.Precision = decimals;
		if (!double.IsNaN(minimum) && double.IsFinite(minimum))
			this.Min = minimum;
		if (!double.IsNaN(maximum) && double.IsFinite(maximum))
			this.Max = maximum;
	}
	public ComboDetailSettingAttribute(CustomComboPreset combo, string lbl, string? tooltip, float minimum, float maximum)
		: this(combo, lbl, tooltip, minimum, maximum, 2) { }
	public ComboDetailSettingAttribute(CustomComboPreset combo, string lbl, string? tooltip) :
		this(combo, lbl, tooltip, float.NaN, float.NaN) { }
	public ComboDetailSettingAttribute(CustomComboPreset combo, string lbl) :
		this(combo, lbl, null) { }
}
