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
        /// ["<c>0</c>"] 10 seconds
        /// </summary>
        [Map("0")]
        TenSeconds,
        /// <summary>
        /// ["<c>1</c>"] 30 seconds
        /// </summary>
        [Map("1")]
        ThirtySeconds,
        /// <summary>
        /// ["<c>2</c>"] 1 minute
        /// </summary>
        [Map("2")]
        OneMinute,
        /// <summary>
        /// ["<c>3</c>"] 2 minutes
        /// </summary>
        [Map("3")]
        TwoMinutes
    }
}

