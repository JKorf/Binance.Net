using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of a transfer between spot and futures account
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesTransferStatus>))]
    public enum FuturesTransferStatus
    {
        /// <summary>
        /// ["<c>PENDING</c>"] Pending to execute
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// ["<c>CONFIRMED</c>"] Successfully transferred
        /// </summary>
        [Map("CONFIRMED")]
        Confirmed,
        /// <summary>
        /// ["<c>FAILED</c>"] Execution failed
        /// </summary>
        [Map("FAILED")]
        Failed
    }
}

