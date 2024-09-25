namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Notional and Leverage Brackets
    /// </summary>
    public record BinanceFuturesSymbolBracket
    {
        /// <summary>
        /// Symbol or pair
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// //user symbol bracket multiplier, only appears when user's symbol bracket is adjusted 
        /// </summary>
        [JsonPropertyName("notionalCoef")]
        public decimal? NotionalCoef { get; set; }
        [JsonPropertyName("pair")]
        private string Pair
        {
            set => Symbol = value;
        }

        /// <summary>
        /// Brackets
        /// </summary>
        [JsonPropertyName("brackets")]
        public IEnumerable<BinanceFuturesBracket> Brackets { get; set; } = Array.Empty<BinanceFuturesBracket>();

    }

    /// <summary>
    /// Bracket
    /// </summary>
    public record BinanceFuturesBracket
    {
        /// <summary>
        /// Bracket
        /// </summary>
        [JsonPropertyName("bracket")]
        public int Bracket { get; set; }

        /// <summary>
        /// Max initial leverage for this bracket
        /// </summary>
        [JsonPropertyName("initialLeverage")]
        public int InitialLeverage { get; set; }

        /// <summary>
        /// Cap of this bracket
        /// </summary>
        [JsonPropertyName("notionalCap")]
        public long Cap { get; set; }
        [JsonPropertyName("qtyCap")]
        private long QuantityCap
        {
            set => Cap = value;
        }

        /// <summary>
        /// Floor of this bracket
        /// </summary>
        [JsonPropertyName("notionalFloor")]
        public long Floor { get; set; }
        [JsonPropertyName("qtyFloor")]
        private long QuantityFloor
        {
            set => Floor = value;
        }

        /// <summary>
        /// Maintenance ratio for this bracket
        /// </summary>
        [JsonPropertyName("maintMarginRatio")]
        public decimal MaintenanceMarginRatio { get; set; }

        /// <summary>
        /// Auxiliary number for quick calculation 
        /// </summary>
        [JsonPropertyName("cum")]
        public decimal MaintAmount { get; set; }
    }
}
