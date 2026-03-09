using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Rate direction
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AdjustRateDirection>))]
    public enum AdjustRateDirection
    {
        /// <summary>
        /// ["<c>ADDITIONAL</c>"] Additional
        /// </summary>
        [Map("ADDITIONAL")]
        Additional,
        /// <summary>
        /// ["<c>REDUCED</c>"] Reduced
        /// </summary>
        [Map("REDUCED")]
        Reduced
    }
}

