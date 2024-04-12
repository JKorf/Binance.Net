namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Redemption history
    /// </summary>
    public class BinanceEthRedemptionHistory
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
        /// Arrival timestamp
        /// </summary>
        [JsonProperty("arrivalTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ArrivalTime { get; set; }
    }
}
