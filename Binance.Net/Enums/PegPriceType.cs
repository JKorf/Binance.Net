using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Peg price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PegPriceType>))]
    public enum PegPriceType
    {
        /// <summary>
        /// ["<c>PRIMARY_PEG</c>"] Primary peg
        /// </summary>
        [Map("PRIMARY_PEG")]
        PrimaryPeg,
        /// <summary>
        /// ["<c>MARKET_PEG</c>"] Market peg
        /// </summary>
        [Map("MARKET_PEG")]
        MarketPeg
    }
}

