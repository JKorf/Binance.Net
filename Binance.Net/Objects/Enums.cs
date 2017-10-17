namespace Binance.Net.Objects
{
    public enum SymbolType
    {
        Spot
    }

    public enum OrderStatus
    {
        New,
        PartiallyFilled,
        Filled,
        Canceled,
        PendingCancel,
        Rejected,
        Expired
    }

    public enum OrderType
    {
        Limit,
        Market
    }

    public enum OrderSide
    {
        Buy,
        Sell
    }

    public enum TimeInForce
    {
        GoodTillCancel,
        ImmediateOrCancel
    }

    public enum ExecutionType
    {
        New,
        Canceled,
        Replaced,
        Rejected,
        Trade,
        Expired
    }

    public enum OrderRejectReason
    {
        None,
        UnknownInstrument,
        MarketClosed,
        PriceQuantityExceedsHardLimits,
        UnknownOrder,
        DuplicateOrder,
        UnknownAccount,
        InsufficientBalance,
        AccountInactive,
        AccountCannotSettle
    }

    public enum KlineInterval
    {
        OneMinute,
        ThreeMinutes,
        FiveMinutes,
        FiveteenMinutes,
        ThirtyMinutes,
        OneHour,
        TwoHour,
        FourHour,
        SixHour,
        EightHour,
        TwelfHour,
        OneDay,
        ThreeDay,
        OneWeek,
        OneMonth
    }    
}
