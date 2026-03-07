namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Oco info
    /// </summary>
    [SerializationModel]
    public record BinanceMarginOrderOcoList : BinanceOrderOcoList
    {
        /// <summary>
        /// ["<c>marginBuyBorrowAmount</c>"] Margin buy borrow quantity
        /// </summary>
        [JsonPropertyName("marginBuyBorrowAmount")]
        public decimal? MarginBuyBorrowQuantity { get; set; }
        /// <summary>
        /// ["<c>marginBuyBorrowAsset</c>"] Margin buy borrow asset
        /// </summary>
        [JsonPropertyName("marginBuyBorrowAsset")]
        public string? MarginBuyBorrowAsset { get; set; }
        /// <summary>
        /// ["<c>isIsolated</c>"] Is isolated margin
        /// </summary>
        [JsonPropertyName("isIsolated")]
        public bool IsIsolated { get; set; }
    }
}

