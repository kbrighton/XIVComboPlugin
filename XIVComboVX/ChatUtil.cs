namespace PrincessRTFM.XIVComboVX;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;

internal class ChatUtil: IDisposable {
	private bool disposed;

	private const uint
		openConfigId = 0,
		openIssueTrackerId = 1;

	internal readonly DalamudLinkPayload
		openConfig,
		openIssueTracker;

	internal const ushort
		colourForeWarning = 32,
		colourForeError = 17,
		colourForeOpenConfig = 34,
		colourGlowOpenConfig = 37;

	internal ChatUtil() {
		this.openConfig = Service.Interface.AddChatLinkHandler(openConfigId, this.onClickChatLink);
		this.openIssueTracker = Service.Interface.AddChatLinkHandler(openIssueTrackerId, this.onClickChatLink);
	}

	internal void addOpenConfigLink(SeStringBuilder sb, string label) {
		sb.AddUiForeground(colourForeOpenConfig);
		sb.AddUiGlow(colourGlowOpenConfig);
		sb.Add(this.openConfig);
		sb.AddText(label);
		sb.Add(RawPayload.LinkTerminator);
		sb.AddUiGlowOff();
		sb.AddUiForegroundOff();
	}
	internal void addOpenIssueTrackerLink(SeStringBuilder sb, string label) {
		sb.AddUiForeground(colourForeOpenConfig);
		sb.AddUiGlow(colourGlowOpenConfig);
		sb.Add(this.openIssueTracker);
		sb.AddText(label);
		sb.Add(RawPayload.LinkTerminator);
		sb.AddUiGlowOff();
		sb.AddUiForegroundOff();
	}

	[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Convention")]
	internal void print(XivChatType type, params Payload[] payloads) {
		if (payloads.Length > 0) {
			Service.ChatGui.Print(new XivChatEntry() {
				Type = type,
				Message = new SeString(payloads),
			});
		}
	}

	private void onClickChatLink(uint id, SeString source) {
		switch (id) {
			case openConfigId:
				Service.Plugin.onPluginCommand("", "");
				break;
			case openIssueTrackerId:
				Process.Start(new ProcessStartInfo("https://github.com/PrincessRTFM/XIVComboPlugin/issues") { UseShellExecute = true });
				break;
			default:
				Service.ChatGui.Print(new XivChatEntry() {
					Type = XivChatType.SystemError,
					Message = new SeString(
						new TextPayload($"An internal error has occured: no handler is registered for id {id}.")
					),
				});
				break;
		}
	}

	#region Disposable

	public void Dispose() {
		if (this.disposed)
			return;
		this.disposed = true;

		Service.Interface.RemoveChatLinkHandler();
	}

	#endregion

}
