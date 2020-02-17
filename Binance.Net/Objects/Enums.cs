using Binance.Net.Converters;
using Newtonsoft.Json;

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
        Break
    }

//TODO
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
        /// <summary>
        /// Stop loss order. Will execute a market order when the price drops below a price to sell and therefor limit the loss
        /// </summary>
        StopLoss,
        /// <summary>
        /// Stop loss order. Will execute a limit order when the price drops below a price to sell and therefor limit the loss
        /// </summary>
        StopLossLimit,
        /// <summary>
        /// Take profit order. Will execute a market order when the price rises above a price to sell and therefor take a profit
        /// </summary>
        TakeProfit,
        /// <summary>
        /// Take profit order. Will execute a limit order when the price rises above a price to sell and therefor take a profit
        /// </summary>
        TakeProfitLimit,
        /// <summary>
        /// Same as a limit order, however it will fail if the order would immediately match, therefor preventing taker orders
        /// </summary>
        LimitMaker
    }

    /// <summary>
    /// The side of an order
    /// </summary>
    public enum OrderSide
    {
        /// <summary>
        /// Buy
        /// </summary>
        Buy,
        /// <summary>
        /// Sell
        /// </summary>
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
        /// ImmediateOrCancel orders have to be at least partially filled upon placing or will be automatically canceled
        /// </summary>
        ImmediateOrCancel,
        /// <summary>
        /// FillOrKill orders have to be entirely filled upon placing or will be automatically canceled
        /// </summary>
        FillOrKill
    }

    /// <summary>
    /// The type of execution
    /// </summary>
    public enum ExecutionType
    {
        /// <summary>
        /// New
        /// </summary>
        New,
        /// <summary>
        /// Canceled
        /// </summary>
        Canceled,
        /// <summary>
        /// Replaced
        /// </summary>
        Replaced,
        /// <summary>
        /// Rejected
        /// </summary>
        Rejected,
        /// <summary>
        /// Trade
        /// </summary>
        Trade,
        /// <summary>
        /// Expired
        /// </summary>
        Expired
    }

    /// <summary>
    /// The reason the order was rejected
    /// </summary>
    public enum OrderRejectReason
    {
        /// <summary>
        /// Not rejected
        /// </summary>
        None,
        /// <summary>
        /// Unknown instrument
        /// </summary>
        UnknownInstrument,
        /// <summary>
        /// Closed market
        /// </summary>
        MarketClosed,
        /// <summary>
        /// Quantity out of bounds
        /// </summary>
        PriceQuantityExceedsHardLimits,
        /// <summary>
        /// Unknown order
        /// </summary>
        UnknownOrder,
        /// <summary>
        /// Duplicate
        /// </summary>
        DuplicateOrder,
        /// <summary>
        /// Unkown account
        /// </summary>
        UnknownAccount,
        /// <summary>
        /// Not enough balance
        /// </summary>
        InsufficientBalance,
        /// <summary>
        /// Account not active
        /// </summary>
        AccountInactive,
        /// <summary>
        /// Cannot settle
        /// </summary>
        AccountCannotSettle
    }

    /// <summary>
    /// The interval for the kline
    /// </summary>
    public enum KlineInterval
    {
        /// <summary>
        /// 1m
        /// </summary>
        OneMinute,
        /// <summary>
        /// 3m
        /// </summary>
        ThreeMinutes,
        /// <summary>
        /// 5m
        /// </summary>
        FiveMinutes,
        /// <summary>
        /// 15m
        /// </summary>
        FifteenMinutes,
        /// <summary>
        /// 30m
        /// </summary>
        ThirtyMinutes,
        /// <summary>
        /// 1h
        /// </summary>
        OneHour,
        /// <summary>
        /// 2h
        /// </summary>
        TwoHour,
        /// <summary>
        /// 4h
        /// </summary>
        FourHour,
        /// <summary>
        /// 6h
        /// </summary>
        SixHour,
        /// <summary>
        /// 8h
        /// </summary>
        EightHour,
        /// <summary>
        /// 12h
        /// </summary>
        TwelveHour,
        /// <summary>
        /// 1d
        /// </summary>
        OneDay,
        /// <summary>
        /// 3d
        /// </summary>
        ThreeDay,
        /// <summary>
        /// 1w
        /// </summary>
        OneWeek,
        /// <summary>
        /// 1M
        /// </summary>
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
        /// <summary>
        /// Pending
        /// </summary>
        Pending,
        /// <summary>
        /// Success
        /// </summary>
        Success,
        /// <summary>
        /// Completed
        /// </summary>
        Completed
    }

    /// <summary>
    /// Rate limit on what unit
    /// </summary>
    public enum RateLimitInterval
    {
        /// <summary>
        /// Seconds
        /// </summary>
        Second,
        /// <summary>
        /// Minutes
        /// </summary>
        Minute,
        /// <summary>
        /// Days
        /// </summary>
        Day
    }

    /// <summary>
    /// Type of rate limit
    /// </summary>
    public enum RateLimitType
    {
        /// <summary>
        /// Request weight
        /// </summary>
        RequestWeight,
        /// <summary>
        /// Order amount
        /// </summary>
        Orders,
        /// <summary>
        /// Raw requests
        /// </summary>
        RawRequests
    }

    /// <summary>
    /// Filter type
    /// </summary>
    public enum SymbolFilterType
    {
        /// <summary>
        /// Unknown filter type
        /// </summary>
        Unknown,
        /// <summary>
        /// Price filter
        /// </summary>
        Price,
        /// <summary>
        /// Price percent filter
        /// </summary>
        PricePercent,
        /// <summary>
        /// Lot size filter
        /// </summary>
        LotSize,
        /// <summary>
        /// Market lot size filter
        /// </summary>
        MarketLotSize,
        /// <summary>
        /// Min notional filter
        /// </summary>
        MinNotional,
        /// <summary>
        /// Max orders filter
        /// </summary>
        MaxNumberOrders,
        /// <summary>
        /// Max iceberg orders filter
        /// </summary>
        MaxNumberIcebergOrders,
        /// <summary>
        /// Max algo orders filter
        /// </summary>
        MaxNumberAlgorithmicOrders,
        /// <summary>
        /// Max iceberg parts filter
        /// </summary>
        IcebergParts
    }

    /// <summary>
    /// Response type
    /// </summary>
    public enum OrderResponseType
    {
        /// <summary>
        /// Ack only
        /// </summary>
        Acknowledge,
        /// <summary>
        /// Resulting order
        /// </summary>
        Result,
        /// <summary>
        /// Full order info
        /// </summary>
        Full
    }

    /// <summary>
    /// Margin Order Side Effect type
    /// </summary>
    public enum SideEffectType
    {
        /// <summary>
        /// No side effect
        /// </summary>
        NoSideEffect,
        /// <summary>
        /// Place Margin Order
        /// </summary>
        MarginBuy,
        /// <summary>
        /// Automatically repay borrowed amount
        /// </summary>
        AutoRepay
    }

    /// <summary>
    /// Trade rules behaviour
    /// </summary>
    public enum TradeRulesBehaviour
    {
        /// <summary>
        /// None
        /// </summary>
        None,
        /// <summary>
        /// Throw an error if not complying
        /// </summary>
        ThrowError,
        /// <summary>
        /// Auto adjust order when not complying
        /// </summary>
        AutoComply
    }

    /// <summary>
    /// Status of the Binance system
    /// </summary>
    public enum SystemStatus
    {
        /// <summary>
        /// Operational
        /// </summary>
        Normal,
        /// <summary>
        /// In maintenance
        /// </summary>
        Maintenance
    }

    /// <summary>
    /// Status of the sub account
    /// </summary>
    public enum SubAccountStatus
    {
        /// <summary>
        /// Enabled
        /// </summary>
        Enabled,
        /// <summary>
        /// Disabled
        /// </summary>
        Disabled
    }

    /// <summary>
    /// Transfer direction
    /// </summary>
    public enum TransferDirectionType
    {
        /// <summary>
        /// From main account to margin account
        /// </summary>
        MainToMargin,
        /// <summary>
        /// From margin account to main account
        /// </summary>
        MarginToMain
    }

    /// <summary>
    /// Status of a margin action
    /// </summary>
    public enum MarginStatus
    {
        /// <summary>
        /// Pending to execution
        /// </summary>
        Pending,
        /// <summary>
        /// Executed, waiting to be confirmed
        /// </summary>
        Completed,
        /// <summary>
        /// Successfully loaned/repayed
        /// </summary>
        Confirmed,
        /// <summary>
        /// execution failed, nothing happened to your account
        /// </summary>
        Failed
    }

    /// <summary>
    /// List status type
    /// </summary>
    public enum ListStatusType
    {
        /// <summary>
        /// Failed action
        /// </summary>
        Response,
        /// <summary>
        /// Placed
        /// </summary>
        ExecutionStarted,
        /// <summary>
        /// Order list is done
        /// </summary>
        Done
    }

    /// <summary>
    /// List order status
    /// </summary>
    public enum ListOrderStatus
    {
        /// <summary>
        /// Executing
        /// </summary>
        Executing,
        /// <summary>
        /// Executed
        /// </summary>
        Done,
        /// <summary>
        /// Rejected
        /// </summary>
        Rejected
    }

    /// <summary>
    /// Types of indicators
    /// </summary>
    [JsonConverter(typeof(IndicatorTypeConverter))]
    public enum IndicatorType
    {
        /// <summary>
        /// Unfilled ratio
        /// </summary>
        UnfilledRatio,
        /// <summary>
        /// Expired orders ratio
        /// </summary>
        ExpirationRatio,
        /// <summary>
        /// Cancelled orders ratio
        /// </summary>
        CancellationRatio
    }

    /// <summary>
    /// Direction of a transfer
    /// </summary>
    public enum TransferDirection
    {
        /// <summary>
        /// Roll-in
        /// </summary>
        RollIn,
        /// <summary>
        /// Roll-out
        /// </summary>
        RollOut
    }
}
