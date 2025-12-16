using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The interval for the period
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PeriodInterval>))]
    public enum PeriodInterval
    {
        /// <summary>
        /// 5m
        /// </summary>
        [Map("5m")]
        FiveMinutes,
        /// <summary>
        /// 15m
        /// </summary>
        [Map("15m")]
        FifteenMinutes,
        /// <summary>
        /// 30m
        /// </summary>
        [Map("30m")]
        ThirtyMinutes,
        /// <summary>
        /// 1h
        /// </summary>
        [Map("1h")]
        OneHour,
        /// <summary>
        /// 2h
        /// </summary>
        [Map("2h")]
        TwoHour,
        /// <summary>
        /// 4h
        /// </summary>
        [Map("4h")]
        FourHour,
        /// <summary>
        /// 6h
        /// </summary>
        [Map("6h")]
        SixHour,
        /// <summary>
        /// 12h
        /// </summary>
        [Map("12h")]
        TwelveHour,
        /// <summary>
        /// 1d
        /// </summary>
        [Map("1d")]
        OneDay
    }
}
