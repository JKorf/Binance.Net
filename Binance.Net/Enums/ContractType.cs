using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of contract
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ContractType>))]
    public enum ContractType
    {
        /// <summary>
        /// Perpetual
        /// </summary>
        [Map("PERPETUAL")]
        Perpetual,
        /// <summary>
        /// Perpetual delivering
        /// </summary>
        [Map("PERPETUAL DELIVERING")]
        PerpetualDelivering,
        /// <summary>
        /// Current month
        /// </summary>
        [Map("CURRENT_MONTH")]
        CurrentMonth,
        /// <summary>
        /// Current quarter
        /// </summary>
        [Map("CURRENT_QUARTER")]
        CurrentQuarter,
        /// <summary>
        /// Current quarter delivering
        /// </summary>
        [Map("CURRENT_QUARTER DELIVERING")]
        CurrentQuarterDelivering,
        /// <summary>
        /// Next quarter
        /// </summary>
        [Map("NEXT_QUARTER")]
        NextQuarter,
        /// <summary>
        /// Next quarter delivering
        /// </summary>
        [Map("NEXT_QUARTER DELIVERING")]
        NextQuarterDelivering,
        /// <summary>
        /// Next month
        /// </summary>
        [Map("NEXT_MONTH")]
        NextMonth,
        /// <summary>
        /// Unknown
        /// </summary>
        [Map("")]
        Unknown,
        /// <summary>
        /// Traditional finance perp contract
        /// </summary>
        [Map("TRADIFI_PERPETUAL")]
        PerpetualTradFi
    }
}
