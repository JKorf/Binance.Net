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
        /// Success
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("FAILED")]
        Failed
    }
}
