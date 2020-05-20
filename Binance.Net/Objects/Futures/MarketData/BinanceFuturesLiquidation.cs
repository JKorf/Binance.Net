using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// Forced liquidation info
    /// </summary>
    public class BinanceFuturesLiquidation : IBinanceFuturesLiquidation
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Total quantity
        /// </summary>
        [JsonProperty("origQty")]
        public decimal LastQuantityFilled { get; set; }
        /// <summary>
        /// The executed quantity
        /// </summary>
        [JsonProperty("executedQty")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Average price
        /// </summary>
        [JsonProperty("avgPrice")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonConverter(typeof(TimeInForceConverter))]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public OrderType Type { get; set; }
        /// <summary>
        /// Forced time
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
    }
}