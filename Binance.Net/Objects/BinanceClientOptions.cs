using System;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Objects
{
    public class BinanceClientOptions : ExchangeOptions
    {
        public BinanceClientOptions()
        {
            BaseAddress = "https://api.binance.com";
        }

        /// <summary>
        /// Whether or not to automatically sync the local time with the server time
        /// </summary>
        public bool AutoTimestamp { get; set; } = false;

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
    }

    public class BinanceSocketClientOptions : ExchangeOptions
    {
        public BinanceSocketClientOptions()
        {
            BaseAddress = "wss://stream.binance.com:9443/ws/";
        }

        /// <summary>
        /// The base address for combined data in socket connections
        /// </summary>
        public string BaseSocketCombinedAddress { get; set; } = "wss://stream.binance.com:9443/";

        /// <summary>
        /// What should be done when the connection is interupted
        /// </summary>
        public ReconnectBehaviour ReconnectTryBehaviour { get; set; } = ReconnectBehaviour.AutoReconnect;

        /// <summary>
        /// The interval to try to reconnect the websocket after the connection was lost
        /// </summary>
        public TimeSpan ReconnectTryInterval { get; set; } = TimeSpan.FromSeconds(5);
    }
}
