namespace Binance.Net.Objects.Models.Spot.Futures
{
    /// <summary>
    /// Borrow result
    /// </summary>
    public record BinanceCrossCollateralBorrowResult
    {
        /// <summary>
        /// Id
        /// </summary>
        public string BorrowId { get; set; } = string.Empty;
        /// <summary>
        /// The asset borrowed
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The asset used for collateral
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity borrowed
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The collateral quantity
        /// </summary>
        [JsonPropertyName("collateralAmount")]
        public decimal CollateralQuantity { get; set; }
        /// <summary>
        /// The timestamp
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
