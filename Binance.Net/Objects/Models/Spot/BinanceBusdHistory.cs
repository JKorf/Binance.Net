using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Busd convert history
    /// </summary>
    [SerializationModel]
    public record BinanceBusdHistory
    {
        /// <summary>
        /// ["<c>tranId</c>"] The transaction identifier.
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// ["<c>type</c>"] The conversion type.
        /// </summary>
        [JsonPropertyName("type")]
        public BusdConvertType Type { get; set; }
        /// <summary>
        /// ["<c>time</c>"] The conversion timestamp.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>deductedAsset</c>"] Deducted asset
        /// </summary>
        [JsonPropertyName("deductedAsset")]
        public string DeductedAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>deductedAmount</c>"] Deducted quantity
        /// </summary>
        [JsonPropertyName("deductedAmount")]
        public decimal DeductedQuantity { get; set; }
        /// <summary>
        /// ["<c>targetAsset</c>"] Target asset
        /// </summary>
        [JsonPropertyName("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>targetAmount</c>"] Target quantity
        /// </summary>
        [JsonPropertyName("targetAmount")]
        public decimal TargetQuantity { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>accountType</c>"] Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public string AccountType { get; set; } = string.Empty;
    }
}

