using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

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
                                             _serviceProvider.GetRequiredService<ILogger<BinanceSpotSymbolOrderBook>>(),
                                             _serviceProvider.GetRequiredService<IBinanceRestClient>(),
                                             _serviceProvider.GetRequiredService<IBinanceSocketClient>());

        
        /// <inheritdoc />
        public ISymbolOrderBook CreateUsdtFutures(string symbol, Action<BinanceOrderBookOptions>? options = null)
            => new BinanceFuturesUsdtSymbolOrderBook(symbol,
                                             options,
                                             _serviceProvider.GetRequiredService<ILogger<BinanceFuturesUsdtSymbolOrderBook>>(),
                                             _serviceProvider.GetRequiredService<IBinanceRestClient>(),
                                             _serviceProvider.GetRequiredService<IBinanceSocketClient>());

        
        /// <inheritdoc />
        public ISymbolOrderBook CreateCoinFutures(string symbol, Action<BinanceOrderBookOptions>? options = null)
            => new BinanceFuturesCoinSymbolOrderBook(symbol,
                                             options,
                                             _serviceProvider.GetRequiredService<ILogger<BinanceFuturesCoinSymbolOrderBook>>(),
                                             _serviceProvider.GetRequiredService<IBinanceRestClient>(),
                                             _serviceProvider.GetRequiredService<IBinanceSocketClient>());
    }
}
