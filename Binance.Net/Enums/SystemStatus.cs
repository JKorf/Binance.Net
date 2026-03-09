using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of the Binance system
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SystemStatus>))]
    public enum SystemStatus
    {
        /// <summary>
        /// ["<c>0</c>"] Operational
        /// </summary>
        [Map("0")]
        Normal,
        /// <summary>
        /// ["<c>1</c>"] In maintenance
        /// </summary>
        [Map("1")]
        Maintenance
    }
}

