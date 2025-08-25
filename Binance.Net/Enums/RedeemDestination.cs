using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Simple earn redemption type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<RedeemDestination>))]
    public enum RedeemDestination
    {
        /// <summary>
        /// Redeem to spot account
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// Redeem to flexible product
        /// </summary>
        [Map("FLEXIBLE")]
        Flexible,
    }
}
