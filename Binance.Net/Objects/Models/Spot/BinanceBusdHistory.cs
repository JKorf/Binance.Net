using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Busd convert history
    /// </summary>
    public class BinanceBusdHistory
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("tranId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter))]
        public BinanceBusdConvertType Type { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Deducted asset
        /// </summary>
        [JsonProperty("deductedAsset")]
        public string DeductedAsset { get; set; } = string.Empty;
        /// <summary>
        /// Deducted quantity
        /// </summary>
        [JsonProperty("deductedAmount")]
        public decimal DeductedQuantity { get; set; }
        /// <summary>
        /// Target asset
        /// </summary>
        [JsonProperty("targetAsset")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// Target quantity
        /// </summary>
        [JsonProperty("targetAmount")]
        public decimal TargetQuantity { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Account type
        /// </summary>
        [JsonProperty("accountType")]
        public string AccountType { get; set; } = string.Empty;
    }
}
