namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Notional and Leverage Brackets
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesSymbolBracket
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol or pair
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>notionalCoef</c>"] user symbol bracket multiplier, only appears when user's symbol bracket is adjusted 
        /// </summary>
        [JsonPropertyName("notionalCoef")]
        public decimal? NotionalCoef { get; set; }
        [JsonInclude, JsonPropertyName("pair")]
        internal string Pair
        {
            set => Symbol = value;
        }

        /// <summary>
        /// ["<c>brackets</c>"] Brackets
        /// </summary>
        [JsonPropertyName("brackets")]
        public BinanceFuturesBracket[] Brackets { get; set; } = Array.Empty<BinanceFuturesBracket>();

    }

    /// <summary>
    /// Bracket
    /// </summary>
    public record BinanceFuturesBracket
    {
        /// <summary>
        /// ["<c>bracket</c>"] Bracket
        /// </summary>
        [JsonPropertyName("bracket")]
        public int Bracket { get; set; }

        /// <summary>
        /// ["<c>initialLeverage</c>"] Max initial leverage for this bracket
        /// </summary>
        [JsonPropertyName("initialLeverage")]
        public int InitialLeverage { get; set; }

        /// <summary>
        /// ["<c>notionalCap</c>"] Cap of this bracket
        /// </summary>
        [JsonPropertyName("notionalCap")]
        public long Cap { get; set; }
        [JsonInclude, JsonPropertyName("qtyCap")]
        internal long QuantityCap
        {
            set => Cap = value;
        }

        /// <summary>
        /// ["<c>notionalFloor</c>"] Floor of this bracket
        /// </summary>
        [JsonPropertyName("notionalFloor")]
        public long Floor { get; set; }
        [JsonInclude, JsonPropertyName("qtyFloor")]
        internal long QuantityFloor
        {
            set => Floor = value;
        }

        /// <summary>
        /// ["<c>maintMarginRatio</c>"] Maintenance ratio for this bracket
        /// </summary>
        [JsonPropertyName("maintMarginRatio")]
        public decimal MaintenanceMarginRatio { get; set; }

        /// <summary>
        /// ["<c>cum</c>"] Auxiliary number for quick calculation 
        /// </summary>
        [JsonPropertyName("cum")]
        public decimal MaintAmount { get; set; }
    }
}

