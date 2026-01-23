using Binance.Net.Interfaces.Clients;
using CryptoExchange.Net.Trackers.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net
{
    public class BinanceUserDataTracker : UserDataTracker
    {
        public BinanceUserDataTracker(
            ILogger<BinanceUserDataTracker> logger,
            IBinanceRestClient restClient,
            IBinanceSocketClient socketClient,
            string? userIdentifier,
            UserDataTrackerConfig config) : base(logger, restClient.SpotApi.SharedClient, socketClient.SpotApi.SharedClient, userIdentifier, config)
        {

        }
    }
}
