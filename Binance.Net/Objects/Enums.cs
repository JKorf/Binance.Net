namespace Binance.Net.Objects
{
    /// <summary>
    /// The status of an order
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Order is new
        /// </summary>
        New,
        /// <summary>
        /// Order is partly filled, still has quantity left to fill
        /// </summary>
        PartiallyFilled,
        /// <summary>
        /// The order has been filled and completed
        /// </summary>
        Filled,
        /// <summary>
        /// The order has been canceled
        /// </summary>
        Canceled,
        /// <summary>
        /// The order is in the process of being canceled
        /// </summary>
        PendingCancel,
        /// <summary>
        /// The order has been rejected
        /// </summary>
        Rejected,
        /// <summary>
        /// The order has expired
        /// </summary>
        Expired
    }

    public enum SymbolStatus
    {
        PreTrading,
        Trading,
        PostTrading,
        EndOfDay,
        Halt,
        AuctionMatch,
        Break
    }

    /// <summary>
    /// The type of an order
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// Limit orders will be placed at a specific price. If the price isn't available in the order book for that asset the order will be added in the order book for someone to fill.
        /// </summary>
        Limit,
        /// <summary>
        /// Market order will be placed without a price. The order will be executed at the best price available at that time in the order book.
        /// </summary>
        Market,
        StopLoss,
        StopLossLimit,
        TakeProfit,
        TakeProfitLimit,
        LimitMaker
    }

    /// <summary>
    /// The side of an order
    /// </summary>
    public enum OrderSide
    {
        Buy,
        Sell
    }

    /// <summary>
    /// The time the order will be active for
    /// </summary>
    public enum TimeInForce
    {
        /// <summary>
        /// GoodTillCancel orders will stay active until they are filled or canceled
        /// </summary>
        GoodTillCancel,
        /// <summary>
        /// ImmediateOrCancel orders have to be filled upon placing or will be automatically canceled
        /// </summary>
        ImmediateOrCancel,
        FillOrKill
    }

    /// <summary>
    /// The type of execution
    /// </summary>
    public enum ExecutionType
    {
        New,
        Canceled,
        Replaced,
        Rejected,
        Trade,
        Expired
    }

    /// <summary>
    /// The reason the order was rejected
    /// </summary>
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

    /// <summary>
    /// The interval for the kline
    /// </summary>
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
        TwelveHour,
        OneDay,
        ThreeDay,
        OneWeek,
        OneMonth
    }    

    /// <summary>
    /// The status of a withdrawal
    /// </summary>
    public enum WithdrawalStatus
    {
        /// <summary>
        /// Email has been send
        /// </summary>
        EmailSend,
        /// <summary>
        /// Withdrawal has been canceled
        /// </summary>
        Canceled,
        /// <summary>
        /// Withdrawal is awaiting approval
        /// </summary>
        AwaitingApproval,
        /// <summary>
        /// Withdrawal has been rejected
        /// </summary>
        Rejected,
        /// <summary>
        /// Withdrawal is processing
        /// </summary>
        Processing,
        /// <summary>
        /// Withdrawal has failed
        /// </summary>
        Failure,
        /// <summary>
        /// Withdrawal has been processed
        /// </summary>
        Completed
    }

    /// <summary>
    /// The status of a deposit
    /// </summary>
    public enum DepositStatus
    {
        Pending,
        Success
    }

    public enum RateLimitInterval
    {
        Second,
        Minute,
        Day
    }

    public enum RateLimitType
    {
        RequestWeight,
        Orders,
        RawRequests
    }

    public enum SymbolFilterType
    {
        Unknown,
        Price,
        PricePercent,
        LotSize,
        MarketLotSize,
        MinNotional,
        MaxNumberOrders,
        MaxNumberIcebergOrders,
        MaxNumberAlgorithmicOrders,
        IcebergParts
    }

    public enum OrderResponseType
    {
        Acknowledge,
        Result,
        Full
    }

    public enum TradeRulesBehaviour
    {
        None,
        ThrowError,
        AutoComply
    }
    
    public enum SystemStatus
    {
        Normal,
        Maintenance
    }

    public enum SubAccountStatus
    {
        Enabled,
        Disabled
    }
}
