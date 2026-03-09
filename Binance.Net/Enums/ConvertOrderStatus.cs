using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Convert Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ConvertOrderStatus>))]
    public enum ConvertOrderStatus
    {
        /// <summary>
        /// ["<c>PROCESS</c>"] Process
        /// </summary>
        [Map("PROCESS")]
        Process,
        /// <summary>
        /// ["<c>ACCEPT_SUCCESS</c>"] Accept success
        /// </summary>
        [Map("ACCEPT_SUCCESS")]
        AcceptSuccess,
        /// <summary>
        /// ["<c>SUCCESS</c>"] Success
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// ["<c>FAIL</c>"] Fail
        /// </summary>
        [Map("FAIL")]
        Fail
    }
}

