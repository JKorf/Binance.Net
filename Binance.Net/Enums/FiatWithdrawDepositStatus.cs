namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of a fiat payment
    /// </summary>
    public enum FiatWithdrawDepositStatus
    {
        /// <summary>
        /// Still processing
        /// </summary>
        Processing,
        /// <summary>
        /// Successfully completed
        /// </summary>
        Successful,
        /// <summary>
        /// Failed
        /// </summary>
        Failed,
        /// <summary>
        /// Finished
        /// </summary>
        Finished,
        /// <summary>
        /// Refunding
        /// </summary>
        Refunding,
        /// <summary>
        /// Refunded
        /// </summary>
        Refunded,
        /// <summary>
        /// Refund failed
        /// </summary>
        RefundFailed,
        /// <summary>
        /// Expired
        /// </summary>
        Expired
    }
}
