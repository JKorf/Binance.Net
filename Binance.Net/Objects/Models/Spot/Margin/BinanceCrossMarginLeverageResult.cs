namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Result
    /// </summary>
    [SerializationModel]
    public record BinanceCrossMarginLeverageResult
    {
        /// <summary>
        /// Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
