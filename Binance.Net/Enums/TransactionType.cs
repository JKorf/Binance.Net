using Binance.Net.Converters;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Transaction type
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<TransactionType>))] public  enum TransactionType
    {
        /// <summary>
        /// Deposit
        /// </summary>
        Deposit,
        /// <summary>
        /// Withdrawal
        /// </summary>
        Withdrawal
    }
}
