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
        /// ["<c>NO_SIDE_EFFECT</c>"] Normal trade
        /// </summary>
        [Map("NO_SIDE_EFFECT")]
        NoSideEffect,
        /// <summary>
        /// ["<c>MARGIN_BUY</c>"] Margin trade order
        /// </summary>
        [Map("MARGIN_BUY")]
        MarginBuy,
        /// <summary>
        /// ["<c>AUTO_REPAY</c>"] Make auto repayment after order is filled
        /// </summary>
        [Map("AUTO_REPAY")]
        AutoRepay,
        /// <summary>
        /// ["<c>AUTO_BORROW_REPAY</c>"] Automatic borrowing and repayment, simultaneously
        /// </summary>
        [Map("AUTO_BORROW_REPAY")]
        AutoBorrowRepay,
    }
}

