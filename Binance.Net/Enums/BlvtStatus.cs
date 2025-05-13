using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of a blvt action
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BlvtStatus>))]
    public enum BlvtStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        [Map("P")]
        Pending,
        /// <summary>
        /// Success
        /// </summary>
        [Map("S")]
        Success,
        /// <summary>
        /// Failure
        /// </summary>
        [Map("F")]
        Failure
    }
}
