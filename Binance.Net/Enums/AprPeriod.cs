using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Apr period
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AprPeriod>))]
    public enum AprPeriod
    {
        /// <summary>
        /// Year
        /// </summary>
        [Map("YEAR")]
        Year,
        /// <summary>
        /// Day
        /// </summary>
        [Map("DAY")]
        Day
    }
}