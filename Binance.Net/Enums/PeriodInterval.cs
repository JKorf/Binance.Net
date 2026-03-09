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
        /// ["<c>5m</c>"] 5m
        /// </summary>
        [Map("5m")]
        FiveMinutes,
        /// <summary>
        /// ["<c>15m</c>"] 15m
        /// </summary>
        [Map("15m")]
        FifteenMinutes,
        /// <summary>
        /// ["<c>30m</c>"] 30m
        /// </summary>
        [Map("30m")]
        ThirtyMinutes,
        /// <summary>
        /// ["<c>1h</c>"] 1h
        /// </summary>
        [Map("1h")]
        OneHour,
        /// <summary>
        /// ["<c>2h</c>"] 2h
        /// </summary>
        [Map("2h")]
        TwoHour,
        /// <summary>
        /// ["<c>4h</c>"] 4h
        /// </summary>
        [Map("4h")]
        FourHour,
        /// <summary>
        /// ["<c>6h</c>"] 6h
        /// </summary>
        [Map("6h")]
        SixHour,
        /// <summary>
        /// ["<c>12h</c>"] 12h
        /// </summary>
        [Map("12h")]
        TwelveHour,
        /// <summary>
        /// ["<c>1d</c>"] 1d
        /// </summary>
        [Map("1d")]
        OneDay
    }
}

