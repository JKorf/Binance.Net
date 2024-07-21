namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trade fee info
    /// </summary>
    public record BinanceTradeFee
    {
        /// <summary>
        /// The symbol this fee is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The fee for trades where you're the maker
        /// </summary>
        [JsonPropertyName("makerCommission")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// The fee for trades where you're the taker
        /// </summary>
        [JsonPropertyName("takerCommission")]
        public decimal TakerFee { get; set; }
    }
}
