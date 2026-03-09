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
        /// ["<c>SUCCESS</c>"] Success
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// ["<c>CONVERTING</c>"] Converting
        /// </summary>
        [Map("CONVERTING")]
        Converting
    }
}

