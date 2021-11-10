using System;
using System.Linq;
using System.Net.Http;
using Binance.Net.Clients.Rest.CoinFutures;
using Binance.Net.Clients.Rest.Spot;
using Binance.Net.Clients.Rest.UsdFutures;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.Rest.Margin;
using Binance.Net.Interfaces.Clients.Rest.UsdFutures;
using Binance.Net.Interfaces.Clients.Socket;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Options for the binance client
    /// </summary>
    public class BinanceClientOptionsBase : RestClientOptions
    {
        /// <summary>
        /// Whether or not to automatically sync the local time with the server time
        /// </summary>
        public bool AutoTimestamp { get; set; } = true;

        /// <summary>
        /// Interval for refreshing the auto timestamp calculation
        /// </summary>
        public TimeSpan AutoTimestampRecalculationInterval { get; set; } = TimeSpan.FromHours(3);

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
        /// The default receive window for requests
        /// </summary>
        public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Copy the values of the def to the input
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="def"></param>
        public new void Copy<T>(T input, T def) where T: BinanceClientOptionsBase
        {
            base.Copy(input, def);

            input.AutoTimestamp = def.AutoTimestamp;
            input.AutoTimestampRecalculationInterval = def.AutoTimestampRecalculationInterval;
            input.ReceiveWindow = def.ReceiveWindow;
            input.TimestampOffset = def.TimestampOffset;
            input.TradeRulesBehaviour = def.TradeRulesBehaviour;
            input.TradeRulesUpdateInterval = def.TradeRulesUpdateInterval;
        }
    }

    /// <summary>
    /// Spot client options
    /// </summary>
    public class BinanceClientSpotOptions : BinanceClientOptionsBase
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static BinanceClientSpotOptions Default { get; set; } = new BinanceClientSpotOptions()
        {
            BaseAddress = BinanceApiAddresses.Default.RestClientAddress
        };

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceClientSpotOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }
    }

    /// <summary>
    /// Usd futures client options
    /// </summary>
    public class BinanceClientUsdFuturesOptions : BinanceClientOptionsBase
    {
        /// <summary>
        /// Default options for the usd futures client
        /// </summary>
        public static BinanceClientUsdFuturesOptions Default { get; set; } = new BinanceClientUsdFuturesOptions()
        {
            BaseAddress = BinanceApiAddresses.Default.UsdFuturesRestClientAddress!
        };

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceClientUsdFuturesOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }
    }

    /// <summary>
    /// Coin futures client options
    /// </summary>
    public class BinanceClientCoinFuturesOptions : BinanceClientOptionsBase
    {
        /// <summary>
        /// Default options for the coin futures client
        /// </summary>
        public static BinanceClientCoinFuturesOptions Default { get; set; } = new BinanceClientCoinFuturesOptions()
        {
            BaseAddress = BinanceApiAddresses.Default.CoinFuturesRestClientAddress!
        };

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceClientCoinFuturesOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }
    }

    /// <summary>
    /// Binance socket client options
    /// </summary>
    public class BinanceSocketClientSpotOptions : SocketClientOptions
    {
        /// <summary>
        /// Default options for the spot socket client
        /// </summary>
        public static BinanceSocketClientSpotOptions Default { get; set; } = new BinanceSocketClientSpotOptions()
        {
            BaseAddress = BinanceApiAddresses.Default.SocketClientAddress,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceSocketClientSpotOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }
    }

    /// <summary>
    /// Binance socket client options
    /// </summary>
    public class BinanceSocketClientUsdFuturesOptions : SocketClientOptions
    {
        /// <summary>
        /// Default options for the usd futures socket client
        /// </summary>
        public static BinanceSocketClientUsdFuturesOptions Default { get; set; } = new BinanceSocketClientUsdFuturesOptions()
        {
            BaseAddress = BinanceApiAddresses.Default.UsdFuturesSocketClientAddress!,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceSocketClientUsdFuturesOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }
    }

    /// <summary>
    /// Binance socket client options
    /// </summary>
    public class BinanceSocketClientCoinFuturesOptions : SocketClientOptions
    {
        /// <summary>
        /// Default options for the coin futures socket client
        /// </summary>
        public static BinanceSocketClientCoinFuturesOptions Default { get; set; } = new BinanceSocketClientCoinFuturesOptions()
        {
            BaseAddress = BinanceApiAddresses.Default.CoinFuturesSocketClientAddress!,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceSocketClientCoinFuturesOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }
    }

    /// <summary>
    /// Binance symbol order book options
    /// </summary>
    public abstract class BinanceOrderBookOptions : OrderBookOptions
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
        /// Create new options
        /// </summary>
        /// <param name="limit">The top amount of results to keep in sync. If for example limit=10 is used, the order book will contain the 10 best bids and 10 best asks. Leaving this null will sync the full order book</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000. Defaults to 1000</param>
        public BinanceOrderBookOptions(int? limit = null, int? updateInterval = null)
        {
            Limit = limit;
            UpdateInterval = updateInterval;
        }
    }

    /// <summary>
    /// Binance symbol order book options
    /// </summary>
    public class BinanceSpotOrderBookOptions : BinanceOrderBookOptions
    {
        /// <summary>
        /// The rest client to use for requesting the initial order book
        /// </summary>
        public IBinanceClientSpot? RestClient { get; set; }

        /// <summary>
        /// The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.
        /// </summary>
        public IBinanceSocketClientSpot? SocketClient { get; set; }

        /// <summary>
        /// Create new options
        /// </summary>
        /// <param name="limit">The top amount of results to keep in sync. If for example limit=10 is used, the order book will contain the 10 best bids and 10 best asks. Leaving this null will sync the full order book</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000. Defaults to 1000</param>
        /// <param name="socketClient">The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.</param>
        /// <param name="restClient">The rest client to use for requesting the initial order book.</param>
        public BinanceSpotOrderBookOptions(int? limit = null, int? updateInterval = null, IBinanceSocketClientSpot? socketClient = null, IBinanceClientSpot? restClient = null) 
            : base(limit, updateInterval)
        {
            RestClient = restClient;
            SocketClient = socketClient;
        }
    }

    /// <summary>
    /// Binance symbol order book options
    /// </summary>
    public class BinanceUsdFuturesOrderBookOptions : BinanceOrderBookOptions
    {
        /// <summary>
        /// The rest client to use for requesting the initial order book
        /// </summary>
        public IBinanceClientUsdFutures? RestClient { get; set; }
        /// <summary>
        /// The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.
        /// </summary>
        public IBinanceSocketClientUsdFutures? SocketClient { get; set; }

        /// <summary>
        /// Create new options
        /// </summary>
        /// <param name="limit">The top amount of results to keep in sync. If for example limit=10 is used, the order book will contain the 10 best bids and 10 best asks. Leaving this null will sync the full order book</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000. Defaults to 1000</param>
        /// <param name="socketClient">The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.</param>
        /// <param name="restClient">The rest client to use for requesting the initial order book.</param>
        public BinanceUsdFuturesOrderBookOptions(int? limit = null, int? updateInterval = null, IBinanceSocketClientUsdFutures? socketClient = null, IBinanceClientUsdFutures? restClient = null)
            : base(limit, updateInterval)
        {
            RestClient = restClient;
            SocketClient = socketClient;
        }
    }

    /// <summary>
    /// Binance symbol order book options
    /// </summary>
    public class BinanceCoinFuturesOrderBookOptions : BinanceOrderBookOptions
    {
        /// <summary>
        /// The rest client to use for requesting the initial order book
        /// </summary>
        public IBinanceClientCoinFutures? RestClient { get; set; }
        /// <summary>
        /// The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.
        /// </summary>
        public IBinanceSocketClientCoinFutures? SocketClient { get; set; }

        /// <summary>
        /// Create new options
        /// </summary>
        /// <param name="limit">The top amount of results to keep in sync. If for example limit=10 is used, the order book will contain the 10 best bids and 10 best asks. Leaving this null will sync the full order book</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000. Defaults to 1000</param>
        /// <param name="socketClient">The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.</param>
        /// <param name="restClient">The rest client to use for requesting the initial order book.</param>
        public BinanceCoinFuturesOrderBookOptions(int? limit = null, int? updateInterval = null, IBinanceSocketClientCoinFutures? socketClient = null, IBinanceClientCoinFutures? restClient = null)
            : base(limit, updateInterval)
        {
            RestClient = restClient;
            SocketClient = socketClient;
        }
    }
}
