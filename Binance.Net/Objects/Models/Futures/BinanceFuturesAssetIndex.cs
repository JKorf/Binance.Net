namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Futures asset index
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesAssetIndex
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>index</c>"] Index
        /// </summary>
        [JsonPropertyName("index")]
        public decimal Index { get; set; }
        /// <summary>
        /// ["<c>bidBuffer</c>"] Bid buffer
        /// </summary>
        [JsonPropertyName("bidBuffer")]
        public decimal BidBuffer { get; set; }
        /// <summary>
        /// ["<c>askBuffer</c>"] Ask buffer
        /// </summary>
        [JsonPropertyName("askBuffer")]
        public decimal AskBuffer { get; set; }
        /// <summary>
        /// ["<c>bidRate</c>"] Bid price
        /// </summary>
        [JsonPropertyName("bidRate")]
        public decimal BidPrice { get; set; }
        /// <summary>
        /// ["<c>askRate</c>"] Ask price
        /// </summary>
        [JsonPropertyName("askRate")]
        public decimal AskPrice { get; set; }
        /// <summary>
        /// ["<c>autoExchangeBidBuffer</c>"] Auto exchange bid buffer
        /// </summary>
        [JsonPropertyName("autoExchangeBidBuffer")]
        public decimal AutoExchangeBidBuffer { get; set; }
        /// <summary>
        /// ["<c>autoExchangeAskBuffer</c>"] Auto exchange ask buffer
        /// </summary>
        [JsonPropertyName("autoExchangeAskBuffer")]
        public decimal AutoExchangeAskBuffer { get; set; }
        /// <summary>
        /// ["<c>autoExchangeBidRate</c>"] Auto exchange bid price
        /// </summary>
        [JsonPropertyName("autoExchangeBidRate")]
        public decimal AutoExchangeBidPrice { get; set; }
        /// <summary>
        /// ["<c>autoExchangeAskRate</c>"] Auto exchange ask price
        /// </summary>
        [JsonPropertyName("autoExchangeAskRate")]
        public decimal AutoExchangeAskPrice { get; set; }
    }
}

