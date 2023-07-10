using CryptoExchange.Net.Attributes;

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
        [Map("PENDING_TRADING")]
        PendingTrading,
        /// <summary>
        /// Trading
        /// </summary>
        [Map("TRADING")]
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
        [Map("CLOSE")]
        Close,
        /// <summary>
        /// Pre delivering
        /// </summary>
        [Map("PRE_DELIVERING")]
        PreDelivering,
        /// <summary>
        /// Delivering
        /// </summary>
        [Map("DELIVERING")]
        Delivering,
        /// <summary>
        /// Pre settle
        /// </summary>
        [Map("PRE_SETTLE")]
        PreSettle,
        /// <summary>
        /// Settings
        /// </summary>
        [Map("SETTLING")]
        Settling
    }
}
