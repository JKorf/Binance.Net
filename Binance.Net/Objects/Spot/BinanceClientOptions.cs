using System;
using Binance.Net.Enums;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Objects.Spot
{
    /// <summary>
    /// Options for the binance client
    /// </summary>
    public class BinanceClientOptions : RestClientOptions
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
        /// Base address for the futures API
        /// </summary>
        public string FuturesBaseAddress { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceClientOptions(): base("https://api.binance.com")
        {
            FuturesBaseAddress = "https://fapi.binance.com";
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseAddress">Сustom url to connect via mirror website</param>
        /// <param name="futuresBaseAddress">Сustom futures url to connect via mirror website</param>
        public BinanceClientOptions(string baseAddress, string futuresBaseAddress) : base(baseAddress)
        {
            FuturesBaseAddress = futuresBaseAddress;
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
            copy.FuturesBaseAddress = FuturesBaseAddress;
            return copy;
        }
    }

    /// <summary>
    /// Binance socket client options
    /// </summary>
    public class BinanceSocketClientOptions : SocketClientOptions
    {
        /// <summary>
        /// The base address for combined data in socket connections
        /// </summary>
        public string BaseSocketCombinedAddress { get; set; } = "wss://stream.binance.com:9443/";

        /// <summary>
        /// The base address for futures
        /// </summary>
        public string BaseAddressFutures { get; set; } = "wss://fstream.binance.com/ws/";

        /// <summary>
        /// The base address for futures
        /// </summary>
        public string BaseAddressFuturesCombined { get; set; } = "wss://fstream.binance.com/";

        /// <summary>
        /// The amount of subscriptions that should be made on a single socket connection. Not all exchanges support multiple subscriptions on a single socket.
        /// Setting this to a higher number increases subscription speed, but having more subscriptions on a single connection will also increase the amount of traffic on that single connection.
        /// Not available on Binance.
        /// </summary>
        public new int? SocketSubscriptionsCombineTarget
        {
            get => 1;
            set
            {
                if (value != 1)
                    throw new ArgumentException("Can't change SocketSubscriptionsCombineTarget; server implementation does not allow multiple subscription on a socket");
            }
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="address">Custom address</param>
        /// <param name="addressCombined">Custom address for combined streams</param>
        /// <param name="futuresAddress">Custom address for futures</param>
        /// <param name="futuresCombinedAddress">Custom address for futures combined streams</param>
        public BinanceSocketClientOptions(string address, string addressCombined, string futuresAddress, string futuresCombinedAddress): base(address)
        {
            BaseAddressFutures = futuresAddress;
            BaseSocketCombinedAddress = addressCombined;
            BaseAddressFuturesCombined = futuresCombinedAddress;
        }

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceSocketClientOptions(): base("wss://stream.binance.com:9443/ws/")
        {
        }

        /// <summary>
        /// Return a copy of these options
        /// </summary>
        /// <returns></returns>
        public BinanceSocketClientOptions Copy()
        {
            var copy = Copy<BinanceSocketClientOptions>();
            copy.BaseSocketCombinedAddress = BaseSocketCombinedAddress;
            copy.BaseAddressFutures = BaseAddressFutures;
            return copy;
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
        public int? Limit { get; }

        /// <summary>
        /// Update interval in milliseconds, either 100 or 1000. Defaults to 1000
        /// </summary>
        public int? UpdateInterval { get; }

        /// <summary>
        /// Create new options
        /// </summary>
        /// <param name="limit">The top amount of results to keep in sync. If for example limit=10 is used, the order book will contain the 10 best bids and 10 best asks. Leaving this null will sync the full order book</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000. Defaults to 1000</param>
        public BinanceOrderBookOptions(int? limit = null, int? updateInterval = null): base("Binance", limit == null, false)
        {
            Limit = limit;
            UpdateInterval = updateInterval;
        }
    }
}
