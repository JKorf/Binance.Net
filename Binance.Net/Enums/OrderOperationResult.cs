using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Operation result
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderOperationResult>))]
    public enum OrderOperationResult
    {
        /// <summary>
        /// Successful
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("FAILURE")]
        Failure,
        /// <summary>
        /// Not attempted
        /// </summary>
        [Map("NOT_ATTEMPTED")]
        NotAttempted
    }
}
