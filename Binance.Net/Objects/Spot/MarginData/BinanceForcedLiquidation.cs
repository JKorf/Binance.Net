using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.MarginData
{
    /// <summary>
    /// Forced liquidation info
    /// </summary>
    public class BinanceForcedLiquidation
    {
        /// <summary>
        /// Average price
        /// </summary>
        [JsonProperty("avgPrice")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// The executed quantity
        /// </summary>
        [JsonProperty("executedQty")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Total quantity
        /// </summary>
        [JsonProperty("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonConverter(typeof(TimeInForceConverter))]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        [JsonProperty("updatedTime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Is isolated margin
        /// </summary>
        public bool IsIsolated { get; set; }
    }
}
