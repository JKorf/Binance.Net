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
    public class BinanceClientOptions : ClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static BinanceClientOptions Default { get; set; } = new BinanceClientOptions();

        /// <summary>
        /// The default receive window for requests
        /// </summary>
        public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

        private BinanceApiClientOptions _spotApiOptions = new BinanceApiClientOptions(BinanceApiAddresses.Default.RestClientAddress)
        {
            AutoTimestamp = true,
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
            set => _spotApiOptions = new BinanceApiClientOptions(_spotApiOptions, value);
        }

        private BinanceApiClientOptions _usdFuturesApiOptions = new BinanceApiClientOptions(BinanceApiAddresses.Default.UsdFuturesRestClientAddress!)
        {
            AutoTimestamp = true
        };
        /// <summary>
        /// Usd futures API options
        /// </summary>
        public BinanceApiClientOptions UsdFuturesApiOptions
        {
            get => _usdFuturesApiOptions;
            set => _usdFuturesApiOptions = new BinanceApiClientOptions(_usdFuturesApiOptions, value);
        }

        private BinanceApiClientOptions _coinFuturesApiOptions = new BinanceApiClientOptions(BinanceApiAddresses.Default.CoinFuturesRestClientAddress!)
        {
            AutoTimestamp = true
        };
        /// <summary>
        /// Coin futures API options
        /// </summary>
        public BinanceApiClientOptions CoinFuturesApiOptions
        {
            get => _coinFuturesApiOptions;
            set => _coinFuturesApiOptions = new BinanceApiClientOptions(_coinFuturesApiOptions, value);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceClientOptions() : this(Default)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn">Base the new options on other options</param>
        internal BinanceClientOptions(BinanceClientOptions baseOn) : base(baseOn)
        {
            if (baseOn == null)
                return;

            ReceiveWindow = baseOn.ReceiveWindow;

            _spotApiOptions = new BinanceApiClientOptions(baseOn.SpotApiOptions, null);
            _usdFuturesApiOptions = new BinanceApiClientOptions(baseOn.UsdFuturesApiOptions, null);
            _coinFuturesApiOptions = new BinanceApiClientOptions(baseOn.CoinFuturesApiOptions, null);
        }
    }

    /// <summary>
    /// Binance socket client options
    /// </summary>
    public class BinanceSocketClientOptions : ClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static BinanceSocketClientOptions Default { get; set; } = new BinanceSocketClientOptions();

        private SocketApiClientOptions _spotStreamsOptions = new SocketApiClientOptions(BinanceApiAddresses.Default.SocketClientAddress)
        {
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// Spot streams options
        /// </summary>
        public SocketApiClientOptions SpotStreamsOptions
        {
            get => _spotStreamsOptions;
            set => _spotStreamsOptions = new SocketApiClientOptions(_spotStreamsOptions, value);
        }

        private SocketApiClientOptions _usdFuturesStreamsOptions = new SocketApiClientOptions(BinanceApiAddresses.Default.UsdFuturesSocketClientAddress!)
        {
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// Usd futures streams options
        /// </summary>
        public SocketApiClientOptions UsdFuturesStreamsOptions
        {
            get => _usdFuturesStreamsOptions;
            set => _usdFuturesStreamsOptions = new SocketApiClientOptions(_usdFuturesStreamsOptions, value);
        }

        private SocketApiClientOptions _coinFuturesStreamsOptions = new SocketApiClientOptions(BinanceApiAddresses.Default.CoinFuturesSocketClientAddress!)
        {
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// Coin futures streams options
        /// </summary>
        public SocketApiClientOptions CoinFuturesStreamsOptions
        {
            get => _coinFuturesStreamsOptions;
            set => _coinFuturesStreamsOptions = new SocketApiClientOptions(_coinFuturesStreamsOptions, value);
        }

        /// <summary>
        /// Address for conencting the BLVT streams
        /// </summary>
        public string? BlvtStreamAddress { get; set; } = BinanceApiAddresses.Default.BlvtSocketClientAddress;

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceSocketClientOptions() : this(Default)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn">Base the new options on other options</param>
        internal BinanceSocketClientOptions(BinanceSocketClientOptions baseOn) : base(baseOn)
        {
            if (baseOn == null)
                return;

            BlvtStreamAddress = baseOn.BlvtStreamAddress;

            _spotStreamsOptions = new SocketApiClientOptions(baseOn.SpotStreamsOptions, null);
            _usdFuturesStreamsOptions = new SocketApiClientOptions(baseOn.UsdFuturesStreamsOptions, null);
            _coinFuturesStreamsOptions = new SocketApiClientOptions(baseOn.CoinFuturesStreamsOptions, null);
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
        internal BinanceApiClientOptions(string baseAddress) : base(baseAddress)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn"></param>
        /// <param name="newValues"></param>
        internal BinanceApiClientOptions(BinanceApiClientOptions baseOn, BinanceApiClientOptions? newValues) : base(baseOn, newValues)
        {
            TimestampOffset = newValues?.TimestampOffset ?? baseOn.TimestampOffset;
            TradeRulesBehaviour = newValues?.TradeRulesBehaviour ?? baseOn.TradeRulesBehaviour;
            TradeRulesUpdateInterval = newValues?.TradeRulesUpdateInterval ?? baseOn.TradeRulesUpdateInterval;
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
        /// After how much time we should consider the connection dropped if no data is received for this time after the initial subscriptions
        /// </summary>
        public TimeSpan? InitialDataTimeout { get; set; }

        /// <summary>
        /// The rest client to use for requesting the initial order book
        /// </summary>
        public IBinanceClient? RestClient { get; set; }

        /// <summary>
        /// The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.
        /// </summary>
        public IBinanceSocketClient? SocketClient { get; set; }
    }
}
