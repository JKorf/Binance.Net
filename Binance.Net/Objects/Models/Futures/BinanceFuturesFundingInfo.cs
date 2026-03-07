namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Funding rate information for Futures trading
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesFundingInfo
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol the information is about
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>adjustedFundingRateCap</c>"] Adjusted funding rate cap
        /// </summary>
        [JsonPropertyName("adjustedFundingRateCap")]
        public decimal AdjustedFundingRateCap { get; set; }
        /// <summary>
        /// ["<c>adjustedFundingRateFloor</c>"] Adjusted funding rate floor
        /// </summary>
        [JsonPropertyName("adjustedFundingRateFloor")]
        public decimal AdjustedFundingRateFloor { get; set; }
        /// <summary>
        /// ["<c>fundingIntervalHours</c>"] Funding interval in hours.
        /// </summary>
        [JsonPropertyName("fundingIntervalHours")]
        public int FundingIntervalHours { get; set; }
    }
}

