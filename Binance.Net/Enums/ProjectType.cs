using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The type of project
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<ProjectType>))] public  enum ProjectType
    {
        /// <summary>
        /// Regular
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
