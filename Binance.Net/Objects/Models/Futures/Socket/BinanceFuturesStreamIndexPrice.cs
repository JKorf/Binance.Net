namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Index price update
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesStreamIndexPrice : BinanceStreamEvent
    {
        /// <summary>
        /// The pair
        /// </summary>
        [JsonPropertyName("i")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// The index price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal IndexPrice { get; set; }
    }
}
