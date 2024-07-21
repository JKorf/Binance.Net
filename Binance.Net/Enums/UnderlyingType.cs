using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Underlying Type
    /// </summary>
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
        Index
    }
}
