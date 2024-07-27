namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Result
    /// </summary>
    public record BinanceCrossMarginLeverageResult
    {
        /// <summary>
        /// Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
