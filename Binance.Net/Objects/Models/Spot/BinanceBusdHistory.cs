using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Busd convert history
    /// </summary>
    public record BinanceBusdHistory
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(EnumConverter))]
        public BusdConvertType Type { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Deducted asset
        /// </summary>
        [JsonPropertyName("deductedAsset")]
        public string DeductedAsset { get; set; } = string.Empty;
        /// <summary>
        /// Deducted quantity
        /// </summary>
        [JsonPropertyName("deductedAmount")]
        public decimal DeductedQuantity { get; set; }
        /// <summary>
        /// Target asset
        /// </summary>
        [JsonPropertyName("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// Target quantity
        /// </summary>
        [JsonPropertyName("targetAmount")]
        public decimal TargetQuantity { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public string AccountType { get; set; } = string.Empty;
    }
}
