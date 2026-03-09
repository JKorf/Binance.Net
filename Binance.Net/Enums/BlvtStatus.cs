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
        /// ["<c>P</c>"] Pending
        /// </summary>
        [Map("P")]
        Pending,
        /// <summary>
        /// ["<c>S</c>"] Success
        /// </summary>
        [Map("S")]
        Success,
        /// <summary>
        /// ["<c>F</c>"] Failure
        /// </summary>
        [Map("F")]
        Failure
    }
}

