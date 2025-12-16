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
        /// Primary peg
        /// </summary>
        [Map("PRIMARY_PEG")]
        PrimaryPeg,
        /// <summary>
        /// Market peg
        /// </summary>
        [Map("MARKET_PEG")]
        MarketPeg
    }
}
