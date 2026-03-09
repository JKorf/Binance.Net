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
        /// ["<c>HIGH</c>"] High risk
        /// </summary>
        [Map("HIGH")]
        High,
        /// <summary>
        /// ["<c>MIDDLE</c>"] Medium risk
        /// </summary>
        [Map("MIDDLE")]
        Middle,
        /// <summary>
        /// ["<c>LOW</c>"] Low risk
        /// </summary>
        [Map("LOW")]
        Low
    }
}

