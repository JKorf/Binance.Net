using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Futures stream symbol update
    /// </summary>
    public class BinanceFuturesStreamSymbolUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Pair
        /// </summary>
        [JsonProperty("ps")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonProperty("ct")]
        [JsonConverter(typeof(EnumConverter))]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Delivery date
        /// </summary>
        [JsonProperty("dt")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// Onboard date
        /// </summary>
        [JsonProperty("ot")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? OnboardDate { get; set; }
        /// <summary>
        /// Symbol status
        /// </summary>
        [JsonProperty("cs")]
        [JsonConverter(typeof(EnumConverter))]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// Brackets
        /// </summary>
        [JsonProperty("bks")]
        public IEnumerable<BinanceBracketUpdate>? Brackets { get; set; }
    }

    /// <summary>
    /// Bracket update
    /// </summary>
    public class BinanceBracketUpdate
    {
        /// <summary>
        /// Notional bracket
        /// </summary>
        [JsonProperty("bs")]
        public int NotionalBracket { get; set; }
        /// <summary>
        /// Floor notional
        /// </summary>
        [JsonProperty("bnf")]
        public decimal FloorNotional { get; set; }
        /// <summary>
        /// Max notional
        /// </summary>
        [JsonProperty("bnc")]
        public decimal MaxNotional { get; set; }
        /// <summary>
        /// Maintenance ratio
        /// </summary>
        [JsonProperty("mmr")]
        public decimal MaintenanceRatio { get; set; }
        /// <summary>
        /// Min leverage
        /// </summary>
        [JsonProperty("mi")]
        public decimal MinLeverage { get; set; }
        /// <summary>
        /// Max leverage
        /// </summary>
        [JsonProperty("ma")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// Auxiliary number for quick calculation
        /// </summary>
        [JsonProperty("cf")]
        public decimal Auxiliary { get; set; }
    }
}
