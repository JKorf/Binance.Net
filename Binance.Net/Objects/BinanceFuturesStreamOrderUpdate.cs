using System;
using Newtonsoft.Json;
using Binance.Net.Converters;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Update data about an order
    /// </summary>
    public class BinanceFuturesStreamOrderUpdate: BinanceStreamEvent
    {
        /// <summary>
        /// The symbol the order is for
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The new client order id
        /// </summary>
        [JsonProperty("c")]
        public string ClientOrderId { get; set; } = "";
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
        /// The commission payed
        /// </summary>
        [JsonProperty("n")]
        public decimal Commission { get; set; }
        /// <summary>
        /// The asset the commission was taken from
        /// </summary>
        [JsonProperty("N")]
        public string CommissionAsset { get; set; } = "";
        /// <summary>
        /// The time of the update
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public DateTime OrderCreationTime { get; set; }
        /// <summary>
        /// The trade id
        /// </summary>
        [JsonProperty("t")]
        public long TradeId { get; set; }
        /// <summary>
        /// Whether the buyer is the maker
        /// </summary>
        [JsonProperty("m")]
        public bool BuyerIsMaker { get; set; }
    }
}
