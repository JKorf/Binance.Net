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
        /// ["<c>Processing</c>"] Still processing
        /// </summary>
        [Map("Processing")]
        Processing,
        /// <summary>
        /// ["<c>Successful</c>"] Successfully completed
        /// </summary>
        [Map("Successful")]
        Successful,
        /// <summary>
        /// ["<c>Failed</c>"] Failed
        /// </summary>
        [Map("Failed")]
        Failed,
        /// <summary>
        /// ["<c>Finished</c>"] Finished
        /// </summary>
        [Map("Finished")]
        Finished,
        /// <summary>
        /// ["<c>Refunding</c>"] Refunding
        /// </summary>
        [Map("Refunding")]
        Refunding,
        /// <summary>
        /// ["<c>Refunded</c>"] Refunded
        /// </summary>
        [Map("Refunded")]
        Refunded,
        /// <summary>
        /// ["<c>Refund Failed</c>"] Refund failed
        /// </summary>
        [Map("Refund Failed")]
        RefundFailed,
        /// <summary>
        /// ["<c>Expired</c>"] Expired
        /// </summary>
        [Map("Expired")]
        Expired
    }
}

