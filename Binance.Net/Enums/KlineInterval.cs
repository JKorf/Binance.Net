using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The interval for the kline, the int value represents the time in seconds
    /// </summary>
    [JsonConverter(typeof(EnumConverter<KlineInterval>))]
    public enum KlineInterval
    {
        /// <summary>
        /// 1s
        /// </summary>
        [Map("1s")]
        OneSecond = 1,

        /// <summary>
        /// 1m
        /// </summary>
        [Map("1m")]
        OneMinute = 60,

        /// <summary>
        /// 3m
        /// </summary>
        [Map("3m")]
        ThreeMinutes = 60 * 3,

        /// <summary>
        /// 5m
        /// </summary>
        [Map("5m")]
        FiveMinutes = 60 * 5,

        /// <summary>
        /// 15m
        /// </summary>
        [Map("15m")]
        FifteenMinutes = 60 * 15,

        /// <summary>
        /// 30m
        /// </summary>
        [Map("30m")]
        ThirtyMinutes = 60 * 30,

        /// <summary>
        /// 1h
        /// </summary>
        [Map("1h")]
        OneHour = 60 * 60,

        /// <summary>
        /// 2h
        /// </summary>
        [Map("2h")]
        TwoHour = 60 * 60 * 2,

        /// <summary>
        /// 4h
        /// </summary>
        [Map("4h")]
        FourHour = 60 * 60 * 4,

        /// <summary>
        /// 6h
        /// </summary>
        [Map("6h")]
        SixHour = 60 * 60 * 6,

        /// <summary>
        /// 8h
        /// </summary>
        [Map("8h")]
        EightHour = 60 * 60 * 8,

        /// <summary>
        /// 12h
        /// </summary>
        [Map("12h")]
        TwelveHour = 60 * 60 * 12,

        /// <summary>
        /// 1d
        /// </summary>
        [Map("1d")]
        OneDay = 60 * 60 * 24,

        /// <summary>
        /// 3d
        /// </summary>
        [Map("3d")]
        ThreeDay = 60 * 60 * 24 * 3,

        /// <summary>
        /// 1w
        /// </summary>
        [Map("1w")]
        OneWeek = 60 * 60 * 24 * 7,

        /// <summary>
        /// 1M
        /// </summary>
        [Map("1M")]
        OneMonth = 60 * 60 * 24 * 30
    }
}
