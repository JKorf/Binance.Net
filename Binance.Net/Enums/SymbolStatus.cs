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
        /// 
        /// </summary>
        Close
    }
}
