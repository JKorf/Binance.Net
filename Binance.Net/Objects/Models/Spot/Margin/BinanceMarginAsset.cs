namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Margin asset info
    /// </summary>
    public record BinanceMarginAsset
    {
        /// <summary>
        /// Full name of the asset
        /// </summary>
        [JsonPropertyName("assetFullName")]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// Short name of the asset
        /// </summary>
        [JsonPropertyName("assetName")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Is borrowable
        /// </summary>
        public bool IsBorrowable { get; set; }
        /// <summary>
        /// Is mortgageable
        /// </summary>
        public bool IsMortgageable { get; set; }
        /// <summary>
        /// Minimal quantity which can be borrowed
        /// </summary>
        [JsonPropertyName("userMinBorrow")]
        public decimal MinimalBorrowQuantity { get; set; }
        /// <summary>
        /// Minimal quantity which can be repaid
        /// </summary>
        [JsonPropertyName("userMinRepay")]
        public decimal MinimalRepayQuanitty { get; set; }
        /// <summary>
        /// Time at which the asset gets delisted
        /// </summary>
        [JsonPropertyName("delistTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DelistTime { get; set; }
    }
}
