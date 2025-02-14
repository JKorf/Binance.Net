using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of working
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<WorkingType>))] public  enum WorkingType
    {
        /// <summary>
        /// Mark price type
        /// </summary>
        [Map("MARK_PRICE")]
        Mark,
        /// <summary>
        /// Contract price type
        /// </summary>
        [Map("CONTRACT_PRICE")]
        Contract
    }
}
