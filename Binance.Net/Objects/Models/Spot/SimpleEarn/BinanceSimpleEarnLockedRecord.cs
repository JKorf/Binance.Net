﻿using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Locked product subscription record
    /// </summary>
    public record BinanceSimpleEarnLockedRecord
    {
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Position id
        /// </summary>
        [JsonPropertyName("positionId")]
        public string PositionId { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Purchase id
        /// </summary>
        [JsonPropertyName("purchaseId")]
        public long PurchaseId { get; set; }
        /// <summary>
        /// Subscription type
        /// </summary>
        [JsonPropertyName("type"), JsonConverter(typeof(EnumConverter<SubscriptionType>))]
        public SubscriptionType Type { get; set; }
        /// <summary>
        /// Source account
        /// </summary>
        [JsonPropertyName("sourceAccount"), JsonConverter(typeof(EnumConverter<AccountSource>))]
        public AccountSource SourceAccount { get; set; }
        /// <summary>
        /// Quantity from spot
        /// </summary>
        [JsonPropertyName("amtFromSpot")]
        public decimal SpotQuantity { get; set; }
        /// <summary>
        /// Quantity from funding
        /// </summary>
        [JsonPropertyName("amtFromFunding")]
        public decimal FundingQuantity { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status"), JsonConverter(typeof(EnumConverter<SubscriptionStatus>))]
        public SubscriptionStatus Status { get; set; }
        /// <summary>
        /// Lock period
        /// </summary>
        [JsonPropertyName("lockPeriod")]
        public int LockPeriod { get; set; }
    }
}
