using System;
using System.Collections.Generic;
using System.Net.Http;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Interfaces.Clients.Rest.CoinFutures;
using Binance.Net.Interfaces.Clients.Rest.Spot;
using Binance.Net.Interfaces.Clients.Rest.UsdFutures;
using Binance.Net.Interfaces.Clients.Socket;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Objects
{
    public class BinanceSubClientOptions: RestSubClientOptions
    {
        /// <summary>
        /// A manual offset for the timestamp. Should only be used if AutoTimestamp and regular time synchronization on the OS is not reliable enough
        /// </summary>
        public TimeSpan TimestampOffset { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// Whether to check the trade rules when placing new orders and what to do if the trade isn't valid
        /// </summary>
        public TradeRulesBehaviour TradeRulesBehaviour { get; set; } = TradeRulesBehaviour.None;
        /// <summary>
        /// How often the trade rules should be updated. Only used when TradeRulesBehaviour is not None
        /// </summary>
        public TimeSpan TradeRulesUpdateInterval { get; set; } = TimeSpan.FromMinutes(60);

        /// <summary>
        /// Copy the values of the def to the input
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="def"></param>
        public new void Copy<T>(T input, T def) where T : BinanceSubClientOptions
        {
            base.Copy(input, def);

            input.TimestampOffset = def.TimestampOffset;
            input.TradeRulesBehaviour = def.TradeRulesBehaviour;
            input.TradeRulesUpdateInterval = def.TradeRulesUpdateInterval;
        }
    }

    public class BinanceClientOptions: RestClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static BinanceClientOptions Default { get; set; } = new BinanceClientOptions()
        {
            OptionsSpot = new BinanceSubClientOptions
            {
                BaseAddress = BinanceApiAddresses.Default.RestClientAddress,
                RateLimiters = new List<IRateLimiter>
                {
                    new RateLimiter()
                        .AddPartialEndpointLimit("/api/", 1200, TimeSpan.FromMinutes(1))
                        .AddPartialEndpointLimit("/sapi/", 12000, TimeSpan.FromMinutes(1))
                        .AddEndpointLimit("/api/v3/order", 50, TimeSpan.FromSeconds(10), HttpMethod.Post, true)
                }
            },
            OptionsUsdFutures = new BinanceSubClientOptions
            {
                BaseAddress = BinanceApiAddresses.Default.UsdFuturesRestClientAddress
            },
            OptionsCoinFutures = new BinanceSubClientOptions
            {
                BaseAddress = BinanceApiAddresses.Default.CoinFuturesRestClientAddress
            }            
        };

        /// <summary>
        /// Whether or not to automatically sync the local time with the server time
        /// </summary>
        public bool AutoTimestamp { get; set; } = true;

        /// <summary>
        /// Interval for refreshing the auto timestamp calculation
        /// </summary>
        public TimeSpan AutoTimestampRecalculationInterval { get; set; } = TimeSpan.FromHours(3);

        /// <summary>
        /// The default receive window for requests
        /// </summary>
        public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

        public BinanceSubClientOptions OptionsSpot { get; set; }
        public BinanceSubClientOptions OptionsUsdFutures { get; set; }
        public BinanceSubClientOptions OptionsCoinFutures { get; set; }

        public BinanceClientOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }

        /// <summary>
        /// Copy the values of the def to the input
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="def"></param>
        public new void Copy<T>(T input, T def) where T : BinanceClientOptions
        {
            base.Copy(input, def);

            input.AutoTimestamp = def.AutoTimestamp;
            input.AutoTimestampRecalculationInterval = def.AutoTimestampRecalculationInterval;
            input.ReceiveWindow = def.ReceiveWindow;

            input.OptionsSpot = new BinanceSubClientOptions();
            def.OptionsSpot.Copy(input.OptionsSpot, def.OptionsSpot);

            input.OptionsUsdFutures = new BinanceSubClientOptions();
            def.OptionsUsdFutures.Copy(input.OptionsUsdFutures, def.OptionsUsdFutures);

            input.OptionsCoinFutures = new BinanceSubClientOptions();
            def.OptionsCoinFutures.Copy(input.OptionsCoinFutures, def.OptionsCoinFutures);
        }
    }

    /// <summary>
    /// Binance socket client options
    /// </summary>
    public class BinanceSocketClientOptions : SocketClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static BinanceSocketClientOptions Default { get; set; } = new BinanceSocketClientOptions()
        {
            SocketSubscriptionsCombineTarget = 10,
            OptionsSpot = new SubClientOptions
            {
                BaseAddress = BinanceApiAddresses.Default.SocketClientAddress
            },
            OptionsUsdFutures = new SubClientOptions
            {
                BaseAddress = BinanceApiAddresses.Default.UsdFuturesSocketClientAddress!
            },
            OptionsCoinFutures = new SubClientOptions
            {
                BaseAddress = BinanceApiAddresses.Default.CoinFuturesSocketClientAddress!
            }
        };

        public SubClientOptions OptionsSpot { get; set; }
        public SubClientOptions OptionsUsdFutures { get; set; }
        public SubClientOptions OptionsCoinFutures { get; set; }

        public BinanceSocketClientOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }

        public new void Copy<T>(T input, T def) where T : BinanceSocketClientOptions
        {
            if (Default == null)
                return;

            input.OptionsSpot = new SubClientOptions();
            def.OptionsSpot.Copy(input.OptionsSpot, def.OptionsSpot);

            input.OptionsUsdFutures = new SubClientOptions();
            def.OptionsUsdFutures.Copy(input.OptionsUsdFutures, def.OptionsUsdFutures);

            input.OptionsCoinFutures = new SubClientOptions();
            def.OptionsCoinFutures.Copy(input.OptionsCoinFutures, def.OptionsCoinFutures);
        }
    }

    /// <summary>
    /// Binance symbol order book options
    /// </summary>
    public class BinanceOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// The top amount of results to keep in sync. If for example limit=10 is used, the order book will contain the 10 best bids and 10 best asks. Leaving this null will sync the full order book
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Update interval in milliseconds, either 100 or 1000. Defaults to 1000
        /// </summary>
        public int? UpdateInterval { get; set; }


        /// <summary>
        /// The rest client to use for requesting the initial order book
        /// </summary>
        public IBinanceClient? RestClient { get; set; }

        /// <summary>
        /// The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.
        /// </summary>
        public IBinanceSocketClient? SocketClient { get; set; }

        /// <summary>
        /// Create new options
        /// </summary>
        /// <param name="limit">The top amount of results to keep in sync. If for example limit=10 is used, the order book will contain the 10 best bids and 10 best asks. Leaving this null will sync the full order book</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000. Defaults to 1000</param>
        public BinanceOrderBookOptions(int? limit = null, int? updateInterval = null, IBinanceSocketClient? socketClient = null, IBinanceClient? restClient = null)
        {
            Limit = limit;
            UpdateInterval = updateInterval;
            RestClient = restClient;
            SocketClient = socketClient;
        }
    }
}
