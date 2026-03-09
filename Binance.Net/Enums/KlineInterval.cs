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
        /// ["<c>1s</c>"] 1s
        /// </summary>
        [Map("1s")]
        OneSecond = 1,

        /// <summary>
        /// ["<c>1m</c>"] 1m
        /// </summary>
        [Map("1m")]
        OneMinute = 60,

        /// <summary>
        /// ["<c>3m</c>"] 3m
        /// </summary>
        [Map("3m")]
        ThreeMinutes = 60 * 3,

        /// <summary>
        /// ["<c>5m</c>"] 5m
        /// </summary>
        [Map("5m")]
        FiveMinutes = 60 * 5,

        /// <summary>
        /// ["<c>15m</c>"] 15m
        /// </summary>
        [Map("15m")]
        FifteenMinutes = 60 * 15,

        /// <summary>
        /// ["<c>30m</c>"] 30m
        /// </summary>
        [Map("30m")]
        ThirtyMinutes = 60 * 30,

        /// <summary>
        /// ["<c>1h</c>"] 1h
        /// </summary>
        [Map("1h")]
        OneHour = 60 * 60,

        /// <summary>
        /// ["<c>2h</c>"] 2h
        /// </summary>
        [Map("2h")]
        TwoHour = 60 * 60 * 2,

        /// <summary>
        /// ["<c>4h</c>"] 4h
        /// </summary>
        [Map("4h")]
        FourHour = 60 * 60 * 4,

        /// <summary>
        /// ["<c>6h</c>"] 6h
        /// </summary>
        [Map("6h")]
        SixHour = 60 * 60 * 6,

        /// <summary>
        /// ["<c>8h</c>"] 8h
        /// </summary>
        [Map("8h")]
        EightHour = 60 * 60 * 8,

        /// <summary>
        /// ["<c>12h</c>"] 12h
        /// </summary>
        [Map("12h")]
        TwelveHour = 60 * 60 * 12,

        /// <summary>
        /// ["<c>1d</c>"] 1d
        /// </summary>
        [Map("1d")]
        OneDay = 60 * 60 * 24,

        /// <summary>
        /// ["<c>3d</c>"] 3d
        /// </summary>
        [Map("3d")]
        ThreeDay = 60 * 60 * 24 * 3,

        /// <summary>
        /// ["<c>1w</c>"] 1w
        /// </summary>
        [Map("1w")]
        OneWeek = 60 * 60 * 24 * 7,

        /// <summary>
        /// ["<c>1M</c>"] 1M
        /// </summary>
        [Map("1M")]
        OneMonth = 60 * 60 * 24 * 30
    }
}

