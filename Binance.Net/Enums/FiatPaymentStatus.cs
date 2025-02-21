using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of a fiat payment
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FiatPaymentStatus>))] public  enum FiatPaymentStatus
    {
        /// <summary>
        /// Still processing
        /// </summary>
        [Map("Processing")]
        Processing,
        /// <summary>
        /// Successfully completed
        /// </summary>
        [Map("Completed")]
        Completed,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("Failed")]
        Failed,
        /// <summary>
        /// Refunded
        /// </summary>
        [Map("Refunded")]
        Refunded
    }
}
