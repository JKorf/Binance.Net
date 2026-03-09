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
        /// ["<c>SUCCESS</c>"] Successful
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// ["<c>FAILURE</c>"] Failed
        /// </summary>
        [Map("FAILURE")]
        Failure,
        /// <summary>
        /// ["<c>NOT_ATTEMPTED</c>"] Not attempted
        /// </summary>
        [Map("NOT_ATTEMPTED")]
        NotAttempted
    }
}

