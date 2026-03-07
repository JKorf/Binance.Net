namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// User commission rate
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesAccountUserCommissionRate
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>makerCommissionRate</c>"] Maker commission rate
        /// </summary>
        [JsonPropertyName("makerCommissionRate")]
        public decimal MakerCommissionRate { get; set; }
        /// <summary>
        /// ["<c>takerCommissionRate</c>"] Taker commission rate
        /// </summary>
        [JsonPropertyName("takerCommissionRate")]
        public decimal TakerCommissionRate { get; set; }
        /// <summary>
        /// ["<c>rpiCommissionRate</c>"] RPI commission rate
        /// </summary>
        [JsonPropertyName("rpiCommissionRate")]
        public decimal RpiCommissionRate { get; set; }
    }
}

