using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.MarketStream
{
    /// <summary>
    /// Composite index info
    /// </summary>
    public class BinanceFuturesStreamCompositeIndex : BinanceStreamEvent
    {
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The price
        /// </summary>
        [JsonProperty("p")]
        public decimal Price { get; set; }

        /// <summary>
        /// Composition
        /// </summary>
        [JsonProperty("c")]
        public IEnumerable<BinanceFuturesStreamCompositeIndexAsset> Composition { get; set; } = new BinanceFuturesStreamCompositeIndexAsset[0];
    }

    /// <summary>
    /// Composite index asset info
    /// </summary>
    public class BinanceFuturesStreamCompositeIndexAsset
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonProperty("b")]
        public string Asset { get; set; } = "";
        /// <summary>
        /// Weight in quantity
        /// </summary>
        [JsonProperty("w")]
        public decimal WeightInQuantity { get; set; }
        /// <summary>
        /// Weight in percentage
        /// </summary>
        [JsonProperty("W")]
        public decimal WeightInPercentage { get; set; }
    }
}
