namespace Binance.Net.Objects.Models.Spot.IsolatedMargin
{
    /// <summary>
    /// Result of creating isolated margin account
    /// </summary>
    [SerializationModel]
    public record CreateIsolatedMarginAccountResult
    {
        /// <summary>
        /// ["<c>success</c>"] Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }
}

