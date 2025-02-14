using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The status of a deposit
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<DepositStatus>))] public  enum DepositStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        [Map("0")]
        Pending,
        /// <summary>
        /// Success
        /// </summary>
        [Map("1")]
        Success,
        /// <summary>
        /// Completed
        /// </summary>
        [Map("6")]
        Completed
    }
}
