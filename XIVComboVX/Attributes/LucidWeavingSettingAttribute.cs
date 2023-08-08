namespace PrincessRTFM.XIVComboVX.Attributes;

using System;

[AttributeUsage(AttributeTargets.Property)]
internal class LucidWeavingSettingAttribute: ComboDetailSettingAttribute {
	internal const int
		lucidWeaveManaMin = 1000,
		lucidWeaveManaMax = 8000;
	internal const string
		lucidWeaveName = "MP threshold",
		lucidWeaveDesc = "When your MP is below this limit, change this button into Lucid Dreaming while weaving\n(LD restores about one third of maximum MP, for reference)";

	public LucidWeavingSettingAttribute(CustomComboPreset combo) : base(combo, lucidWeaveName, lucidWeaveDesc, lucidWeaveManaMin, lucidWeaveManaMax) { }
}
