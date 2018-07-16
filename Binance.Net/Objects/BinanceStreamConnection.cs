using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Objects
{
    internal class BinanceStream
    {
        internal bool TryReconnect { get; set; } = true;
        public IWebsocket Socket { get; set; }
        public BinanceStreamSubscription StreamResult { get; set; }

        public async Task Close()
        {
            TryReconnect = false;
            await Socket.Close().ConfigureAwait(false);
        }
    }
}
