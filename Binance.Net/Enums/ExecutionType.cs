namespace Binance.Net.Enums
{
    /// <summary>
    /// The type of execution
    /// </summary>
    public enum ExecutionType
    {
        /// <summary>
        /// New
        /// </summary>
        New,
        /// <summary>
        /// Canceled
        /// </summary>
        Canceled,
        /// <summary>
        /// Replaced
        /// </summary>
        Replaced,
        /// <summary>
        /// Rejected
        /// </summary>
        Rejected,
        /// <summary>
        /// Trade
        /// </summary>
        Trade,
        /// <summary>
        /// Expired
        /// </summary>
        Expired,
        /// <summary>
        /// Amendment
        /// </summary>
        Amendment,
        /// <summary>
        /// Self trade prevented
        /// </summary>
        TradePrevention
    }
}
