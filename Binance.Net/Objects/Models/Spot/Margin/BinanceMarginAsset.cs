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
        [JsonProperty("assetFullName")]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// Short name of the asset
        /// </summary>
        [JsonProperty("assetName")]
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
        [JsonProperty("userMinBorrow")]
        public decimal MinimalBorrowQuantity { get; set; }
        /// <summary>
        /// Minimal quantity which can be repaid
        /// </summary>
        [JsonProperty("userMinRepay")]
        public decimal MinimalRepayQuanitty { get; set; }
        /// <summary>
        /// Time at which the asset gets delisted
        /// </summary>
        [JsonProperty("delistTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DelistTime { get; set; }
    }
}
