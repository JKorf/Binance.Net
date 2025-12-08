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
        /// Redeem to spot account
        /// </summary>
        [Map("MATURE")]
        ToSpot,
        /// <summary>
        /// Redeem to flexible product
        /// </summary>
        [Map("NEW_TRANSFERRED")]
        ToFlexibleProduct,
        /// <summary>
        /// Early redemption
        /// </summary>
        [Map("AHEAD")]
        EarlyRedemption
    }
}
