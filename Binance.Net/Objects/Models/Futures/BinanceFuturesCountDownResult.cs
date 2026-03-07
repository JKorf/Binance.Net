namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Result of setting a countdown timer
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesCountDownResult
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>countdownTime</c>"] Count down time in milliseconds
        /// </summary>
        [JsonPropertyName("countdownTime")]
        public int CountDownTime { get; set; }
    }
}

