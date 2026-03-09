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
        /// ["<c>YEAR</c>"] Year
        /// </summary>
        [Map("YEAR")]
        Year,
        /// <summary>
        /// ["<c>DAY</c>"] Day
        /// </summary>
        [Map("DAY")]
        Day
    }
}
