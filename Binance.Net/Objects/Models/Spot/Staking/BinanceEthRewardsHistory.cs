using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Rewards history
    /// </summary>
    public class BinanceEthRewardsHistory
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// BETH holding balance
        /// </summary>
        [JsonProperty("holding")]
        public decimal Holding { get; set; }
        /// <summary>
        /// Annual percentage rate
        /// </summary>
        [JsonProperty("annualPercentageRate")]
        public decimal AnnualPercentageRate { get; set; }
    }
}
