using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Redemption status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AutoInvestRedemptionStatus>))]
    public enum AutoInvestRedemptionStatus
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
        Failed
    }
}

