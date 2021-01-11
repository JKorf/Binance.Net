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
        Perpetual,
        /// <summary>
        /// Current month
        /// </summary>
        CurrentMonth,
        /// <summary>
        /// Current quarter
        /// </summary>
        CurrentQuarter,
        /// <summary>
        /// Current quarter delivering
        /// </summary>
        CurrentQuarterDelivering,
        /// <summary>
        /// Next quarter
        /// </summary>
        NextQuarter,
        /// <summary>
        /// Next quarter delivering
        /// </summary>
        NextQuarterDelivering,
        /// <summary>
        /// Next month
        /// </summary>
        NextMonth,
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown
    }
}
