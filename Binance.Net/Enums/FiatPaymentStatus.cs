using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of a fiat payment
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FiatPaymentStatus>))]
    public enum FiatPaymentStatus
    {
        /// <summary>
        /// ["<c>Processing</c>"] Still processing
        /// </summary>
        [Map("Processing")]
        Processing,
        /// <summary>
        /// ["<c>Completed</c>"] Successfully completed
        /// </summary>
        [Map("Completed")]
        Completed,
        /// <summary>
        /// ["<c>Failed</c>"] Failed
        /// </summary>
        [Map("Failed")]
        Failed,
        /// <summary>
        /// ["<c>Refunded</c>"] Refunded
        /// </summary>
        [Map("Refunded")]
        Refunded
    }
}

