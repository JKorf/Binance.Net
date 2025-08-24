using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Underlying Type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UnderlyingType>))]
    public enum UnderlyingType
    {
        /// <summary>
        /// Coin
        /// </summary>
        [Map("COIN")]
        Coin,
        /// <summary>
        /// Index
        /// </summary>
        [Map("INDEX")]
        Index,
        /// <summary>
        /// Pre-market
        /// </summary>
        [Map("PREMARKET")]
        PreMarket
    }
}
