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
        /// ["<c>0</c>"] Not required or already provided
        /// </summary>
        [Map("0")]
        NotRequired,
        /// <summary>
        /// ["<c>1</c>"] Travel rule info required
        /// </summary>
        [Map("1")]
        Required
    }
}

