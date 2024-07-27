namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Funding rate information for Futures trading
    /// </summary>
    public record BinanceFuturesFundingInfo
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Adjusted funding rate cap
        /// </summary>
        [JsonPropertyName("adjustedFundingRateCap")]
        public decimal AdjustedFundingRateCap { get; set; }
        /// <summary>
        /// Adjusted funding rate floor
        /// </summary>
        [JsonPropertyName("adjustedFundingRateFloor")]
        public decimal AdjustedFundingRateFloor { get; set; }
        /// <summary>
        /// Funding interval in hours
        /// </summary>
        [JsonPropertyName("fundingIntervalHours")]
        public int FundingIntervalHours { get; set; }
    }
}
