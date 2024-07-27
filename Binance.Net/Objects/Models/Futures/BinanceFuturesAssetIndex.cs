namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Futures asset index
    /// </summary>
    public record BinanceFuturesAssetIndex
    {
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Index
        /// </summary>
        [JsonPropertyName("index")]
        public decimal Index { get; set; }
        /// <summary>
        /// Bid buffer
        /// </summary>
        [JsonPropertyName("bidBuffer")]
        public decimal BidBuffer { get; set; }
        /// <summary>
        /// Ask buffer
        /// </summary>
        [JsonPropertyName("askBuffer")]
        public decimal AskBuffer { get; set; }
        /// <summary>
        /// Bid price
        /// </summary>
        [JsonPropertyName("bidRate")]
        public decimal BidPrice { get; set; }
        /// <summary>
        /// Ask price
        /// </summary>
        [JsonPropertyName("askRate")]
        public decimal AskPrice { get; set; }
        /// <summary>
        /// Auto exchange bid buffer
        /// </summary>
        [JsonPropertyName("autoExchangeBidBuffer")]
        public decimal AutoExchangeBidBuffer { get; set; }
        /// <summary>
        /// Auto exchange ask buffer
        /// </summary>
        [JsonPropertyName("autoExchangeAskBuffer")]
        public decimal AutoExchangeAskBuffer { get; set; }
        /// <summary>
        /// Auto exchange bid price
        /// </summary>
        [JsonPropertyName("autoExchangeBidRate")]
        public decimal AutoExchangeBidPrice { get; set; }
        /// <summary>
        /// Auto exchange ask price
        /// </summary>
        [JsonPropertyName("autoExchangeAskRate")]
        public decimal AutoExchangeAskPrice { get; set; }
    }
}
