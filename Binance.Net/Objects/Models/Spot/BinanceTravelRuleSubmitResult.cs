namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Submit result
    /// </summary>
    public record BinanceTravelRuleSubmitResult
    {
        /// <summary>
        /// Accepted
        /// </summary>
        [JsonPropertyName("accepted")]
        public bool Accepted { get; set; }
        /// <summary>
        /// Info
        /// </summary>
        [JsonPropertyName("info")]
        public string Info { get; set; } = string.Empty;
    }
}
