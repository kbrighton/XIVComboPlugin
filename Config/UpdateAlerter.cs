using System;
using System.Threading;
using System.Threading.Tasks;

using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using Dalamud.Logging;

namespace XIVComboVX.Config {
	internal class UpdateAlerter: IDisposable {
		private bool disposed;

		private bool seenUpdateMessage = false;
		private readonly bool hasUpdated = false;
		private readonly Version? from;
		private readonly Version to;

		private CancellationTokenSource? aborter;

		internal UpdateAlerter(Version? from, Version to) {
			this.from = from;
			this.to = to;
			this.hasUpdated = from is null || !from.Equals(to);
			if (this.hasUpdated && Service.Configuration.ShowUpdateMessage) {
				this.register();
				this.checkMessage();
			}
		}

		internal void checkMessage() {
			if (!this.hasUpdated || this.seenUpdateMessage) {
				this.unregister();
				return;
			}
			if (!Service.GameState.isChatVisible || this.disposed)
				return;

			this.aborter?.Cancel();
			this.aborter = new();

			Task.Delay(250, this.aborter.Token).ContinueWith(waiter => {
				if (!waiter.IsCanceled)
					this.displayMessage();
			});
		}

		internal void displayMessage() {
			PluginLog.Information("Displaying update alert in game chat");

			this.aborter?.Cancel();
			this.seenUpdateMessage = true;
			this.unregister();

			string name = Service.Plugin.Name;

			Service.ChatUtils.print(
				XivChatType.Notice,
				new TextPayload(
					this.from is null
						? $"{name} v{this.to} has been installed. By default, all features are disabled.\n["
						: $"{name} has been updated from {this.from} to {this.to}. Features may have been added or changed.\n"
				),
				new UIForegroundPayload(ChatUtil.clfgOpenConfig),
				new UIGlowPayload(ChatUtil.clbgOpenConfig),
				Service.ChatUtils.clplOpenConfig,
				new TextPayload($"[Open {Service.Plugin.Name} Settings]"),
				RawPayload.LinkTerminator,
				new UIGlowPayload(0),
				new UIForegroundPayload(0)
			);

		}

		internal void register() {
			PluginLog.Information("Registering update alerter");
			Service.ChatGui.ChatMessage += this.onChatMessage;
		}

		internal void unregister() {
			PluginLog.Information("Unregistering update alerter");
			Service.ChatGui.ChatMessage -= this.onChatMessage;
		}

		private void onChatMessage(XivChatType type, uint senderId, ref SeString sender, ref SeString message, ref bool isHandled) {
			if (type is XivChatType.Notice)
				this.checkMessage();
		}

		#region Disposing

		public void Dispose() {
			if (this.disposed)
				return;
			this.disposed = true;

			this.unregister();
		}

		#endregion

	}
}
