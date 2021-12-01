using Binance.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Clients.Base
{
    public class BinanceClientBaseSpot : RestApiClient
    {
        #region fields 
        internal static double CalculatedTimeOffset;
        internal static bool TimeSynced;
        internal static DateTime LastTimeSync;
        #endregion

        public BinanceClientBaseSpot(BinanceClientOptions options) :
            base(options, options.SpotApiOptions)
        {
        }

        public override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BinanceAuthenticationProvider(credentials);
    }
}
