namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Response to the change in initial leverage request
    /// </summary>
    public record BinanceFuturesInitialLeverageChangeResult
    {
        /// <summary>
        /// New leverage multiplier
        /// </summary>
        [JsonPropertyName("leverage")]
        public int Leverage { get; set; }

        /// <summary>
        /// Maximum value that can be held
        /// NOTE: string type, because the value van be 'inf' (infinite)
        /// </summary>
        [JsonPropertyName("maxNotionalValue")]
        public string? MaxNotionalValue { get; set; }

        /// <summary>
        /// Max quantity
        /// </summary>
        [JsonPropertyName("maxQty")]
        public string? MaxQuantity { get; set; }
        /// <summary>
        /// Symbol the request is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
    }

}
