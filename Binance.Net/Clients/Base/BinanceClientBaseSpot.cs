using Binance.Net.Objects;
using CryptoExchange.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Clients.Base
{
    public class BinanceClientBaseSpot : RestSubClient
    {
        #region fields 
        internal static double CalculatedTimeOffset;
        internal static bool TimeSynced;
        internal static DateTime LastTimeSync;
        #endregion

        public BinanceClientBaseSpot(BinanceClientOptions options) :
            base(options.OptionsSpot, options.OptionsSpot.ApiCredentials == null ? null : new BinanceAuthenticationProvider(options.OptionsSpot.ApiCredentials))
        {
        }

    }
}
