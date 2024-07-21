using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Blvt
{
    /// <summary>
    /// Redeem result
    /// </summary>
    public record BinanceBlvtRedeemResult
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public BlvtStatus Status { get; set; }
        /// <summary>
        /// Name of the token
        /// </summary>
        public string TokenName { get; set; } = string.Empty;
        /// <summary>
        /// Redemption value in usdt
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Redemption token quantity
        /// </summary>
        [JsonPropertyName("redeemAmount")]
        public decimal RedeemQuantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
