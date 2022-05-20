namespace XIVComboVX.GameData;

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Explicit)]
internal struct CooldownData {
	[FieldOffset(0x0)] private readonly bool isCooldown;
	[FieldOffset(0x4)] private readonly uint actionID;
	[FieldOffset(0x8)] private readonly float cooldownElapsed;
	[FieldOffset(0xC)] private readonly float cooldownTotal;

	public bool IsCooldown {
		get {
			(ushort cur, ushort max) = Service.DataCache.GetMaxCharges(this.ActionID);
			return cur == max
				? this.isCooldown
				: this.cooldownElapsed < this.CooldownTotal;
		}
	}

	public uint ActionID => this.actionID;

	/// <summary>
	/// Elapsed time on the cooldown, covering only the number of max charges available at current level (if applicable)
	/// </summary>
	public float CooldownElapsed => this.cooldownElapsed == 0
		? 0
		: this.cooldownElapsed > this.CooldownTotal
			? 0
			: this.cooldownElapsed;

	/// <summary>
	/// Total cooldown time, covering only the number of max charges available at current level (if applicable)
	/// </summary>
	public float CooldownTotal {
		get {
			if (this.cooldownTotal == 0)
				return 0;

			(ushort cur, ushort max) = Service.DataCache.GetMaxCharges(this.ActionID);
			if (cur == max)
				return this.cooldownTotal;

			float total = this.cooldownTotal / max * cur;

			return this.cooldownElapsed > total
				? 0
				: total;
		}
	}

	/// <summary>
	/// Remaining cooldown time, covering only the number of max charges available at current level (if applicable)
	/// </summary>
	public float CooldownRemaining => this.IsCooldown ? this.CooldownTotal - this.CooldownElapsed : 0;

	/// <summary>
	/// The maximum number of charges available at the current level
	/// </summary>
	public ushort MaxCharges => Service.DataCache.GetMaxCharges(this.ActionID).Current;

	/// <summary>
	/// Whether the action has any charges available at the current level
	/// </summary>
	public bool HasCharges => this.MaxCharges > 1;

	/// <summary>
	/// The number of charges left at the current level
	/// </summary>
	public ushort RemainingCharges {
		get {
			(ushort cur, ushort _) = Service.DataCache.GetMaxCharges(this.ActionID);

			return !this.IsCooldown
				? cur
				: (ushort)(this.CooldownElapsed / (this.CooldownTotal / this.MaxCharges));
		}
	}

	/// <summary>
	/// The cooldown time remaining for the currently-recovering charge.
	/// </summary>
	public float ChargeCooldownRemaining {
		get {
			if (!this.IsCooldown)
				return 0;

			(ushort cur, ushort _) = Service.DataCache.GetMaxCharges(this.ActionID);

			return this.CooldownRemaining % (this.CooldownTotal / cur);
		}
	}

	public string DebugLabel
		=> $"{(this.IsCooldown ? "on" : "off")} cd, {(this.HasCharges ? this.RemainingCharges + "/" + this.MaxCharges : "no")} charges, {this.CooldownElapsed}/{this.CooldownTotal} ({this.CooldownRemaining})";
}
