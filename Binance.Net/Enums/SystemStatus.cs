using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of the Binance system
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<SystemStatus>))] public  enum SystemStatus
    {
        /// <summary>
        /// Operational
        /// </summary>
        [Map("0")]
        Normal,
        /// <summary>
        /// In maintenance
        /// </summary>
        [Map("1")]
        Maintenance
    }
}
