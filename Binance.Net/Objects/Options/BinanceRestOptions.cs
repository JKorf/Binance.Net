using Binance.Net.Clients.SpotApi;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Options;
using CryptoExchange.Net.RateLimiting;
using CryptoExchange.Net.RateLimiting.Guards;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Binance.Net.Objects.Options
{
    /// <summary>
    /// Options for the BinanceRestClient
    /// </summary>
    public class BinanceRestOptions : RestExchangeOptions<BinanceEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        public static BinanceRestOptions Default { get; set; } = new BinanceRestOptions()
        {
            Environment = BinanceEnvironment.Live,
            AutoTimestamp = true
        };

        /// <summary>
        /// The default receive window for requests
        /// </summary>
        public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Spot API options
        /// </summary>
        public BinanceRestApiOptions SpotOptions { get; private set; } = new BinanceRestApiOptions
        {
            RateLimiter = BinanceRateLimiters.SpotApi
        };

        /// <summary>
        /// Usd futures API options
        /// </summary>
        public BinanceRestApiOptions UsdFuturesOptions { get; private set; } = new BinanceRestApiOptions();

        /// <summary>
        /// Coin futures API options
        /// </summary>
        public BinanceRestApiOptions CoinFuturesOptions { get; private set; } = new BinanceRestApiOptions();

        internal BinanceRestOptions Copy()
        {
            var options = Copy<BinanceRestOptions>();
            options.ReceiveWindow = ReceiveWindow;
            options.SpotOptions = SpotOptions.Copy();
            options.UsdFuturesOptions = UsdFuturesOptions.Copy();
            options.CoinFuturesOptions = CoinFuturesOptions.Copy();
            return options;
        }
    }
}
