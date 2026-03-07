namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Futures stream symbol update
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesStreamAssetIndexUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>s</c>"] The symbol.
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>i</c>"] Index price
        /// </summary>
        [JsonPropertyName("i")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// ["<c>b</c>"] Bid buffer
        /// </summary>
        [JsonPropertyName("b")]
        public decimal BidBuffer { get; set; }
        /// <summary>
        /// ["<c>a</c>"] Ask buffer
        /// </summary>
        [JsonPropertyName("a")]
        public decimal AskBuffer { get; set; }
        /// <summary>
        /// ["<c>B</c>"] Bid rate
        /// </summary>
        [JsonPropertyName("B")]
        public decimal BidRate { get; set; }
        /// <summary>
        /// ["<c>A</c>"] Ask rate
        /// </summary>
        [JsonPropertyName("A")]
        public decimal AskRate { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Auto exchange bid buffer
        /// </summary>
        [JsonPropertyName("q")]
        public decimal AutoExchangeBidBuffer { get; set; }
        /// <summary>
        /// ["<c>g</c>"] Auto exchange ask buffer
        /// </summary>
        [JsonPropertyName("g")]
        public decimal AutoExchangeAskBuffer { get; set; }
        /// <summary>
        /// ["<c>Q</c>"] Auto exchange bid rate
        /// </summary>
        [JsonPropertyName("Q")]
        public decimal AutoExchangeBidRate { get; set; }
        /// <summary>
        /// ["<c>G</c>"] Auto exchange ask rate
        /// </summary>
        [JsonPropertyName("G")]
        public decimal AutoExchangeAskRate { get; set; }
    }
}

