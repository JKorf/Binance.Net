namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Futures stream symbol update
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesStreamAssetIndexUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Index price
        /// </summary>
        [JsonPropertyName("i")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// Bid buffer
        /// </summary>
        [JsonPropertyName("b")]
        public decimal BidBuffer { get; set; }
        /// <summary>
        /// Ask buffer
        /// </summary>
        [JsonPropertyName("a")]
        public decimal AskBuffer { get; set; }
        /// <summary>
        /// Bid rate
        /// </summary>
        [JsonPropertyName("B")]
        public decimal BidRate { get; set; }
        /// <summary>
        /// Ask rate
        /// </summary>
        [JsonPropertyName("A")]
        public decimal AskRate { get; set; }
        /// <summary>
        /// Auto exchange bid buffer
        /// </summary>
        [JsonPropertyName("q")]
        public decimal AutoExchangeBidBuffer { get; set; }
        /// <summary>
        /// Auto exchange ask buffer
        /// </summary>
        [JsonPropertyName("g")]
        public decimal AutoExchangeAskBuffer { get; set; }
        /// <summary>
        /// Auto exchange bid rate
        /// </summary>
        [JsonPropertyName("Q")]
        public decimal AutoExchangeBidRate { get; set; }
        /// <summary>
        /// Auto exchange ask rate
        /// </summary>
        [JsonPropertyName("G")]
        public decimal AutoExchangeAskRate { get; set; }
    }
}
