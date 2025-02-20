using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of a margin action
    /// </summary>
    public enum MarginStatus
    {
        /// <summary>
        /// Pending to execution
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// Executed, waiting to be confirmed
        /// </summary>
        [Map("COMPLETED")]
        Completed,
        /// <summary>
        /// Successfully loaned/repaid
        /// </summary>
        [Map("CONFIRMED")]
        Confirmed,
        /// <summary>
        /// execution failed, nothing happened to your account
        /// </summary>
        [Map("FAILED")]
        Failed
    }
}
