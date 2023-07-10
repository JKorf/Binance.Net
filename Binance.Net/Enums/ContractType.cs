using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of contract
    /// </summary>
    public enum ContractType
    {
        /// <summary>
        /// Perpetual
        /// </summary>
        [Map("PERPETUAL")]
        Perpetual,
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
        CurrentQuarterDelivering,
        /// <summary>
        /// Next quarter
        /// </summary>
        [Map("NEXT_QUARTER")]
        NextQuarter,
        /// <summary>
        /// Next quarter delivering
        /// </summary>
        NextQuarterDelivering,
        /// <summary>
        /// Next month
        /// </summary>
        [Map("NEXT_MONTH")]
        NextMonth,
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown
    }
}
