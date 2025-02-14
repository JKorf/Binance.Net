using Binance.Net.Converters;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Withdrawal transfer type
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<WithdrawalTransferType>))] public  enum WithdrawalTransferType
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