namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of a fiat payment
    /// </summary>
    public enum FiatPaymentStatus
    {
        /// <summary>
        /// Still processing
        /// </summary>
        Processing,
        /// <summary>
        /// Successfully completed
        /// </summary>
        Completed,
        /// <summary>
        /// Failed
        /// </summary>
        Failed,
        /// <summary>
        /// Refunded
        /// </summary>
        Refunded
    }
}
