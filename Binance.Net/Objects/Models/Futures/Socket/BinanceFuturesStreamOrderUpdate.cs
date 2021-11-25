using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Order update
    /// </summary>
    public class BinanceFuturesStreamOrderUpdate: BinanceStreamEvent
    {
        /// <summary>
        /// Update data
        /// </summary>
        [JsonProperty("o")]
        public BinanceFuturesStreamOrderUpdateData UpdateData { get; set; } = new BinanceFuturesStreamOrderUpdateData();

        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTime { get; set; }
    }

    /// <summary>
    /// Update data about an order
    /// </summary>
    public class BinanceFuturesStreamOrderUpdateData
    {
        /// <summary>
        /// The symbol the order is for
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The new client order id
        /// </summary>
        [JsonProperty("c")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonProperty("S"), JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonProperty("o"), JsonConverter(typeof(OrderTypeConverter))]
        public OrderType Type { get; set; }
        /// <summary>
        /// The timespan the order is active
        /// </summary>
        [JsonProperty("f"), JsonConverter(typeof(TimeInForceConverter))]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonProperty("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonProperty("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// The average price of the order
        /// </summary>
        [JsonProperty("ap")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// The stop price of the order
        /// </summary>
        [JsonProperty("sp")]
        public decimal StopPrice { get; set; }
        /// <summary>
        /// The execution type
        /// </summary>
        [JsonProperty("x"), JsonConverter(typeof(ExecutionTypeConverter))]
        public ExecutionType ExecutionType { get; set; }
        /// <summary>
        /// The status of the order
        /// </summary>
        [JsonProperty("X"), JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// The id of the order as assigned by Binance
        /// </summary>
        [JsonProperty("i")]
        public long OrderId { get; set; }
        /// <summary>
        /// The quantity of the last filled trade of this order
        /// </summary>
        [JsonProperty("l")]
        public decimal QuantityOfLastFilledTrade { get; set; }
        /// <summary>
        /// The quantity of all trades that were filled for this order
        /// </summary>
        [JsonProperty("z")]
        public decimal AccumulatedQuantityOfFilledTrades { get; set; }
        /// <summary>
        /// The price of the last filled trade
        /// </summary>
        [JsonProperty("L")]
        public decimal PriceLastFilledTrade { get; set; }
        /// <summary>
        /// The fee payed
        /// </summary>
        [JsonProperty("n")]
        public decimal Fee { get; set; }
        /// <summary>
        /// The asset the fee was taken from
        /// </summary>
        [JsonProperty("N")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// The time of the update
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// The trade id
        /// </summary>
        [JsonProperty("t")]
        public long TradeId { get; set; }
        /// <summary>
        /// Bid Notional
        /// </summary>
        [JsonProperty("b")]
        public decimal BidNotional { get; set; }
        /// <summary>
        /// Ask Notional
        /// </summary>
        [JsonProperty("a")]
        public decimal AskNotional { get; set; }
        /// <summary>
        /// Whether the buyer is the maker
        /// </summary>
        [JsonProperty("m")]
        public bool BuyerIsMaker { get; set; }
        /// <summary>
        /// Is this reduce only
        /// </summary>
        [JsonProperty("R")]
        public bool IsReduce { get; set; }
        /// <summary>
        /// Stop price working type
        /// </summary>
        [JsonProperty("wt"), JsonConverter(typeof(WorkingTypeConverter))]
        public WorkingType StopPriceWorking { get; set; }
        /// <summary>
        /// Original Order Type
        /// </summary>
        [JsonProperty("ot"), JsonConverter(typeof(OrderTypeConverter))]
        public OrderType OriginalType { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonProperty("ps"), JsonConverter(typeof(PositionSideConverter))]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// If Close-All, only pushed with conditional order
        /// </summary>
        [JsonProperty("cp")]
        public bool PushedConditionalOrder { get; set; }
        /// <summary>
        /// Activation Price, only pushed with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonProperty("AP")]
        public decimal ActivationPrice { get; set; }
        /// <summary>
        /// Callback Rate, only pushed with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonProperty("cr")]
        public decimal CallbackRate { get; set; }
        /// <summary>
        /// Realized profit of the trade
        /// </summary>
        [JsonProperty("rp")]
        public decimal RealizedProfit { get; set; }
    }
}
