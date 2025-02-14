using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of a symbol
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<SymbolStatus>))] public  enum SymbolStatus
    {
        /// <summary>
        /// Not trading yet
        /// </summary>
        [Map("PRE_TRADING")]
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
        [Map("POST_TRADING")]
        PostTrading,
        /// <summary>
        /// Not trading
        /// </summary>
        [Map("END_OF_DAY")]
        EndOfDay,
        /// <summary>
        /// Halted
        /// </summary>
        [Map("HALT")]
        Halt,
        /// <summary>
        /// 
        /// </summary>
        [Map("AUCTION_MATCH")]
        AuctionMatch,
        /// <summary>
        /// 
        /// </summary>
        [Map("BREAK")]
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
