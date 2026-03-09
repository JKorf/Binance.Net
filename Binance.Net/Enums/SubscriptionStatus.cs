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
        /// ["<c>PURCHASING</c>"] Purchasing
        /// </summary>
        [Map("PURCHASING")]
        Purchasing,
        /// <summary>
        /// ["<c>SUCCESS</c>"] Success
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// ["<c>FAILED</c>"] Failed
        /// </summary>
        [Map("FAILED")]
        Failed
    }
}

