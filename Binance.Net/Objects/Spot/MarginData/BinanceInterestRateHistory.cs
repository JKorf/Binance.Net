using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.MarginData
{
    /// <summary>
    /// Interest rate history
    /// </summary>
    public class BinanceInterestRateHistory
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = "";
        /// <summary>
        /// The daily interest
        /// </summary>
        [JsonProperty("dailyInterestRate")]
        public decimal DailyInterest { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Vip level
        /// </summary>
        public string VipLevel { get; set; } = "";
    }
}
