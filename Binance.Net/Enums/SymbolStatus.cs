namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of a symbol
    /// </summary>
    public enum SymbolStatus
    {
        /// <summary>
        /// Not trading yet
        /// </summary>
        PreTrading,
        /// <summary>
        /// Pending trading
        /// </summary>
        PendingTrading,
        /// <summary>
        /// Trading
        /// </summary>
        Trading,
        /// <summary>
        /// No longer trading
        /// </summary>
        PostTrading,
        /// <summary>
        /// Not trading
        /// </summary>
        EndOfDay,
        /// <summary>
        /// Halted
        /// </summary>
        Halt,
        /// <summary>
        /// 
        /// </summary>
        AuctionMatch,
        /// <summary>
        /// 
        /// </summary>
        Break,
        /// <summary>
        /// Closed
        /// </summary>
        Close,
        /// <summary>
        /// Pre delivering
        /// </summary>
        PreDelivering,
        /// <summary>
        /// Delivering
        /// </summary>
        Delivering,
        /// <summary>
        /// Pre settle
        /// </summary>
        PreSettle,
        /// <summary>
        /// Settings
        /// </summary>
        Settling
    }
}
