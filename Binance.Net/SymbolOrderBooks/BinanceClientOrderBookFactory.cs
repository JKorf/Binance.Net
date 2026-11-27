using Binance.Net.Interfaces.Clients;
using CryptoExchange.Net.OrderBook.V2;
using CryptoExchange.Net.OrderBook.V2.Interfaces;
using CryptoExchange.Net.SharedApis;

namespace Binance.Net.SymbolOrderBooks
{
    /// <summary>
    /// Client order book factory
    /// </summary>
    public class BinanceClientOrderBookFactory : ClientOrderBookFactory, IClientOrderBookFactory
    {
        /// <summary>
        /// ctor
        /// </summary>
        public BinanceClientOrderBookFactory(ILoggerFactory logger, IBinanceSharedApiClient api) : base(logger, api)
        {
        }

        /// <inheritdoc />
        public IClientOrderBook Create(SharedSymbol symbol, ExchangeParameters? exchangeParameters = null)
        {
            var config = new BookConfig()
            {
                SkipSequenceCheckFirstUpdateAfterSnapshotSet = true,
                MinFirstBufferedUpdateWait = TimeSpan.FromMilliseconds(100),
                MaxFirstBufferedUpdateWait = TimeSpan.FromMilliseconds(250),
            };
            return Create(symbol, config, exchangeParameters);
        }
    }
}
