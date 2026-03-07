namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Submit result
    /// </summary>
    public record BinanceTravelRuleSubmitResult
    {
        /// <summary>
        /// ["<c>accepted</c>"] Whether the submission was accepted.
        /// </summary>
        [JsonPropertyName("accepted")]
        public bool Accepted { get; set; }
        /// <summary>
        /// ["<c>info</c>"] Additional submission info.
        /// </summary>
        [JsonPropertyName("info")]
        public string Info { get; set; } = string.Empty;
    }
}

