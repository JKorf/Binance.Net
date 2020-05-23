﻿using Binance.Net.Converters;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Attributes;
using Newtonsoft.Json;
using System;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// The result of cancel order
    /// </summary>
    public class BinanceFuturesCancelOrder
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
        /// Cummulative amount
        /// </summary>
        [JsonProperty("cumQty")]
        public decimal CummulativeQuantity { get; set; }
        /// <summary>
        /// Cummulative amount
        /// </summary>
        [JsonProperty("cumQuote")]
        public decimal CummulativeQuoteQuantity { get; set; }
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
        public decimal ActivatePrice { get; set; }

        /// <summary>
        /// Callback rate, only return with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonProperty("priceRate")]
        public decimal PriceRate { get; set; }

        /// <summary>
        /// The time the order was updated
        /// </summary>
        [JsonProperty("updateTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// The working type
        /// </summary>
        [JsonProperty("workingType"), JsonConverter(typeof(WorkingTypeConverter))]
        public WorkingType WorkingType { get; set; }
    }
}
