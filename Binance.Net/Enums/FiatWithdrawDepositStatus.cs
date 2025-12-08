using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of a fiat payment
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FiatWithdrawDepositStatus>))]
    public enum FiatWithdrawDepositStatus
    {
        /// <summary>
        /// Still processing
        /// </summary>
        [Map("Processing")]
        Processing,
        /// <summary>
        /// Successfully completed
        /// </summary>
        [Map("Successful")]
        Successful,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("Failed")]
        Failed,
        /// <summary>
        /// Finished
        /// </summary>
        [Map("Finished")]
        Finished,
        /// <summary>
        /// Refunding
        /// </summary>
        [Map("Refunding")]
        Refunding,
        /// <summary>
        /// Refunded
        /// </summary>
        [Map("Refunded")]
        Refunded,
        /// <summary>
        /// Refund failed
        /// </summary>
        [Map("Refund Failed")]
        RefundFailed,
        /// <summary>
        /// Expired
        /// </summary>
        [Map("Expired")]
        Expired
    }
}
