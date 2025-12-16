using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// ADL risk rate
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AdlRisk>))]
    public enum AdlRisk
    {
        /// <summary>
        /// High risk
        /// </summary>
        [Map("HIGH")]
        High,
        /// <summary>
        /// Medium risk
        /// </summary>
        [Map("MIDDLE")]
        Middle,
        /// <summary>
        /// Low risk
        /// </summary>
        [Map("LOW")]
        Low
    }
}
