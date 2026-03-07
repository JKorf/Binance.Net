namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trade fee info
    /// </summary>
    [SerializationModel]
    public record BinanceTradeFee
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol this fee is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>makerCommission</c>"] The fee for trades where you're the maker
        /// </summary>
        [JsonPropertyName("makerCommission")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// ["<c>takerCommission</c>"] The fee for trades where you're the taker
        /// </summary>
        [JsonPropertyName("takerCommission")]
        public decimal TakerFee { get; set; }
    }
}

