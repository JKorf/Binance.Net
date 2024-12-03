using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Order update
    /// </summary>
    public record BinanceFuturesStreamOrderUpdate: BinanceStreamEvent
    {
        /// <summary>
        /// Update data
        /// </summary>
        [JsonPropertyName("o")]
        public BinanceFuturesStreamOrderUpdateData UpdateData { get; set; } = new BinanceFuturesStreamOrderUpdateData();

        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// The listen key the update was for
        /// </summary>
        public string ListenKey { get; set; } = string.Empty;
    }

    /// <summary>
    /// Update data about an order
    /// </summary>
    public record BinanceFuturesStreamOrderUpdateData
    {
        /// <summary>
        /// The symbol the order is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The new client order id
        /// </summary>
        [JsonPropertyName("c")]
        [JsonConverterCtor<ReplaceConverter>(
            $"{BinanceExchange.ClientOrderIdPrefixSpot}->",
            $"{BinanceExchange.ClientOrderIdPrefixFutures}->")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonPropertyName("o")]
        public FuturesOrderType Type { get; set; }
        /// <summary>
        /// The timespan the order is active
        /// </summary>
        [JsonPropertyName("f")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// The average price of the order
        /// </summary>
        [JsonPropertyName("ap")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// The stop price of the order
        /// </summary>
        [JsonPropertyName("sp")]
        public decimal StopPrice { get; set; }
        /// <summary>
        /// The execution type
        /// </summary>
        [JsonPropertyName("x")]
        public ExecutionType ExecutionType { get; set; }
        /// <summary>
        /// The status of the order
        /// </summary>
        [JsonPropertyName("X")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// The id of the order as assigned by Binance
        /// </summary>
        [JsonPropertyName("i")]
        public long OrderId { get; set; }
        /// <summary>
        /// The quantity of the last filled trade of this order
        /// </summary>
        [JsonPropertyName("l")]
        public decimal QuantityOfLastFilledTrade { get; set; }
        /// <summary>
        /// The quantity of all trades that were filled for this order
        /// </summary>
        [JsonPropertyName("z")]
        public decimal AccumulatedQuantityOfFilledTrades { get; set; }
        /// <summary>
        /// The price of the last filled trade
        /// </summary>
        [JsonPropertyName("L")]
        public decimal PriceLastFilledTrade { get; set; }
        /// <summary>
        /// The fee payed
        /// </summary>
        [JsonPropertyName("n")]
        public decimal Fee { get; set; }
        /// <summary>
        /// The asset the fee was taken from
        /// </summary>
        [JsonPropertyName("N")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// The time of the update
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// The trade id
        /// </summary>
        [JsonPropertyName("t")]
        public long TradeId { get; set; }
        /// <summary>
        /// Bid Notional
        /// </summary>
        [JsonPropertyName("b")]
        public decimal BidNotional { get; set; }
        /// <summary>
        /// Ask Notional
        /// </summary>
        [JsonPropertyName("a")]
        public decimal AskNotional { get; set; }
        /// <summary>
        /// Whether the buyer is the maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool BuyerIsMaker { get; set; }
        /// <summary>
        /// Is this reduce only
        /// </summary>
        [JsonPropertyName("R")]
        public bool IsReduce { get; set; }
        /// <summary>
        /// Stop price working type
        /// </summary>
        [JsonPropertyName("wt")]
        public WorkingType StopPriceWorking { get; set; }
        /// <summary>
        /// Original Order Type
        /// </summary>
        [JsonPropertyName("ot")]
        public FuturesOrderType OriginalType { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("ps")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// If Close-All, only pushed with conditional order
        /// </summary>
        [JsonPropertyName("cp")]
        public bool PushedConditionalOrder { get; set; }
        /// <summary>
        /// Activation Price, only pushed with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonPropertyName("AP")]
        public decimal ActivationPrice { get; set; }
        /// <summary>
        /// Callback Rate, only pushed with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonPropertyName("cr")]
        public decimal CallbackRate { get; set; }
        /// <summary>
        /// Realized profit of the trade
        /// </summary>
        [JsonPropertyName("rp")]
        public decimal RealizedProfit { get; set; }
        /// <summary>
        /// Is price protection enable
        /// </summary>
        [JsonPropertyName("pP")]
        public bool PriceProtection { get; set; }
        /// <summary>
        /// Self trade prevention mode
        /// </summary>
        [JsonPropertyName("V")]
        public SelfTradePreventionMode SelfTradePrevention { get; set; }
        /// <summary>
        /// Price match mode
        /// </summary>
        [JsonPropertyName("pm")]
        public PriceMatch PriceMatchMode { get; set; }
        /// <summary>
        /// The GoodTillDate if GTD time in force
        /// </summary>
        [JsonPropertyName("gtd")]
        public DateTime? GoodTillDate { get; set; }
    }
}
