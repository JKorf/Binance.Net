using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Travel rule status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TravelRuleStatus>))]
    public enum TravelRuleStatus
    {
        /// <summary>
        /// Not required or already provided
        /// </summary>
        [Map("0")]
        NotRequired,
        /// <summary>
        /// Travel rule info required
        /// </summary>
        [Map("1")]
        Required
    }
}
