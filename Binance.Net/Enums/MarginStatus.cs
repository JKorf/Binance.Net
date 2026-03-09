using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of a margin action
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginStatus>))]
    public enum MarginStatus
    {
        /// <summary>
        /// ["<c>PENDING</c>"] Pending to execution
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// ["<c>COMPLETED</c>"] Executed, waiting to be confirmed
        /// </summary>
        [Map("COMPLETED")]
        Completed,
        /// <summary>
        /// ["<c>CONFIRMED</c>"] Successfully loaned/repaid
        /// </summary>
        [Map("CONFIRMED")]
        Confirmed,
        /// <summary>
        /// ["<c>FAILED</c>"] execution failed, nothing happened to your account
        /// </summary>
        [Map("FAILED")]
        Failed
    }
}

