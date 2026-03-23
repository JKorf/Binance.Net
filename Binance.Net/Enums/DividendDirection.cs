using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Dividend direction
    /// </summary>
    [JsonConverter(typeof(EnumConverter<DividendDirection>))]
    public enum DividendDirection
    {
        /// <summary>
        /// In flow
        /// </summary>
        [Map("1")]
        In,
        /// <summary>
        /// Out flow
        /// </summary>
        [Map("-1")]
        Out
    }
}
