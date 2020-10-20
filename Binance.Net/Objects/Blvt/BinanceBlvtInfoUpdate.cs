using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Blvt
{
    /// <summary>
    /// Blvt info update
    /// </summary>
    public class BinanceBlvtInfoUpdate: BinanceStreamEvent
    {
        /// <summary>
        /// Token name
        /// </summary>
        [JsonProperty("s")]
        public string TokenName { get; set; } = "";
        /// <summary>
        /// Token issued
        /// </summary>
        [JsonProperty("m")]
        public decimal TokenIssued { get; set; }
        /// <summary>
        /// Nav
        /// </summary>
        [JsonProperty("n")]
        public decimal Nav { get; set; }

        /// <summary>
        /// Baskets
        /// </summary>
        [JsonProperty("b")]
        public IEnumerable<BlvtBasket> Baskets { get; set; } = new List<BlvtBasket>();
        /// <summary>
        /// Token issued
        /// </summary>
        [JsonProperty("l")]
        public decimal RealLeverage { get; set; }
        /// <summary>
        /// Token issued
        /// </summary>
        [JsonProperty("t")]
        public decimal TargetLeverage { get; set; }
        /// <summary>
        /// Funding ratio
        /// </summary>
        [JsonProperty("f")]
        public decimal FundingRatio { get; set; }
    }

    /// <summary>
    /// Basket
    /// </summary>
    public class BlvtBasket
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = "";
        /// <summary>
        /// Position
        /// </summary>
        [JsonProperty("n")]
        public decimal Position { get; set; }
    }
}
