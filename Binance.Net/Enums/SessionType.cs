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
        /// ["<c>PRE_MARKET</c>"] Pre-market
        /// </summary>
        [Map("PRE_MARKET")]
        PreMarket,
        /// <summary>
        /// ["<c>REGULAR</c>"] Regular
        /// </summary>
        [Map("REGULAR")]
        Regular,
        /// <summary>
        /// ["<c>AFTER_MARKET</c>"] After market
        /// </summary>
        [Map("AFTER_MARKET")]
        AfterMarket,
        /// <summary>
        /// ["<c>OVERNIGHT</c>"] Overnight
        /// </summary>
        [Map("OVERNIGHT")]
        Overnight,
        /// <summary>
        /// ["<c>NO_TRADING</c>"] No trading
        /// </summary>
        [Map("NO_TRADING")]
        NoTrading

    }
}

