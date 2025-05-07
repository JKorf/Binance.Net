using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Valid Time
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ValidTime>))]
    public enum ValidTime
    {
        /// <summary>
        /// 10 seconds
        /// </summary>
        [Map("0")]
        TenSeconds,
        /// <summary>
        /// 30 seconds
        /// </summary>
        [Map("1")]
        ThirtySeconds,
        /// <summary>
        /// 1 minute
        /// </summary>
        [Map("2")]
        OneMinute,
        /// <summary>
        /// 2 minutes
        /// </summary>
        [Map("3")]
        TwoMinutes
    }
}
