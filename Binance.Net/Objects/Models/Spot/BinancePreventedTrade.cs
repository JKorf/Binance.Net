using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Prevented order info
    /// </summary>
    [SerializationModel]
    public record BinancePreventedTrade
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>preventedMatchId</c>"] The prevented match identifier.
        /// </summary>
        [JsonPropertyName("preventedMatchId")]
        public long PreventedMatchId { get; set; }
        /// <summary>
        /// ["<c>takerOrderId</c>"] Taker order id
        /// </summary>
        [JsonPropertyName("takerOrderId")]
        public long TakerOrderId { get; set; }
        /// <summary>
        /// ["<c>makerSymbol</c>"] Maker symbol
        /// </summary>
        [JsonPropertyName("makerSymbol")]
        public string MakerSymbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>makerOrderId</c>"] Maker order id
        /// </summary>
        [JsonPropertyName("makerOrderId")]
        public long MakerOrderId { get; set; }
        /// <summary>
        /// ["<c>tradeGroupId</c>"] Trade group id
        /// </summary>
        [JsonPropertyName("tradeGroupId")]
        public long TradeGroupId { get; set; }
        /// <summary>
        /// ["<c>selfTradePreventionMode</c>"] Self trade prevention mode
        /// </summary>
        [JsonPropertyName("selfTradePreventionMode")]
        public SelfTradePreventionMode SelfTradePreventionMode { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>makerPreventedQuantity</c>"] Prevented quantity
        /// </summary>
        [JsonPropertyName("makerPreventedQuantity")]
        public decimal MakerPreventedQuantity { get; set; }
        /// <summary>
        /// ["<c>transactTime</c>"] Transaction time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("transactTime")]
        public DateTime TransactTime { get; set; }
    }
}

