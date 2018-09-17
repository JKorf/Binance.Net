using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Objects
{
    internal class BinanceStream
    {
        internal bool TryReconnect { get; set; } = true;
        internal bool Reconnecting { get; set; } = false;
        public IWebsocket Socket { get; set; }
        public BinanceStreamSubscription StreamResult { get; set; }

        public async Task Close()
        {
            TryReconnect = false;
            await Socket.Close().ConfigureAwait(false);
        }
    }
}
