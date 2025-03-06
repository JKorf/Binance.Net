namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Wrap history
    /// </summary>
    [SerializationModel]
    public record BinanceBethWrapHistory
    {
        /// <summary>
        /// Exchange rate
        /// </summary>
        [JsonPropertyName("exchangeRate")]
        public decimal ExchangeRate { get; set; }
        /// <summary>
        /// Output quantity
        /// </summary>
        [JsonPropertyName("toAmount")]
        public decimal ToQuantity { get; set; }
        /// <summary>
        /// Input quantity
        /// </summary>
        [JsonPropertyName("fromAmount")]
        public decimal FromQuantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// From asset
        /// </summary>
        [JsonPropertyName("fromAsset")]
        public string FromAsset { get; set; } = string.Empty;
        /// <summary>
        /// To asset
        /// </summary>
        [JsonPropertyName("toAsset")]
        public string ToAsset { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}
