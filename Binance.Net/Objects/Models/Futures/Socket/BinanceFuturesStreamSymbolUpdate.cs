using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Futures stream symbol update
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesStreamSymbolUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>s</c>"] The symbol.
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ps</c>"] The pair.
        /// </summary>
        [JsonPropertyName("ps")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ct</c>"] Contract type
        /// </summary>
        [JsonPropertyName("ct")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// ["<c>dt</c>"] Delivery date
        /// </summary>
        [JsonPropertyName("dt")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// ["<c>ot</c>"] Onboard date
        /// </summary>
        [JsonPropertyName("ot")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? OnboardDate { get; set; }
        /// <summary>
        /// ["<c>cs</c>"] Symbol status
        /// </summary>
        [JsonPropertyName("cs")]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// ["<c>bks</c>"] Brackets
        /// </summary>
        [JsonPropertyName("bks")]
        public BinanceBracketUpdate[]? Brackets { get; set; }
    }

    /// <summary>
    /// Bracket update
    /// </summary>
    public record BinanceBracketUpdate
    {
        /// <summary>
        /// ["<c>bs</c>"] Notional bracket
        /// </summary>
        [JsonPropertyName("bs")]
        public int NotionalBracket { get; set; }
        /// <summary>
        /// ["<c>bnf</c>"] Floor notional
        /// </summary>
        [JsonPropertyName("bnf")]
        public decimal FloorNotional { get; set; }
        /// <summary>
        /// ["<c>bnc</c>"] Max notional
        /// </summary>
        [JsonPropertyName("bnc")]
        public decimal MaxNotional { get; set; }
        /// <summary>
        /// ["<c>mmr</c>"] Maintenance ratio
        /// </summary>
        [JsonPropertyName("mmr")]
        public decimal MaintenanceRatio { get; set; }
        /// <summary>
        /// ["<c>mi</c>"] Min leverage
        /// </summary>
        [JsonPropertyName("mi")]
        public decimal MinLeverage { get; set; }
        /// <summary>
        /// ["<c>ma</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("ma")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>cf</c>"] Auxiliary number for quick calculation
        /// </summary>
        [JsonPropertyName("cf")]
        public decimal Auxiliary { get; set; }
    }
}

