using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Side effect for a margin order
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SideEffectType>))]
    public enum SideEffectType
    {
        /// <summary>
        /// Normal trade
        /// </summary>
        [Map("NO_SIDE_EFFECT")]
        NoSideEffect,
        /// <summary>
        /// Margin trade order
        /// </summary>
        [Map("MARGIN_BUY")]
        MarginBuy,
        /// <summary>
        /// Make auto repayment after order is filled
        /// </summary>
        [Map("AUTO_REPAY")]
        AutoRepay,
        /// <summary>
        /// Automatic borrowing and repayment, simultaneously
        /// </summary>
        [Map("AUTO_BORROW_REPAY")]
        AutoBorrowRepay,
    }
}
