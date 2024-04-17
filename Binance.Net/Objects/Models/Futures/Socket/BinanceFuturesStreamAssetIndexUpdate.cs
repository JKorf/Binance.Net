namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Futures stream symbol update
    /// </summary>
    public class BinanceFuturesStreamAssetIndexUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Index price
        /// </summary>
        [JsonProperty("i")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// Bid buffer
        /// </summary>
        [JsonProperty("b")]
        public decimal BidBuffer { get; set; }
        /// <summary>
        /// Ask buffer
        /// </summary>
        [JsonProperty("a")]
        public decimal AskBuffer { get; set; }
        /// <summary>
        /// Bid rate
        /// </summary>
        [JsonProperty("B")]
        public decimal BidRate { get; set; }
        /// <summary>
        /// Ask rate
        /// </summary>
        [JsonProperty("A")]
        public decimal AskRate { get; set; }
        /// <summary>
        /// Auto exchange bid buffer
        /// </summary>
        [JsonProperty("q")]
        public decimal AutoExchangeBidBuffer { get; set; }
        /// <summary>
        /// Auto exchange ask buffer
        /// </summary>
        [JsonProperty("g")]
        public decimal AutoExchangeAskBuffer { get; set; }
        /// <summary>
        /// Auto exchange bid rate
        /// </summary>
        [JsonProperty("Q")]
        public decimal AutoExchangeBidRate { get; set; }
        /// <summary>
        /// Auto exchange ask rate
        /// </summary>
        [JsonProperty("G")]
        public decimal AutoExchangeAskRate { get; set; }
    }
}
