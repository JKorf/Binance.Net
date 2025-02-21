using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Rate direction
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AdjustRateDirection>))] public  enum AdjustRateDirection
    {
        /// <summary>
        /// Additional
        /// </summary>
        [Map("ADDITIONAL")]
        Additional,
        /// <summary>
        /// Reduced
        /// </summary>
        [Map("REDUCED")]
        Reduced
    }
}
