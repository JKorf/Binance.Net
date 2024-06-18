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
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Adjusted funding rate cap
        /// </summary>
        [JsonProperty("adjustedFundingRateCap")]
        public decimal AdjustedFundingRateCap { get; set; }
        /// <summary>
        /// Adjusted funding rate floor
        /// </summary>
        [JsonProperty("adjustedFundingRateFloor")]
        public decimal AdjustedFundingRateFloor { get; set; }
        /// <summary>
        /// Funding interval in hours
        /// </summary>
        [JsonProperty("fundingIntervalHours")]
        public int FundingIntervalHours { get; set; }
    }
}
