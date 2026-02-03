using Binance.Net.Interfaces.Clients;
using CryptoExchange.Net.Trackers.UserData;
using CryptoExchange.Net.Trackers.UserData.Objects;

namespace Binance.Net
{
    /// <inheritdoc />
    public class BinanceUserSpotDataTracker : UserSpotDataTracker
    {
        /// <summary>
        /// ctor
        /// </summary>
        public BinanceUserSpotDataTracker(
            ILogger<BinanceUserSpotDataTracker> logger,
            IBinanceRestClient restClient,
            IBinanceSocketClient socketClient,
            string? userIdentifier,
            SpotUserDataTrackerConfig config) : base(
                logger,
                restClient.SpotApi.SharedClient,
                restClient.SpotApi.SharedClient,
                restClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
                restClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
                null,
                userIdentifier, 
                config)
        {

        }
    }

    /// <inheritdoc />
    public class BinanceUserUsdFuturesDataTracker : UserFuturesDataTracker
    {
        /// <inheritdoc />
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceUserUsdFuturesDataTracker(
            ILogger<BinanceUserUsdFuturesDataTracker> logger,
            IBinanceRestClient restClient,
            IBinanceSocketClient socketClient,
            string? userIdentifier,
            FuturesUserDataTrackerConfig config) : 
            base(logger,
                restClient.UsdFuturesApi.SharedClient,
                restClient.UsdFuturesApi.SharedClient,
                restClient.UsdFuturesApi.SharedClient,
                socketClient.UsdFuturesApi.SharedClient,
                restClient.UsdFuturesApi.SharedClient,
                socketClient.UsdFuturesApi.SharedClient,
                null,
                socketClient.UsdFuturesApi.SharedClient,
                userIdentifier,
                config)
        {

        }
    }

    /// <inheritdoc />
    public class BinanceUserCoinFuturesDataTracker : UserFuturesDataTracker
    {
        /// <inheritdoc />
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceUserCoinFuturesDataTracker(
            ILogger<BinanceUserCoinFuturesDataTracker> logger,
            IBinanceRestClient restClient,
            IBinanceSocketClient socketClient,
            string? userIdentifier,
            FuturesUserDataTrackerConfig config) : base(logger,
                restClient.CoinFuturesApi.SharedClient,
                restClient.CoinFuturesApi.SharedClient,
                restClient.CoinFuturesApi.SharedClient,
                socketClient.CoinFuturesApi.SharedClient,
                restClient.CoinFuturesApi.SharedClient,
                socketClient.CoinFuturesApi.SharedClient,
                null,
                socketClient.CoinFuturesApi.SharedClient,
                userIdentifier,
                config)
        {

        }
    }
}
