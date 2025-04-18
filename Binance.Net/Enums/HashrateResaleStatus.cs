using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Resale status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<HashrateResaleStatus>))]
    public enum HashrateResaleStatus
    {
        /// <summary>
        /// Processing
        /// </summary>
        [Map("0")]
        Processing,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("1")]
        Canceled,
        /// <summary>
        /// Terminated
        /// </summary>
        [Map("2")]
        Terminated
    }
}
