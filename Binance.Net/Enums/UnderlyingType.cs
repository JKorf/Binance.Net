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
        /// ["<c>COIN</c>"] Coin
        /// </summary>
        [Map("COIN")]
        Coin,
        /// <summary>
        /// ["<c>INDEX</c>"] Index
        /// </summary>
        [Map("INDEX")]
        Index,
        /// <summary>
        /// ["<c>PREMARKET</c>"] Pre-market
        /// </summary>
        [Map("PREMARKET")]
        PreMarket,
        /// <summary>
        /// ["<c>COMMODITY</c>"] Commodity
        /// </summary>
        [Map("COMMODITY")]
        Commodity,
        /// <summary>
        /// ["<c>EQUITY</c>"] Equity
        /// </summary>
        [Map("EQUITY")]
        Equity
    }
}

