using System;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Objects
{
    internal class BinanceStream
    {
        private Action<string> onMessage;
        private IWebsocket socket;

        internal bool TryReconnect { get; set; } = false;
        internal bool Reconnecting { get; set; } = false;
        public IWebsocket Socket
        {
            get => socket;
            set
            {
                socket = value;
                if (onMessage != null)
                    socket.OnMessage += onMessage;
            }
        }
        public Action<string> OnMessage
        {
            get => onMessage;
            set {
                onMessage = value;
                Socket.OnMessage += value;
            }
        }
        public BinanceStreamSubscription StreamResult { get; set; }

        public async Task Close()
        {
            TryReconnect = false;
            await Socket.Close().ConfigureAwait(false);
        }
    }
}
