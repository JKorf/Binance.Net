using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of symbol
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SymbolType>))]
    public enum SymbolType
    {
        /// <summary>
        /// ["<c>1</c>"] USD margined contract
        /// </summary>
        [Map("1")]
        UsdMargin,
        /// <summary>
        /// ["<c>2</c>"] Coin Margined contract
        /// </summary>>
        [Map("2")]
        CoinMargin
    }
}

