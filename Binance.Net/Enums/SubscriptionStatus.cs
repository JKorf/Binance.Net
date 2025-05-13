using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Simple earn subscription status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SubscriptionStatus>))]
    public enum SubscriptionStatus
    {
        /// <summary>
        /// Purchasing
        /// </summary>
        [Map("PURCHASING")]
        Purchasing,
        /// <summary>
        /// Success
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("FAILED")]
        Failed
    }
}
