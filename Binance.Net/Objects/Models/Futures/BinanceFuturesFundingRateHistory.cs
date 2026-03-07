namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Funding rate information for Futures trading
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesFundingRateHistory
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol the information is about
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fundingRate</c>"] The funding rate for the given symbol and time.
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>fundingTime</c>"] The time the funding rate is applied
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("fundingTime")]
        public DateTime FundingTime { get; set; }
        /// <summary>
        /// ["<c>markPrice</c>"] The mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal? MarkPrice { get; set; }
    }
}

