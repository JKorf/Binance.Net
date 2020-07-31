using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// The result of query order
    /// </summary>
    public class BinanceFuturesOrder
    {
        /// <summary>
        /// The symbol the order is for
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The order id as assigned by Binance
        /// </summary>
        [JsonProperty("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// The order id as assigned by the client
        /// </summary>
        [JsonProperty("clientOrderId")]
        public string ClientOrderId { get; set; } = "";
        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// The average price of the order
        /// </summary>
        [JsonProperty("avgPrice")]
        public decimal AvgPrice { get; set; }
        /// <summary>
        /// Cumulative amount
        /// </summary>
        [JsonProperty("cumQty")]
        public decimal CumulativeQuantity { get; set; }
        /// <summary>
        /// Cumulative amount
        /// </summary>
        [JsonProperty("cumQuote")]
        public decimal CumulativeQuoteQuantity { get; set; }
        /// <summary>
        /// The quantity of the order that is executed
        /// </summary>
        [JsonProperty("executedQty")]
        public decimal ExecutedQuantity { get; set; }
        /// <summary>
        /// The original quantity of the order
        /// </summary>
        [JsonProperty("origQty")]
        public decimal OriginalQuantity { get; set; }
        /// <summary>
        /// Reduce Only
        /// </summary>
        [JsonProperty("reduceOnly")]
        public bool ReduceOnly { get; set; }

        /// <summary>
        /// if Close-All
        /// </summary>
        [JsonProperty("closePosition")]
        public bool ClosePosition { get; set; }

        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }

        /// <summary>
        /// The current status of the order
        /// </summary>
        [JsonProperty("status"), JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Stop price for the order
        /// </summary>
        [JsonProperty("stopPrice"), JsonOptionalProperty]
        public decimal? StopPrice { get; set; }

        /// <summary>
        /// For what time the order lasts
        /// </summary>
        [JsonProperty("timeInForce"), JsonConverter(typeof(TimeInForceConverter))]
        public TimeInForce TimeInForce { get; set; }

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonProperty("origType"), JsonConverter(typeof(OrderTypeConverter))]
        public OrderType OriginalType { get; set; }
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(OrderTypeConverter))]
        public OrderType Type { get; set; }

        /// <summary>
        /// Activation price, only return with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonProperty("activatePrice")]
        public decimal? ActivatePrice { get; set; }

        /// <summary>
        /// Callback rate, only return with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonProperty("priceRate")]
        public decimal? PriceRate { get; set; }

        /// <summary>
        /// The time the order was updated
        /// </summary>
        [JsonProperty("updateTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// The time the order was created
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// The working type
        /// </summary>
        [JsonProperty("workingType"), JsonConverter(typeof(WorkingTypeConverter))]
        public WorkingType WorkingType { get; set; }

        /// <summary>
        /// The position side of the order
        /// </summary>
        [JsonProperty("positionSide"), JsonConverter(typeof(PositionSideConverter))]
        public PositionSide PositionSide { get; set; }
    }
}
