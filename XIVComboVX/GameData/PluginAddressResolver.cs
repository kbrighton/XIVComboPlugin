using System;
using System.Text;

namespace PrincessRTFM.XIVComboVX.GameData;

internal class PluginAddressResolver {
	private const string AddrFmtSpec = "X16";

	public Exception? LoadFailReason { get; private set; }
	public bool LoadSuccessful => this.LoadFailReason is null;

	public IntPtr ComboTimer { get; private set; } = IntPtr.Zero;
	public string ComboTimerAddr => this.ComboTimer.ToInt64().ToString(AddrFmtSpec);

	public IntPtr LastComboMove => this.ComboTimer + 0x4;
	public string LastComboMoveAddr => this.LastComboMove.ToInt64().ToString(AddrFmtSpec);

	public IntPtr GetAdjustedActionId { get; private set; } = IntPtr.Zero;
	public string GetAdjustedActionIdAddr => this.GetAdjustedActionId.ToInt64().ToString(AddrFmtSpec);

	public IntPtr IsActionIdReplaceable { get; private set; } = IntPtr.Zero;
	public string IsActionIdReplaceableAddr => this.IsActionIdReplaceable.ToInt64().ToString(AddrFmtSpec);


	internal void Setup() {
		try {
			Service.Log.Information("Scanning for ComboTimer signature");
			this.ComboTimer = Service.SigScanner.GetStaticAddressFromSig("F3 0F 11 05 ?? ?? ?? ?? 48 83 C7 08");

			Service.Log.Information("Scanning for GetAdjustedActionId signature");
			this.GetAdjustedActionId = Service.SigScanner.ScanText("E8 ?? ?? ?? ?? 89 03 8B 03");  // Client::Game::ActionManager.GetAdjustedActionId

			Service.Log.Information("Scanning for IsActionIdReplaceable signature");
			this.IsActionIdReplaceable = Service.SigScanner.ScanText("E8 ?? ?? ?? ?? 84 C0 74 4C 8B D3");
		}
		catch (Exception ex) {
			this.LoadFailReason = ex;
			StringBuilder msg = new();
			msg.AppendLine("Address scanning failed, plugin cannot load.");
			msg.AppendLine("Please present this error message to the developer.");
			msg.AppendLine();
			msg.Append("Signature scan failed for ");
			if (this.ComboTimer == IntPtr.Zero)
				msg.Append("ComboTimer");
			else if (this.GetAdjustedActionId == IntPtr.Zero)
				msg.Append("GetAdjustedActionId");
			else if (this.IsActionIdReplaceable == IntPtr.Zero)
				msg.Append("IsActionIdReplaceable");
			msg.AppendLine(":");
			msg.Append(ex.ToString());
			Service.Log.Fatal(msg.ToString());
			return;
		}

		Service.Log.Information("Address resolution successful");

		Service.Log.Information($"GetAdjustedActionId 0x{this.GetAdjustedActionIdAddr}");
		Service.Log.Information($"IsIconReplaceable   0x{this.IsActionIdReplaceableAddr}");
		Service.Log.Information($"ComboTimer          0x{this.ComboTimerAddr}");
		Service.Log.Information($"LastComboMove       0x{this.LastComboMoveAddr}");
	}
}
