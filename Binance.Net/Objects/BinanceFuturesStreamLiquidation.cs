using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class BinanceFuturesStreamLiquidation: BinanceStreamEvent
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("s")]
        public string? Symbol { get; set; }

        /// <summary>
        /// Liquidation Sided
        /// </summary>
        [JsonProperty("S")]
        public OrderSide Side { get; set; }
        
        /// <summary>
        /// Liquidation order type
        /// </summary>
        [JsonProperty("o")]
        public OrderType OrderType { get; set; }
        
        /// <summary>
        /// Liquidation Time in Force
        /// </summary>
        [JsonProperty("f")]
        public TimeInForce TimeInForce { get; set; }
        
        /// <summary>
        /// Liquidation Original Quantity
        /// </summary>
        [JsonProperty("q")]
        public decimal OriginalQuantity { get; set; }
        
        /// <summary>
        /// Liquidation order price
        /// </summary>
        [JsonProperty("p")]
        public decimal Price { get; set; }
        
        /// <summary>
        /// Liquidation Average Price
        /// </summary>
        [JsonProperty("ap")]
        public decimal AveragePrice { get; set; }
        
        /// <summary>
        /// Liquidation Order Status
        /// </summary>
        [JsonProperty("X")]
        public OrderStatus OrderStatus { get; set; }
        
        /// <summary>
        /// Liquidation Last Filled Quantity
        /// </summary>
        [JsonProperty("l")]
        public decimal LastFilledQty { get; set; }
        
        /// <summary>
        /// Liquidation Accumulated fill quantity
        /// </summary>
        [JsonProperty("z")]
        public decimal CumulativeFilledQty { get; set; }
        
        /// <summary>
        /// Liquidation Trade Time
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
    }
}
