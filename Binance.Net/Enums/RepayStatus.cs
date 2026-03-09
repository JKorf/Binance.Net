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
        /// ["<c>Repaid</c>"] Repaid
        /// </summary>
        [Map("Repaid")]
        Repaid,
        /// <summary>
        /// ["<c>Repaying</c>"] Repaying
        /// </summary>
        [Map("Repaying")]
        Repaying,
        /// <summary>
        /// ["<c>Failed</c>"] Failed
        /// </summary>
        [Map("Failed")]
        Failed
    }
}

