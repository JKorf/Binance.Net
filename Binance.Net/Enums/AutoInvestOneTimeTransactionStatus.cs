using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Transaction status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AutoInvestOneTimeTransactionStatus>))]
    public enum AutoInvestOneTimeTransactionStatus
    {
        /// <summary>
        /// Success
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// Converting
        /// </summary>
        [Map("CONVERTING")]
        Converting
    }
}
