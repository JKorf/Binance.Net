﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// Notional and Leverage Brackets
    /// </summary>
    public class BinanceFuturesSymbolBracket
    {
        /// <summary>
        /// Symbol or pair
        /// </summary>
        [JsonProperty("symbol")]
        public string SymbolOrPair { get; set; } = "";

        [JsonProperty("pair")]
        private string Pair
        {
            set => SymbolOrPair = value;
        }

        /// <summary>
        /// Brackets
        /// </summary>
        public IEnumerable<BinanceFuturesBracket> Brackets { get; set; } = new List<BinanceFuturesBracket>();

    }

    /// <summary>
    /// Bracket
    /// </summary>
    public class BinanceFuturesBracket
    {
        /// <summary>
        /// Bracket
        /// </summary>
        public int Bracket { get; set; }

        /// <summary>
        /// Max initial leverage for this bracket
        /// </summary>
        public int InitialLeverage { get; set; }

        /// <summary>
        /// Cap of this bracket
        /// </summary>
        [JsonProperty("notionalCap")]
        public long Cap { get; set; }
        [JsonProperty("qtyCap")]
        private long QuantityCap
        {
            set => Cap = value;
        }

        /// <summary>
        /// Floor of this bracket
        /// </summary>
        [JsonProperty("notionalFloor")]
        public long Floor { get; set; }
        [JsonProperty("qtylFloor")]
        private long QuantityFloor
        {
            set => Floor = value;
        }

        /// <summary>
        /// Maintenance ratio for this bracket
        /// </summary>
        [JsonProperty("maintMarginRatio")]
        public decimal MaintenanceMarginRatio { get; set; }
    }
}