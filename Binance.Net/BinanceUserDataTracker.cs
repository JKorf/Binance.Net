using Binance.Net.Interfaces.Clients;
using CryptoExchange.Net.Trackers.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net
{

    public class BinanceUserSpotDataTracker : UserSpotDataTracker
    {
        public BinanceUserSpotDataTracker(
            ILogger<BinanceUserSpotDataTracker> logger,
            IBinanceRestClient restClient,
            IBinanceSocketClient socketClient,
            string? userIdentifier,
            UserDataTrackerConfig config) : base(logger, restClient.SpotApi.SharedClient, socketClient.SpotApi.SharedClient, userIdentifier, config)
        {

        }
    }

    public class BinanceUserUsdFuturesDataTracker : UserFuturesDataTracker
    {
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        public BinanceUserUsdFuturesDataTracker(
            ILogger<BinanceUserUsdFuturesDataTracker> logger,
            IBinanceRestClient restClient,
            IBinanceSocketClient socketClient,
            string? userIdentifier,
            UserDataTrackerConfig config) : base(logger, restClient.UsdFuturesApi.SharedClient, socketClient.UsdFuturesApi.SharedClient, userIdentifier, config)
        {

        }
    }

    public class BinanceUserCoinFuturesDataTracker : UserFuturesDataTracker
    {
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        public BinanceUserCoinFuturesDataTracker(
            ILogger<BinanceUserCoinFuturesDataTracker> logger,
            IBinanceRestClient restClient,
            IBinanceSocketClient socketClient,
            string? userIdentifier,
            UserDataTrackerConfig config) : base(logger, restClient.CoinFuturesApi.SharedClient, socketClient.CoinFuturesApi.SharedClient, userIdentifier, config)
        {

        }
    }
}
