using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Lending type
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<LendingType>))] public  enum LendingType
    {
        /// <summary>
        /// Flexible
        /// </summary>
        [Map("DAILY")]
        Daily,
        /// <summary>
        /// Activity
        /// </summary>
        [Map("ACTIVITY")]
        Activity,
        /// <summary>
        /// Customized fixed
        /// </summary>
        [Map("CUSTOMIZED_FIXED")]
        CustomizedFixed
    }
}
