using System;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Objects
{
    public class BinanceClientOptions : ClientOptions
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

        public BinanceClientOptions()
        {
            BaseAddress = "https://api.binance.com";
        }

        public BinanceClientOptions Copy()
        {
            var copy = Copy<BinanceClientOptions>();
            copy.AutoTimestamp = AutoTimestamp;
            copy.AutoTimestampRecalculationInterval = AutoTimestampRecalculationInterval;
            copy.TradeRulesBehaviour = TradeRulesBehaviour;
            copy.TradeRulesUpdateInterval = TradeRulesUpdateInterval;
            copy.ReceiveWindow = ReceiveWindow;
            return copy;
        }
    }

    public class BinanceSocketClientOptions : SocketClientOptions
    {
        /// <summary>
        /// The base address for combined data in socket connections
        /// </summary>
        public string BaseSocketCombinedAddress { get; set; } = "wss://stream.binance.com:9443/";

        public BinanceSocketClientOptions()
        {
            BaseAddress = "wss://stream.binance.com:9443/ws/";
        }        

        public BinanceSocketClientOptions Copy()
        {
            var copy = Copy<BinanceSocketClientOptions>();
            copy.BaseSocketCombinedAddress = BaseSocketCombinedAddress;
            return copy;
        }

    }
}
