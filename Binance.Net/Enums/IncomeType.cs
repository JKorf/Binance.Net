namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of futures income
    /// </summary>
    public enum IncomeType
    {
        /// <summary>
        /// Transfer into account
        /// </summary>
        Transfer,
        /// <summary>
        /// Futures welcome bonus
        /// </summary>
        WelcomeBonus,
        /// <summary>
        /// Futures realized profit
        /// </summary>
        RealizedPnl,
        /// <summary>
        /// Futures funding fee
        /// </summary>
        FundingFee,
        /// <summary>
        /// Futures trading commission
        /// </summary>
        Commission,
        /// <summary>
        /// Insurance clear
        /// </summary>
        InsuranceClear
    }
}
