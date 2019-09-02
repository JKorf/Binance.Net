using System;
using System.Collections.Generic;
using System.Text;
using Binance.Net.Converters;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Forced liquidation info
    /// </summary>
    public class BinanceForcedLiquidationHistory
    {
        /// <summary>
        /// Total results
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// Results this page
        /// </summary>
        public BinanceForcedLiquidationEntry[] Data { get; set; }
    }

    /// <summary>
    /// Forced liquidation info
    /// </summary>
    public class BinanceForcedLiquidationEntry
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
        public decimal ExecutedQuantity { get; set; }
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
        public string Symbol { get; set; }
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
    }
}
