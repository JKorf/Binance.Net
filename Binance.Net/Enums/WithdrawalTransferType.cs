namespace Binance.Net.Enums
{
    /// <summary>
    /// Withdrawal transfer type
    /// </summary>
    public enum WithdrawalTransferType
    {
        /// <summary>
        /// Withdrawal to external wallets
        /// </summary>
        ExternalTransfer,
        /// <summary>
        /// Withdrawal from one binance account to another one
        /// </summary>
        InternalTransfer
    }
}