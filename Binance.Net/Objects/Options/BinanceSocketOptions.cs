using CryptoExchange.Net.Objects.Options;
using System.Collections.Generic;
using System;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting;
using CryptoExchange.Net.RateLimiting.Guards;

namespace Binance.Net.Objects.Options
{
    /// <summary>
    /// Options for the BinanceSocketClient
    /// </summary>
    public class BinanceSocketOptions : SocketExchangeOptions<BinanceEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        public static BinanceSocketOptions Default { get; set; } = new BinanceSocketOptions()
        {
            Environment = BinanceEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// Options for the Spot API
        /// </summary>
        public BinanceSocketApiOptions SpotOptions { get; private set; } = new BinanceSocketApiOptions()
        {
            //RateLimiter = new RateLimitGate()
            //    .AddGuard(new HostLimitGuard("stream.binance.com", 5, TimeSpan.FromMinutes(1)))
            //    .AddGuard(new HostLimitGuard("ws-api.binance.com", 10, TimeSpan.FromMinutes(1)))
            //    .WithLimitBehaviour(RateLimitingBehaviour.Wait)
            //{
            //    new RateLimiter()
            //        .AddConnectionRateLimit("stream.binance.com", 5, TimeSpan.FromSeconds(1))
            //        .AddConnectionRateLimit("ws-api.binance.com", 6000, TimeSpan.FromSeconds(60))
            //}
        };

        /// <summary>
        /// Options for the Usd Futures API
        /// </summary>
        public BinanceSocketApiOptions UsdFuturesOptions { get; private set; } = new BinanceSocketApiOptions();

        /// <summary>
        /// Options for the Coin Futures API
        /// </summary>
        public BinanceSocketApiOptions CoinFuturesOptions { get; private set; } = new BinanceSocketApiOptions(); 

        internal BinanceSocketOptions Copy()
        {
            var options = Copy<BinanceSocketOptions>();
            options.SpotOptions = SpotOptions.Copy();
            options.UsdFuturesOptions = UsdFuturesOptions.Copy();
            options.CoinFuturesOptions = CoinFuturesOptions.Copy();
            return options;
        }
    }
}
