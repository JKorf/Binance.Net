using System;
using System.Net.Http;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Options for the binance client
    /// </summary>
    public class BinanceClientOptions : RestClientOptions
    {
        private string? _baseAddressUsdtFutures;
        private string? _baseAddressCoinFutures;

        /// <summary>
        /// The base address for USDT-M futures
        /// </summary>
        public string? BaseAddressUsdtFutures
        {
            get => _baseAddressUsdtFutures;
            set
            {
                var newValue = value;
                if (newValue != null && !newValue.EndsWith("/"))
                    newValue += "/";
                _baseAddressUsdtFutures = newValue;
            }
        }

        /// <summary>
        /// The base address for Coin-M futures
        /// </summary>
        public string? BaseAddressCoinFutures
        {
            get => _baseAddressCoinFutures;
            set
            {
                var newValue = value;
                if (newValue != null && !newValue.EndsWith("/"))
                    newValue += "/";
                _baseAddressCoinFutures = newValue;
            }
        }

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
        /// Constructor with default endpoints
        /// </summary>
        public BinanceClientOptions(): this(BinanceApiAddresses.Default)
        {
        }

        /// <summary>
        /// Constructor with default endpoints
        /// </summary>
        /// <param name="client">HttpClient to use for requests from this client</param>
        public BinanceClientOptions(HttpClient client) : this(BinanceApiAddresses.Default, client)
        {
        }

        /// <summary>
        /// Constructor with custom endpoints
        /// </summary>
        /// <param name="addresses">The base addresses to use</param>
        public BinanceClientOptions(BinanceApiAddresses addresses) : this(addresses.RestClientAddress, addresses.UsdtFuturesRestClientAddress, addresses.CoinFuturesRestClientAddress, null)
        {
        }


        /// <summary>
        /// Constructor with custom endpoints
        /// </summary>
        /// <param name="addresses">The base addresses to use</param>
        /// <param name="client">HttpClient to use for requests from this client</param>
        public BinanceClientOptions(BinanceApiAddresses addresses, HttpClient client) : this(addresses.RestClientAddress, addresses.UsdtFuturesRestClientAddress, addresses.CoinFuturesRestClientAddress, client)
        {
        }

        /// <summary>
        /// Constructor with custom endpoints
        /// </summary>
        /// <param name="spotBaseAddress">Сustom url for the SPOT API</param>
        /// <param name="futuresUsdtBaseAddress">Сustom url for USDT-M futures API</param>
        /// <param name="futuresCoinBaseAddress">Сustom url for Coin-M futures API</param>
        public BinanceClientOptions(string spotBaseAddress, string? futuresUsdtBaseAddress, string? futuresCoinBaseAddress) : this(spotBaseAddress, futuresUsdtBaseAddress, futuresCoinBaseAddress, null)
        {
        }

        /// <summary>
        /// Constructor with custom endpoints
        /// </summary>
        /// <param name="spotBaseAddress">Сustom url for the SPOT API</param>
        /// <param name="futuresUsdtBaseAddress">Сustom url for USDT-M futures API</param>
        /// <param name="futuresCoinBaseAddress">Сustom url for Coin-M futures API</param>
        /// <param name="client">HttpClient to use for requests from this client</param>
        public BinanceClientOptions(string spotBaseAddress, string? futuresUsdtBaseAddress, string? futuresCoinBaseAddress, HttpClient? client) : base(spotBaseAddress)
        {
            HttpClient = client;
            BaseAddressCoinFutures = futuresCoinBaseAddress;
            BaseAddressUsdtFutures = futuresUsdtBaseAddress;
        }

        /// <summary>
        /// Return a copy of these options
        /// </summary>
        /// <returns></returns>
        public BinanceClientOptions Copy()
        {
            var copy = Copy<BinanceClientOptions>();
            copy.AutoTimestamp = AutoTimestamp;
            copy.AutoTimestampRecalculationInterval = AutoTimestampRecalculationInterval;
            copy.TimestampOffset = TimestampOffset;
            copy.TradeRulesBehaviour = TradeRulesBehaviour;
            copy.TradeRulesUpdateInterval = TradeRulesUpdateInterval;
            copy.ReceiveWindow = ReceiveWindow;
            copy.BaseAddressCoinFutures = BaseAddressCoinFutures;
            copy.BaseAddressUsdtFutures = BaseAddressUsdtFutures;
            return copy;
        }
    }

    /// <summary>
    /// Binance socket client options
    /// </summary>
    public class BinanceSocketClientOptions : SocketClientOptions
    {
        private string? _baseAddressUsdtFutures;
        private string? _baseAddressCoinFutures;

        /// <summary>
        /// The base address for USDT-M futures
        /// </summary>
        public string? BaseAddressUsdtFutures
        {
            get => _baseAddressUsdtFutures;
            set
            {
                var newValue = value;
                if (newValue != null && !newValue.EndsWith("/"))
                    newValue += "/";
                _baseAddressUsdtFutures = newValue;
            }
        }

        /// <summary>
        /// The base address for Coin-M futures
        /// </summary>
        public string? BaseAddressCoinFutures
        {
            get => _baseAddressCoinFutures;
            set
            {
                var newValue = value;
                if (newValue != null && !newValue.EndsWith("/"))
                    newValue += "/";
                _baseAddressCoinFutures = newValue;
            }
        }

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceSocketClientOptions() : this(BinanceApiAddresses.Default)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="addresses">The base addresses to use</param>
        public BinanceSocketClientOptions(BinanceApiAddresses addresses) : this(addresses.SocketClientAddress, addresses.UsdtFuturesSocketClientAddress, addresses.CoinFuturesSocketClientAddress)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="address">Custom address for spot API</param>
        /// <param name="futuresUsdtAddress">Custom address for usdt futures streams</param>
        /// <param name="futuresCoinAddress">Custom address for coin futures streams</param>
        public BinanceSocketClientOptions(string address, string? futuresUsdtAddress, string? futuresCoinAddress) : base(address)
        {
            BaseAddressUsdtFutures = futuresUsdtAddress;
            BaseAddressCoinFutures = futuresCoinAddress;
            SocketSubscriptionsCombineTarget = 10;
        }

        /// <summary>
        /// Return a copy of these options
        /// </summary>
        /// <returns></returns>
        public BinanceSocketClientOptions Copy()
        {
            var copy = Copy<BinanceSocketClientOptions>();
            copy.BaseAddressCoinFutures = BaseAddressCoinFutures;
            copy.BaseAddressUsdtFutures = BaseAddressUsdtFutures;
            return copy;
        }
    }

    /// <summary>
    /// Binance symbol order book options
    /// </summary>
    public class BinanceOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// The rest client to use for requesting the initial order book
        /// </summary>
        public IBinanceClient? RestClient { get; set; }

        /// <summary>
        /// The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.
        /// </summary>
        public IBinanceSocketClient? SocketClient { get; set; }

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
        /// <param name="socketClient">The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.</param>
        /// <param name="restClient">The rest client to use for requesting the initial order book.</param>
        public BinanceOrderBookOptions(int? limit = null, int? updateInterval = null, IBinanceSocketClient? socketClient = null, IBinanceClient? restClient = null) : base("Binance", limit == null, false)
        {
            Limit = limit;
            UpdateInterval = updateInterval;
            SocketClient = socketClient;
            RestClient = restClient;
        }
    }
}
