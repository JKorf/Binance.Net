using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// TradFi session type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SessionType>))]
    public enum SessionType
    {
        /// <summary>
        /// Pre-market
        /// </summary>
        [Map("PRE_MARKET")]
        PreMarket,
        /// <summary>
        /// Regular
        /// </summary>
        [Map("REGULAR")]
        Regular,
        /// <summary>
        /// After market
        /// </summary>
        [Map("AFTER_MARKET")]
        AfterMarket,
        /// <summary>
        /// Overnight
        /// </summary>
        [Map("OVERNIGHT")]
        Overnight,
        /// <summary>
        /// No trading
        /// </summary>
        [Map("NO_TRADING")]
        NoTrading

    }
}
