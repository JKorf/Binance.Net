namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Margin asset info
    /// </summary>
    [SerializationModel]
    public record BinanceMarginAsset
    {
        /// <summary>
        /// ["<c>assetFullName</c>"] Full name of the asset
        /// </summary>
        [JsonPropertyName("assetFullName")]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>assetName</c>"] Short name of the asset
        /// </summary>
        [JsonPropertyName("assetName")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isBorrowable</c>"] Is borrowable
        /// </summary>
        [JsonPropertyName("isBorrowable")]
        public bool IsBorrowable { get; set; }
        /// <summary>
        /// ["<c>isMortgageable</c>"] Is mortgageable
        /// </summary>
        [JsonPropertyName("isMortgageable")]
        public bool IsMortgageable { get; set; }
        /// <summary>
        /// ["<c>userMinBorrow</c>"] Minimal quantity which can be borrowed
        /// </summary>
        [JsonPropertyName("userMinBorrow")]
        public decimal MinimalBorrowQuantity { get; set; }
        /// <summary>
        /// ["<c>userMinRepay</c>"] Minimal quantity which can be repaid
        /// </summary>
        [JsonPropertyName("userMinRepay")]
        public decimal MinimalRepayQuantity { get; set; }
        /// <summary>
        /// ["<c>delistTime</c>"] Time at which the asset gets delisted
        /// </summary>
        [JsonPropertyName("delistTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DelistTime { get; set; }
    }
}

