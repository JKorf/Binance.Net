using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using CryptoExchange.Net.Trackers.UserData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

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
        public bool CanCreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval) => true;

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
        public bool CanCreateTradeTracker(SharedSymbol symbol) => true;

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

        public IUserSpotDataTracker CreateUserSpotDataTracker(UserDataTrackerConfig config)
        {
            var restClient = _serviceProvider?.GetRequiredService<IBinanceRestClient>() ?? new BinanceRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IBinanceSocketClient>() ?? new BinanceSocketClient();
            return new BinanceUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BinanceUserSpotDataTracker>>() ?? new NullLogger<BinanceUserSpotDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, UserDataTrackerConfig config, ApiCredentials credentials, BinanceEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IBinanceUserClientProvider>() ?? new BinanceUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new BinanceUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BinanceUserSpotDataTracker>>() ?? new NullLogger<BinanceUserSpotDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }

        public IUserFuturesDataTracker CreateUserUsdFuturesDataTracker(UserDataTrackerConfig config)
        {
            var restClient = _serviceProvider?.GetRequiredService<IBinanceRestClient>() ?? new BinanceRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IBinanceSocketClient>() ?? new BinanceSocketClient();
            return new BinanceUserUsdFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BinanceUserUsdFuturesDataTracker>>() ?? new NullLogger<BinanceUserUsdFuturesDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserUsdFuturesDataTracker(string userIdentifier, UserDataTrackerConfig config, ApiCredentials credentials, BinanceEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IBinanceUserClientProvider>() ?? new BinanceUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new BinanceUserUsdFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BinanceUserUsdFuturesDataTracker>>() ?? new NullLogger<BinanceUserUsdFuturesDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }

        public IUserFuturesDataTracker CreateUserCoinFuturesDataTracker(UserDataTrackerConfig config)
        {
            var restClient = _serviceProvider?.GetRequiredService<IBinanceRestClient>() ?? new BinanceRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IBinanceSocketClient>() ?? new BinanceSocketClient();
            return new BinanceUserCoinFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BinanceUserCoinFuturesDataTracker>>() ?? new NullLogger<BinanceUserCoinFuturesDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserCoinFuturesDataTracker(string userIdentifier, UserDataTrackerConfig config, ApiCredentials credentials, BinanceEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IBinanceUserClientProvider>() ?? new BinanceUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new BinanceUserCoinFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BinanceUserCoinFuturesDataTracker>>() ?? new NullLogger<BinanceUserCoinFuturesDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }
    }
}
