using Binance.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public string Symbol { get; set; }
        /// <summary>
        /// The order id as assigned by Binance
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// The order id as assigned by the client
        /// </summary>
        public string ClientOrderId { get; set; }
        /// <summary>
        /// The time the order was placed
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime TransactTime { get; set; }
        public decimal Price { get; set; }
        [JsonProperty("origQty")]
        public decimal OriginalQuantity { get; set; }
        [JsonProperty("executedQty")]
        public decimal ExecutedQuantity { get; set; }
        [JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status { get; set; }
        [JsonConverter(typeof(TimeInForceConverter))]
        public TimeInForce TimeInForce { get; set; }
        [JsonConverter(typeof(OrderTypeConverter))]
        public OrderType Type { get; set; }
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        public List<BinanceOrderTrade> Fills { get; set; }
    }
}
