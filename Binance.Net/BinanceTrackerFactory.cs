using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using Microsoft.Extensions.DependencyInjection;

namespace Binance.Net
{
    /// <inheritdoc />
    public class BinanceTrackerFactory : IBinanceTrackerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public BinanceTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public IKlineTracker CreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval, int? limit = null, TimeSpan? period = null)
        {
            IKlineRestClient restClient;
            IKlineSocketClient socketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                restClient = _serviceProvider.GetRequiredService<IBinanceRestClient>().SpotApi.SharedClient;
                socketClient = _serviceProvider.GetRequiredService<IBinanceSocketClient>().SpotApi.SharedClient;
            }
            else if (symbol.TradingMode.IsLinear())
            {
                restClient = _serviceProvider.GetRequiredService<IBinanceRestClient>().UsdFuturesApi.SharedClient;
                socketClient = _serviceProvider.GetRequiredService<IBinanceSocketClient>().UsdFuturesApi.SharedClient;
            }
            else
            {
                restClient = _serviceProvider.GetRequiredService<IBinanceRestClient>().CoinFuturesApi.SharedClient;
                socketClient = _serviceProvider.GetRequiredService<IBinanceSocketClient>().CoinFuturesApi.SharedClient;
            }

            return new KlineTracker(
                _serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                restClient,
                socketClient,
                symbol,
                interval,
                limit,
                period
                );
        }

        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null)
        {
            ITradeHistoryRestClient restClient;
            ITradeSocketClient socketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                restClient = _serviceProvider.GetRequiredService<IBinanceRestClient>().SpotApi.SharedClient;
                socketClient = _serviceProvider.GetRequiredService<IBinanceSocketClient>().SpotApi.SharedClient;
            }
            else if (symbol.TradingMode.IsLinear())
            {
                restClient = _serviceProvider.GetRequiredService<IBinanceRestClient>().UsdFuturesApi.SharedClient;
                socketClient = _serviceProvider.GetRequiredService<IBinanceSocketClient>().UsdFuturesApi.SharedClient;
            }
            else
            {
                restClient = _serviceProvider.GetRequiredService<IBinanceRestClient>().CoinFuturesApi.SharedClient;
                socketClient = _serviceProvider.GetRequiredService<IBinanceSocketClient>().CoinFuturesApi.SharedClient;
            }

            return new TradeTracker(
                _serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                null,
                restClient,
                socketClient,
                symbol,
                limit,
                period
                );
        }
    }
}
