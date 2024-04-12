namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Wrap history
    /// </summary>
    public class BinanceBethWrapHistory
    {
        /// <summary>
        /// Exchange rate
        /// </summary>
        [JsonProperty("exchangeRate")]
        public decimal ExchangeRate { get; set; }
        /// <summary>
        /// Output quantity
        /// </summary>
        [JsonProperty("toAmount")]
        public decimal ToQuantity { get; set; }
        /// <summary>
        /// Input quantity
        /// </summary>
        [JsonProperty("fromAmount")]
        public decimal FromQuantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// From asset
        /// </summary>
        [JsonProperty("fromAsset")]
        public string FromAsset { get; set; } = string.Empty;
        /// <summary>
        /// To asset
        /// </summary>
        [JsonProperty("toAsset")]
        public string ToAsset { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;
    }
}
