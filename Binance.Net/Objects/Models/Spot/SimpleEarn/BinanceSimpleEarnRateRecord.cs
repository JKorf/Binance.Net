namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn rate record
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnRateRecord
    {
        /// <summary>
        /// ["<c>productId</c>"] Product identifier.
        /// </summary>
        [JsonPropertyName("productId")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>asset</c>"] Product asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>annualPercentageRate</c>"] Annual percentage rate
        /// </summary>
        [JsonPropertyName("annualPercentageRate")]
        public decimal AnnualPercentageRate { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}

