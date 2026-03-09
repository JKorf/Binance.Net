using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The reason the order was rejected
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderRejectReason>))]
    public enum OrderRejectReason
    {
        /// <summary>
        /// ["<c>NONE</c>"] Not rejected
        /// </summary>
        [Map("NONE")]
        None,
        /// <summary>
        /// ["<c>UNKNOWN_INSTRUMENT</c>"] Unknown instrument
        /// </summary>
        [Map("UNKNOWN_INSTRUMENT")]
        UnknownInstrument,
        /// <summary>
        /// ["<c>MARKET_CLOSED</c>"] Closed market
        /// </summary>
        [Map("MARKET_CLOSED")]
        MarketClosed,
        /// <summary>
        /// ["<c>PRICE_QTY_EXCEED_HARD_LIMITS</c>"] Quantity out of bounds
        /// </summary>
        [Map("PRICE_QTY_EXCEED_HARD_LIMITS")]
        PriceQuantityExceedsHardLimits,
        /// <summary>
        /// ["<c>UNKNOWN_ORDER</c>"] Unknown order
        /// </summary>
        [Map("UNKNOWN_ORDER")]
        UnknownOrder,
        /// <summary>
        /// ["<c>DUPLICATE_ORDER</c>"] Duplicate
        /// </summary>
        [Map("DUPLICATE_ORDER")]
        DuplicateOrder,
        /// <summary>
        /// ["<c>UNKNOWN_ACCOUNT</c>"] Unknown account
        /// </summary>
        [Map("UNKNOWN_ACCOUNT")]
        UnknownAccount,
        /// <summary>
        /// Not enough balance
        /// </summary>
        [Map("INSUFFICIENT_BALANCE", "INSUFFICIENT_BALANCES")]
        InsufficientBalance,
        /// <summary>
        /// ["<c>ACCOUNT_INACTIVE</c>"] Account not active
        /// </summary>
        [Map("ACCOUNT_INACTIVE")]
        AccountInactive,
        /// <summary>
        /// ["<c>ACCOUNT_CANNOT_SETTLE</c>"] Cannot settle
        /// </summary>
        [Map("ACCOUNT_CANNOT_SETTLE")]
        AccountCannotSettle,
        /// <summary>
        /// ["<c>STOP_PRICE_WOULD_TRIGGER_IMMEDIATELY</c>"] Stop price would trigger immediately
        /// </summary>
        [Map("STOP_PRICE_WOULD_TRIGGER_IMMEDIATELY")]
        StopPriceWouldTrigger,
        /// <summary>
        /// ["<c>WOULD_MATCH_IMMEDIATELY</c>"] Trade would match immediately
        /// </summary>
        [Map("WOULD_MATCH_IMMEDIATELY")]
        WouldMatchImmediately,
        /// <summary>
        /// ["<c>OCO_BAD_PRICES</c>"] OCO order bad prices
        /// </summary>
        [Map("OCO_BAD_PRICES")]
        OCOOrderBadPrices
    }
}

