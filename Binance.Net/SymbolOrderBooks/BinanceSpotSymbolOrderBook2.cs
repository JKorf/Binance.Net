using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Options;

namespace Binance.Net.SymbolOrderBooks
{
    /// <summary>
    /// Implementation for a synchronized order book. After calling Start the order book will sync itself and keep up to date with new data. It will automatically try to reconnect and resync in case of a lost/interrupted connection.
    /// Make sure to check the State property to see if the order book is synced.
    /// </summary>
    public class BinanceSpotSymbolOrderBook2 : CryptoExchange.Net.OrderBook.V2.ClientOrderBook
    {
        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BinanceSpotSymbolOrderBook2(string baseAsset, string quoteAsset, Action<BinanceOrderBookOptions>? optionsDelegate = null)
            : this(
                  new SharedSymbol(TradingMode.Spot, baseAsset, quoteAsset),
                  optionsDelegate,
                  null,
                  new BinanceSharedApiClient(
                      new BinanceRestClient(),
                      new BinanceSocketClient(),
                      Options.Create(new BinanceOptions())
                      ))
        {
        }

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="logger">Logger</param>
        /// <param name="restClient">Rest client instance</param>
        /// <param name="socketClient">Socket client instance</param>
        public BinanceSpotSymbolOrderBook2(
            SharedSymbol symbol,
            Action<BinanceOrderBookOptions>? optionsDelegate,
            ILoggerFactory? logger,
            IBinanceSharedApiClient client) : base(
                logger, 
                symbol, 
                incrementalApi: client.SpotSocket,
                restSnapshotApi: client.SpotRest)
        {
            var options = BinanceOrderBookOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            _strictLevels = false;
        }
    }
}
