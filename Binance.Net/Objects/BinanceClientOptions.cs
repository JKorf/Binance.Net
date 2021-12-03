using System;
using System.Collections.Generic;
using System.Net.Http;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Options for the Binance client
    /// </summary>
    public class BinanceClientOptions: BaseRestClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static BinanceClientOptions Default { get; set; } = new BinanceClientOptions();

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

        private BinanceApiClientOptions _spotApiOptions = new BinanceApiClientOptions(BinanceApiAddresses.Default.RestClientAddress)
        {
            RateLimiters = new List<IRateLimiter>
                {
                    new RateLimiter()
                        .AddPartialEndpointLimit("/api/", 1200, TimeSpan.FromMinutes(1))
                        .AddPartialEndpointLimit("/sapi/", 12000, TimeSpan.FromMinutes(1))
                        .AddEndpointLimit("/api/v3/order", 50, TimeSpan.FromSeconds(10), HttpMethod.Post, true)
                }
        };
        /// <summary>
        /// Spot API options
        /// </summary>
        public BinanceApiClientOptions SpotApiOptions
        {
            get => _spotApiOptions;
            set => _spotApiOptions.Copy(_spotApiOptions, value);
        }

        private BinanceApiClientOptions _usdFuturesApiOptions = new BinanceApiClientOptions(BinanceApiAddresses.Default.UsdFuturesRestClientAddress!);
        /// <summary>
        /// Usd futures API options
        /// </summary>
        public BinanceApiClientOptions UsdFuturesApiOptions
        {
            get => _usdFuturesApiOptions;
            set => _usdFuturesApiOptions.Copy(_usdFuturesApiOptions, value);
        }
        
        private BinanceApiClientOptions _coinFuturesApiOptions = new BinanceApiClientOptions(BinanceApiAddresses.Default.CoinFuturesRestClientAddress!);
        /// <summary>
        /// Coin futures API options
        /// </summary>
        public BinanceApiClientOptions CoinFuturesApiOptions
        {
            get => _coinFuturesApiOptions;
            set => _coinFuturesApiOptions.Copy(_coinFuturesApiOptions, value);
        }

        /// <summary>
        /// ctor
        /// </summary>
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

            input.SpotApiOptions = new BinanceApiClientOptions(def.SpotApiOptions);
            input.UsdFuturesApiOptions = new BinanceApiClientOptions(def.UsdFuturesApiOptions);
            input.CoinFuturesApiOptions = new BinanceApiClientOptions(def.CoinFuturesApiOptions);
        }
    }

    /// <summary>
    /// Binance socket client options
    /// </summary>
    public class BinanceSocketClientOptions : BaseSocketClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static BinanceSocketClientOptions Default { get; set; } = new BinanceSocketClientOptions()
        {
            SocketSubscriptionsCombineTarget = 10
        };

        private ApiClientOptions _spotStreamsOptions = new ApiClientOptions(BinanceApiAddresses.Default.SocketClientAddress);
        /// <summary>
        /// Spot streams options
        /// </summary>
        public ApiClientOptions SpotStreamsOptions
        {
            get => _spotStreamsOptions;
            set => _spotStreamsOptions.Copy(_spotStreamsOptions, value);
        }

        private ApiClientOptions _usdFuturestStreamsOptions = new ApiClientOptions(BinanceApiAddresses.Default.UsdFuturesSocketClientAddress!);
        /// <summary>
        /// Usd futures streams options
        /// </summary>
        public ApiClientOptions UsdFuturesStreamsOptions
        {
            get => _usdFuturestStreamsOptions;
            set => _usdFuturestStreamsOptions.Copy(_usdFuturestStreamsOptions, value);
        }

        private ApiClientOptions _coinFuturesStreamsOptions = new ApiClientOptions(BinanceApiAddresses.Default.CoinFuturesSocketClientAddress!);
        /// <summary>
        /// Coin futures streams options
        /// </summary>
        public ApiClientOptions CoinFuturesStreamsOptions
        {
            get => _coinFuturesStreamsOptions;
            set => _coinFuturesStreamsOptions.Copy(_coinFuturesStreamsOptions, value);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceSocketClientOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }

        /// <inheritdoc />
        public new void Copy<T>(T input, T def) where T : BinanceSocketClientOptions
        {
            if (Default == null)
                return;

            input.SpotStreamsOptions = new ApiClientOptions(def.SpotStreamsOptions);
            input.UsdFuturesStreamsOptions = new ApiClientOptions(def.UsdFuturesStreamsOptions);
            input.CoinFuturesStreamsOptions = new ApiClientOptions(def.CoinFuturesStreamsOptions);
        }
    }

    /// <summary>
    /// Binance API client options
    /// </summary>
    public class BinanceApiClientOptions : RestApiClientOptions
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
        /// ctor
        /// </summary>
        public BinanceApiClientOptions() 
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseAddress"></param>
        public BinanceApiClientOptions(string baseAddress): base(baseAddress)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn"></param>
        public BinanceApiClientOptions(BinanceApiClientOptions baseOn): base(baseOn)
        {
            TimestampOffset = baseOn.TimestampOffset;
            TradeRulesBehaviour = baseOn.TradeRulesBehaviour;
            TradeRulesUpdateInterval = baseOn.TradeRulesUpdateInterval;
        }

        /// <summary>
        /// Copy the values of the def to the input
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="def"></param>
        public new void Copy<T>(T input, T def) where T : BinanceApiClientOptions
        {
            base.Copy(input, def);

            input.TimestampOffset = def.TimestampOffset;
            input.TradeRulesBehaviour = def.TradeRulesBehaviour;
            input.TradeRulesUpdateInterval = def.TradeRulesUpdateInterval;
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
        /// <param name="socketClient">The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.</param>
        /// <param name="restClient">The rest client to use for requesting the initial order book</param>
        public BinanceOrderBookOptions(int? limit = null, int? updateInterval = null, IBinanceSocketClient? socketClient = null, IBinanceClient? restClient = null)
        {
            Limit = limit;
            UpdateInterval = updateInterval;
            RestClient = restClient;
            SocketClient = socketClient;
        }
    }
}
