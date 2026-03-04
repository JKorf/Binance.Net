namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Submit result
    /// </summary>
    public record BinanceTravelRuleSubmitResult
    {
        /// <summary>
        /// Whether the submission was accepted.
        /// </summary>
        [JsonPropertyName("accepted")]
        public bool Accepted { get; set; }
        /// <summary>
        /// Additional submission info.
        /// </summary>
        [JsonPropertyName("info")]
        public string Info { get; set; } = string.Empty;
    }
}
