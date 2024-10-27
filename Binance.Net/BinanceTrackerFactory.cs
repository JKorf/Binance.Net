using Binance.Net.Clients;
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
        private readonly IServiceProvider? _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceTrackerFactory()
        {
        }

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
            var restClient = _serviceProvider?.GetRequiredService<IBinanceRestClient>() ?? new BinanceRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IBinanceSocketClient>() ?? new BinanceSocketClient();

            IKlineRestClient sharedRestClient;
            IKlineSocketClient sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = restClient.SpotApi.SharedClient;
                sharedSocketClient = socketClient.SpotApi.SharedClient;
            }
            else if (symbol.TradingMode.IsLinear())
            {
                sharedRestClient = restClient.UsdFuturesApi.SharedClient;
                sharedSocketClient = socketClient.UsdFuturesApi.SharedClient;
            }
            else
            {
                sharedRestClient = restClient.CoinFuturesApi.SharedClient;
                sharedSocketClient = socketClient.CoinFuturesApi.SharedClient;
            }

            return new KlineTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                sharedRestClient,
                sharedSocketClient,
                symbol,
                interval,
                limit,
                period
                );
        }

        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IBinanceRestClient>() ?? new BinanceRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IBinanceSocketClient>() ?? new BinanceSocketClient();

            ITradeHistoryRestClient sharedRestClient;
            ITradeSocketClient sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = restClient.SpotApi.SharedClient;
                sharedSocketClient = socketClient.SpotApi.SharedClient;
            }
            else if (symbol.TradingMode.IsLinear())
            {
                sharedRestClient = restClient.UsdFuturesApi.SharedClient;
                sharedSocketClient = socketClient.UsdFuturesApi.SharedClient;
            }
            else
            {
                sharedRestClient = restClient.CoinFuturesApi.SharedClient;
                sharedSocketClient = socketClient.CoinFuturesApi.SharedClient;
            }

            return new TradeTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                null,
                sharedRestClient,
                sharedSocketClient,
                symbol,
                limit,
                period
                );
        }
    }
}
