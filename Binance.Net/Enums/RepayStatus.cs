using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Repay status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<RepayStatus>))]
    public enum RepayStatus
    {
        /// <summary>
        /// Repaid
        /// </summary>
        [Map("Repaid")]
        Repaid,
        /// <summary>
        /// Repaying
        /// </summary>
        [Map("Repaying")]
        Repaying,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("Failed")]
        Failed
    }
}
