using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Prevented order info
    /// </summary>
    public record BinancePreventedTrade
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Match id
        /// </summary>
        [JsonPropertyName("preventedMatchId")]
        public long PreventedMatchId { get; set; }
        /// <summary>
        /// Taker order id
        /// </summary>
        [JsonPropertyName("takerOrderId")]
        public long TakerOrderId { get; set; }
        /// <summary>
        /// Maker order id
        /// </summary>
        [JsonPropertyName("markerOrderId")]
        public long MakerOrderId { get; set; }
        /// <summary>
        /// Trade group id
        /// </summary>
        [JsonPropertyName("tradeGroupId")]
        public long TradeGroupId { get; set; }
        /// <summary>
        /// Self trade prevention mode
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonPropertyName("selfTradePreventionMode")]
        public SelfTradePreventionMode SelfTradePreventionMode { get; set; }
        /// <summary>
        /// Trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Prevented quantity
        /// </summary>
        [JsonPropertyName("makerPreventedQuantity")]
        public decimal MakerPreventedQuantity { get; set; }
        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("transactTime")]
        public DateTime TransactTime { get; set; }
    }
}
