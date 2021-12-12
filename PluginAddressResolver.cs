using System;
using System.Text;

using Dalamud.Game;
using Dalamud.Logging;

namespace XIVComboVX {
	internal class PluginAddressResolver: BaseAddressResolver {
		private const string addrFmtSpec = "X16";

		public Exception? LoadFailReason { get; private set; }
		public bool LoadSuccessful => this.LoadFailReason is null;

		public IntPtr ComboTimer { get; private set; } = IntPtr.Zero;
		public string ComboTimerAddr => this.ComboTimer.ToInt64().ToString(addrFmtSpec);

		public IntPtr LastComboMove => this.ComboTimer + 0x4;
		public string LastComboMoveAddr => this.LastComboMove.ToInt64().ToString(addrFmtSpec);

		public IntPtr GetAdjustedActionId { get; private set; } = IntPtr.Zero;
		public string GetAdjustedActionIdAddr => this.GetAdjustedActionId.ToInt64().ToString(addrFmtSpec);

		public IntPtr IsActionIdReplaceable { get; private set; } = IntPtr.Zero;
		public string IsActionIdReplaceableAddr => this.IsActionIdReplaceable.ToInt64().ToString(addrFmtSpec);

		public IntPtr GetActionCooldown { get; private set; } = IntPtr.Zero;
		public string GetActionCooldownAddr => this.GetActionCooldown.ToInt64().ToString(addrFmtSpec);

		protected override void Setup64Bit(SigScanner scanner) {
			try {
				this.ComboTimer = scanner.GetStaticAddressFromSig("48 89 2D ?? ?? ?? ?? 85 C0 74 0F");

				this.GetAdjustedActionId = scanner.ScanText("E8 ?? ?? ?? ?? 8B F8 3B DF");  // Client::Game::ActionManager.GetAdjustedActionId

				this.IsActionIdReplaceable = scanner.ScanText("81 F9 ?? ?? ?? ?? 7F 35");

				this.GetActionCooldown = scanner.ScanText("E8 ?? ?? ?? ?? 0F 57 FF 48 85 C0");
			}
			catch (Exception ex) {
				this.LoadFailReason = ex;
				bool die = Service.Configuration.FailFastOnError;
				StringBuilder msg = new();
				msg.Append("Address scanning failed, plugin cannot load.");
				if (die)
					msg.Append(" The game will now exit.");
				msg.AppendLine();
				msg.AppendLine("Please present this error message to the developer.");
				msg.AppendLine();
				msg.Append("Signature scan failed for ");
				if (this.ComboTimer == IntPtr.Zero)
					msg.Append("ComboTimer");
				else if (this.GetAdjustedActionId == IntPtr.Zero)
					msg.Append("GetAdjustedActionId");
				else if (this.IsActionIdReplaceable == IntPtr.Zero)
					msg.Append("IsActionIdReplaceable");
				else if (this.GetActionCooldown == IntPtr.Zero)
					msg.Append("GetActionCooldown");
				msg.AppendLine(":");
				msg.AppendLine(ex.ToString());
				PluginLog.Fatal(msg.ToString());
				if (die)
					Dalamud.Utility.Util.Fatal(msg.ToString(), "XCVX Load/Init Failure");
			}

			PluginLog.Verbose("===== X C V X =====");
			PluginLog.Verbose($"GetAdjustedActionId 0x{this.GetAdjustedActionIdAddr}");
			PluginLog.Verbose($"IsIconReplaceable   0x{this.IsActionIdReplaceableAddr}");
			PluginLog.Verbose($"ComboTimer          0x{this.ComboTimerAddr}");
			PluginLog.Verbose($"LastComboMove       0x{this.LastComboMoveAddr}");
			PluginLog.Verbose($"GetActionCooldown   0x{this.GetActionCooldownAddr}");
		}
	}
}
