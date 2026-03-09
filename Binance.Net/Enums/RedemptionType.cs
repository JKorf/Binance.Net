using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Simple earn redemption type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<RedemptionType>))]
    public enum RedemptionType
    {
        /// <summary>
        /// ["<c>MATURE</c>"] Redeem to spot account
        /// </summary>
        [Map("MATURE")]
        ToSpot,
        /// <summary>
        /// ["<c>NEW_TRANSFERRED</c>"] Redeem to flexible product
        /// </summary>
        [Map("NEW_TRANSFERRED")]
        ToFlexibleProduct,
        /// <summary>
        /// ["<c>AHEAD</c>"] Early redemption
        /// </summary>
        [Map("AHEAD")]
        EarlyRedemption
    }
}

