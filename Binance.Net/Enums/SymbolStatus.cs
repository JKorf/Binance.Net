using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of a symbol
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SymbolStatus>))]
    public enum SymbolStatus
    {
        /// <summary>
        /// ["<c>PRE_TRADING</c>"] Not trading yet
        /// </summary>
        [Map("PRE_TRADING")]
        PreTrading,
        /// <summary>
        /// ["<c>PENDING_TRADING</c>"] Pending trading
        /// </summary>
        [Map("PENDING_TRADING")]
        PendingTrading,
        /// <summary>
        /// ["<c>TRADING</c>"] Trading
        /// </summary>
        [Map("TRADING")]
        Trading,
        /// <summary>
        /// ["<c>POST_TRADING</c>"] No longer trading
        /// </summary>
        [Map("POST_TRADING")]
        PostTrading,
        /// <summary>
        /// ["<c>END_OF_DAY</c>"] Not trading
        /// </summary>
        [Map("END_OF_DAY")]
        EndOfDay,
        /// <summary>
        /// ["<c>HALT</c>"] Halted
        /// </summary>
        [Map("HALT")]
        Halt,
        /// <summary>
        /// ["<c>AUCTION_MATCH</c>"] 
        /// </summary>
        [Map("AUCTION_MATCH")]
        AuctionMatch,
        /// <summary>
        /// ["<c>BREAK</c>"] 
        /// </summary>
        [Map("BREAK")]
        Break,
        /// <summary>
        /// ["<c>CLOSE</c>"] Closed
        /// </summary>
        [Map("CLOSE")]
        Close,
        /// <summary>
        /// ["<c>PRE_DELIVERING</c>"] Pre delivering
        /// </summary>
        [Map("PRE_DELIVERING")]
        PreDelivering,
        /// <summary>
        /// ["<c>DELIVERING</c>"] Delivering
        /// </summary>
        [Map("DELIVERING")]
        Delivering,
        /// <summary>
        /// ["<c>PRE_SETTLE</c>"] Pre settle
        /// </summary>
        [Map("PRE_SETTLE")]
        PreSettle,
        /// <summary>
        /// ["<c>SETTLING</c>"] Settings
        /// </summary>
        [Map("SETTLING")]
        Settling
    }
}

