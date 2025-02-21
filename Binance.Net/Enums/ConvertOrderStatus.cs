using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Convert Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ConvertOrderStatus>))] public  enum ConvertOrderStatus
    {
        /// <summary>
        /// Process
        /// </summary>
        [Map("PROCESS")]
        Process,
        /// <summary>
        /// Accept success
        /// </summary>
        [Map("ACCEPT_SUCCESS")]
        AcceptSuccess,
        /// <summary>
        /// Success
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// Fail
        /// </summary>
        [Map("FAIL")]
        Fail
    }
}
