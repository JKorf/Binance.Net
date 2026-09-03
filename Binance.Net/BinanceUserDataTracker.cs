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
            SpotUserDataTrackerConfig? config = null) : base(
                logger,
                restClient.SpotApi.SharedApi,
                restClient.SpotApi.SharedApi,
                socketClient.SpotApi.SharedApi,

                restClient.SpotApi.SharedApi,
                restClient.SpotApi.SharedApi,
                socketClient.SpotApi.SharedApi,

                restClient.SpotApi.SharedApi,
                null,
                userIdentifier, 
                config ?? new SpotUserDataTrackerConfig())
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
            FuturesUserDataTrackerConfig? config = null) : 
            base(logger,
                restClient.UsdFuturesApi.SharedApi,
                restClient.UsdFuturesApi.SharedApi,
                socketClient.UsdFuturesApi.SharedApi,

                restClient.UsdFuturesApi.SharedApi,
                restClient.UsdFuturesApi.SharedApi,
                socketClient.UsdFuturesApi.SharedApi,

                restClient.UsdFuturesApi.SharedApi,
                null,

                restClient.UsdFuturesApi.SharedApi,
                socketClient.UsdFuturesApi.SharedApi,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig())
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
            FuturesUserDataTrackerConfig? config = null) : base(logger,
                restClient.UsdFuturesApi.SharedApi,
                restClient.UsdFuturesApi.SharedApi,
                socketClient.UsdFuturesApi.SharedApi,

                restClient.UsdFuturesApi.SharedApi,
                restClient.UsdFuturesApi.SharedApi,
                socketClient.UsdFuturesApi.SharedApi,

                restClient.UsdFuturesApi.SharedApi,
                null,

                restClient.UsdFuturesApi.SharedApi,
                socketClient.UsdFuturesApi.SharedApi,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig())
        {

        }
    }
}
