namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Wrap history
    /// </summary>
    [SerializationModel]
    public record BinanceBethWrapHistory
    {
        /// <summary>
        /// ["<c>exchangeRate</c>"] Exchange rate
        /// </summary>
        [JsonPropertyName("exchangeRate")]
        public decimal ExchangeRate { get; set; }
        /// <summary>
        /// ["<c>toAmount</c>"] Output quantity
        /// </summary>
        [JsonPropertyName("toAmount")]
        public decimal ToQuantity { get; set; }
        /// <summary>
        /// ["<c>fromAmount</c>"] Input quantity
        /// </summary>
        [JsonPropertyName("fromAmount")]
        public decimal FromQuantity { get; set; }
        /// <summary>
        /// ["<c>time</c>"] The timestamp.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ["<c>fromAsset</c>"] From asset
        /// </summary>
        [JsonPropertyName("fromAsset")]
        public string FromAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>toAsset</c>"] To asset
        /// </summary>
        [JsonPropertyName("toAsset")]
        public string ToAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}

