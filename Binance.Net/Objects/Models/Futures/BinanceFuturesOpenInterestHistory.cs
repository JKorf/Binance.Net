namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Open Interest History info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesOpenInterestHistory
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol the information is about
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>sumOpenInterest</c>"] Total open interest
        /// </summary>
        [JsonPropertyName("sumOpenInterest")]
        public decimal SumOpenInterest { get; set; }

        /// <summary>
        /// ["<c>sumOpenInterestValue</c>"] Total open interest value
        /// </summary>
        [JsonPropertyName("sumOpenInterestValue")]
        public decimal SumOpenInterestValue { get; set; }

        /// <summary>
        /// ["<c>timestamp</c>"] The data timestamp.
        /// </summary>
        [JsonPropertyName("timestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; set; }
    }
}

