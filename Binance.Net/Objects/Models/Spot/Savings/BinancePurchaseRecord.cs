﻿using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Lending
{
    /// <summary>
    /// Purchase record
    /// </summary>
    public record BinancePurchaseRecord
    {
        /// <summary>
        /// Quantity purchased
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Asset name
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Lending type
        /// </summary>
        public LendingType LendingType { get; set; }
        /// <summary>
        /// Lot
        /// </summary>
        public int Lot { get; set; }
        /// <summary>
        /// Name of the product
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// Purchase id
        /// </summary>
        public string PurchaseId { get; set; } = string.Empty;

        /// <summary>
        /// Purchase status
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}
