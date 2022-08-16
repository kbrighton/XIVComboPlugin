namespace XIVComboVX.Config;

using System;
using System.Reflection;

using ImGuiNET;

using XIVComboVX.Attributes;

internal class ComboDetailSetting {
	public PropertyInfo Property { get; }
	public CustomComboPreset Combo { get; }
	public Type Type { get; }
	public ImGuiDataType ImGuiType { get; }

	public int Precision { get; }
	public float Max { get; }
	public float Min { get; }
	public float Val {
		get => Convert.ToSingle(this.Property.GetValue(Service.Configuration) ?? default(float));
		set {
			if (float.IsNaN(value) || float.IsInfinity(value) || value > this.Max || value < this.Min)
				return;
			// Apparently these can't be switch/case'd, because the `typeof(type)` expression isn't a constant
			if (this.Type == typeof(int))
				this.Property.SetValue(Service.Configuration, (int)value);
			else if (this.Type == typeof(uint))
				this.Property.SetValue(Service.Configuration, (uint)value);
			else if (this.Type == typeof(float))
				this.Property.SetValue(Service.Configuration, value);
			else
				throw new ArgumentException($"Cannot assign value ({value.GetType().Name} {value}) to property ({this.Type.Name} {this.Property.Name})");
		}
	}

	public string Label { get; }
	public string? Description { get; }

	internal ComboDetailSetting(PropertyInfo prop, ComboDetailSettingAttribute attr) {
		this.Property = prop;
		this.Type = prop.PropertyType;
		this.ImGuiType = this.Type == typeof(int)
			? ImGuiDataType.S32
			: this.Type == typeof(uint)
			? ImGuiDataType.U32
			: ImGuiDataType.Float;
		this.Min = Math.Max(
			attr.Min,
			this.ImGuiType switch {
				ImGuiDataType.S32 => int.MinValue,
				ImGuiDataType.U32 => uint.MinValue,
				ImGuiDataType.Float => float.MinValue,
				_ => 0,
			}
		);
		this.Max = Math.Min(
			attr.Max,
			this.ImGuiType switch {
				ImGuiDataType.S32 => int.MaxValue,
				ImGuiDataType.U32 => uint.MaxValue,
				ImGuiDataType.Float => float.MaxValue,
				_ => 0,
			}
		);
		this.Combo = attr.Combo;
		this.Label = attr.Label;
		this.Description = attr.Description;
		this.Precision = this.ImGuiType == ImGuiDataType.Float ? attr.Precision : 0;
	}
}
