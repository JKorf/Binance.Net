using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.OrderBook;
using Microsoft.Extensions.DependencyInjection;

namespace Binance.Net.SymbolOrderBooks
{
    /// <summary>
    /// Binance order book factory
    /// </summary>
    public class BinanceOrderBookFactory : IBinanceOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public BinanceOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Spot = new OrderBookFactory<BinanceOrderBookOptions>(
                CreateSpot,
                (sharedSymbol, options) => CreateSpot(BinanceExchange.FormatSymbol(sharedSymbol.BaseAsset, sharedSymbol.QuoteAsset, sharedSymbol.TradingMode, sharedSymbol.DeliverTime), options));
            UsdFutures = new OrderBookFactory<BinanceOrderBookOptions>(
                CreateUsdtFutures,
                (sharedSymbol, options) => CreateUsdtFutures(BinanceExchange.FormatSymbol(sharedSymbol.BaseAsset, sharedSymbol.QuoteAsset, sharedSymbol.TradingMode, sharedSymbol.DeliverTime), options));
            CoinFutures = new OrderBookFactory<BinanceOrderBookOptions>(
                CreateCoinFutures,
                (sharedSymbol, options) => CreateCoinFutures(BinanceExchange.FormatSymbol(sharedSymbol.BaseAsset, sharedSymbol.QuoteAsset, sharedSymbol.TradingMode, sharedSymbol.DeliverTime), options));
        }

        /// <inheritdoc />
        public IOrderBookFactory<BinanceOrderBookOptions> Spot { get; }
        /// <inheritdoc />
        public IOrderBookFactory<BinanceOrderBookOptions> UsdFutures { get; }
        /// <inheritdoc />
        public IOrderBookFactory<BinanceOrderBookOptions> CoinFutures { get; }

        /// <inheritdoc />
        public ISymbolOrderBook CreateSpot(string symbol, Action<BinanceOrderBookOptions>? options = null)
            => new BinanceSpotSymbolOrderBook(symbol,
                                             options,
                                             _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                             _serviceProvider.GetRequiredService<IBinanceRestClient>(),
                                             _serviceProvider.GetRequiredService<IBinanceSocketClient>());

        
        /// <inheritdoc />
        public ISymbolOrderBook CreateUsdtFutures(string symbol, Action<BinanceOrderBookOptions>? options = null)
            => new BinanceFuturesUsdtSymbolOrderBook(symbol,
                                             options,
                                             _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                             _serviceProvider.GetRequiredService<IBinanceRestClient>(),
                                             _serviceProvider.GetRequiredService<IBinanceSocketClient>());

        
        /// <inheritdoc />
        public ISymbolOrderBook CreateCoinFutures(string symbol, Action<BinanceOrderBookOptions>? options = null)
            => new BinanceFuturesCoinSymbolOrderBook(symbol,
                                             options,
                                             _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                             _serviceProvider.GetRequiredService<IBinanceRestClient>(),
                                             _serviceProvider.GetRequiredService<IBinanceSocketClient>());
    }
}
