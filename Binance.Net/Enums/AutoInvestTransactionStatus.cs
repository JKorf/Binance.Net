using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Transaction status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AutoInvestTransactionStatus>))]
    public enum AutoInvestTransactionStatus
    {
        /// <summary>
        /// ["<c>SUCCESS</c>"] Success
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// ["<c>FAILED</c>"] Failed
        /// </summary>
        [Map("FAILED")]
        Failed,
        /// <summary>
        /// ["<c>PENDING</c>"] Pending
        /// </summary>
        [Map("PENDING")]
        Pending
    }
}

