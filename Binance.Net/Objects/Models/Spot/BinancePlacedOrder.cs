namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// The result of placing a new order
    /// </summary>
    public record BinancePlacedOrder: BinanceOrderBase
    {
        /// <summary>
        /// The time the order was placed
        /// </summary>
        [JsonPropertyName("transactTime"), JsonConverter(typeof(DateTimeConverter))]
        public new DateTime CreateTime { get; set; }
        
        /// <summary>
        /// Trades for the order
        /// </summary>
        [JsonPropertyName("fills")]
        public IEnumerable<BinanceOrderTrade>? Trades { get; set; }

        /// <summary>
        /// Only present if a margin trade happened
        /// </summary>
        [JsonPropertyName("marginBuyBorrowAmount")]
        public decimal? MarginBuyBorrowQuantity { get; set; }
        /// <summary>
        /// Only present if a margin trade happened
        /// </summary>
        public string? MarginBuyBorrowAsset { get; set; }
    }
}
