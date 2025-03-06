namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// The result of placing a new order
    /// </summary>
    [SerializationModel]
    public record BinancePlacedOrder : BinanceOrderBase
    {

        /// <summary>
        /// Trades for the order
        /// </summary>
        [JsonPropertyName("fills")]
        public BinanceOrderTrade[]? Trades { get; set; }

        /// <summary>
        /// Only present if a margin trade happened
        /// </summary>
        [JsonPropertyName("marginBuyBorrowAmount")]
        public decimal? MarginBuyBorrowQuantity { get; set; }
        /// <summary>
        /// Only present if a margin trade happened
        /// </summary>
        [JsonPropertyName("marginBuyBorrowAsset")]
        public string? MarginBuyBorrowAsset { get; set; }
    }
}
