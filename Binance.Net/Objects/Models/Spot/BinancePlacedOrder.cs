namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// The result of placing a new order
    /// </summary>
    [SerializationModel]
    public record BinancePlacedOrder : BinanceOrderBase
    {

        /// <summary>
        /// ["<c>fills</c>"] Trades for the order
        /// </summary>
        [JsonPropertyName("fills")]
        public BinanceOrderTrade[]? Trades { get; set; }

        /// <summary>
        /// ["<c>marginBuyBorrowAmount</c>"] Borrowed quantity, only present for margin trades.
        /// </summary>
        [JsonPropertyName("marginBuyBorrowAmount")]
        public decimal? MarginBuyBorrowQuantity { get; set; }
        /// <summary>
        /// ["<c>marginBuyBorrowAsset</c>"] Borrowed asset, only present for margin trades.
        /// </summary>
        [JsonPropertyName("marginBuyBorrowAsset")]
        public string? MarginBuyBorrowAsset { get; set; }
    }
}

