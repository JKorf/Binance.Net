using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Options;
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
        }

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
