using Binance.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects
{
    /// <summary>
    /// The result of placing a new order
    /// </summary>
    public class BinancePlacedOrder
    {
        /// <summary>
        /// The symbol the order is for
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The order id as assigned by Binance
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// The order id as assigned by the client
        /// </summary>
        public string ClientOrderId { get; set; } = "";
        /// <summary>
        /// Original order id
        /// </summary>
        [JsonOptionalProperty]
        [JsonProperty("origClientOrderId")]
        public string OriginalClientOrderId { get; set; } = "";
        /// <summary>
        /// The time the order was placed
        /// </summary>
        [JsonOptionalProperty]
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime TransactTime { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The original quantity of the order
        /// </summary>
        [JsonProperty("origQty")]
        public decimal OriginalQuantity { get; set; }
        /// <summary>
        /// The quantity of the order that is executed
        /// </summary>
        [JsonProperty("executedQty")]
        public decimal ExecutedQuantity { get; set; }
        /// <summary>
        /// Cummulative amount
        /// </summary>
        [JsonProperty("cummulativeQuoteQty")]
        public decimal CummulativeQuoteQuantity { get; set; }
        /// <summary>
        /// The current status of the order
        /// </summary>
        [JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// For what time the order lasts
        /// </summary>
        [JsonConverter(typeof(TimeInForceConverter))]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public OrderType Type { get; set; }
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Stop price for the order
        /// </summary>
        [JsonOptionalProperty]
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// Id of the order list this order belongs to
        /// </summary>
        public long? OrderListId { get; set; }
        /// <summary>
        /// Fills for the order
        /// </summary>
        [JsonOptionalProperty]
        public IEnumerable<BinanceOrderTrade>? Fills { get; set; }
    }
}
