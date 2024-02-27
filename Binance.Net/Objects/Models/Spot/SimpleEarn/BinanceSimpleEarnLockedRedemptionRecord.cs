using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn locked product redemption record
    /// </summary>
    public class BinanceSimpleEarnLockedRedemptionRecord
    {
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Position id
        /// </summary>
        [JsonProperty("positionId")]
        public string PositionId { get; set; } = string.Empty;
        /// <summary>
        /// Redeem id
        /// </summary>
        [JsonProperty("redeemId")]
        public long RedeemId { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Lock period
        /// </summary>
        [JsonProperty("lockPeriod")]
        public int LockPeriod { get; set; }
        /// <summary>
        /// Delivery date
        /// </summary>
        [JsonProperty("deliverDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime DeliveryDate { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(EnumConverter))]
        public RedemptionType Type { get; set; }
    }
}
