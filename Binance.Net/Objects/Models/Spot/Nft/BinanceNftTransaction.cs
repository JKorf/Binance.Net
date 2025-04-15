namespace Binance.Net.Objects.Models.Spot.NFT
{
    /// <summary>
    /// NFT transaction
    /// </summary>
    public record BinanceNftTransaction
    {
        /// <summary>
        /// Order number, 0: purchase order, 1: sell order, 2: royalty income, 3: primary market order, 4: mint fee
        /// </summary>
        [JsonPropertyName("orderNo")]
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// Tokens
        /// </summary>
        [JsonPropertyName("tokens")]
        public IEnumerable<BinanceNftAsset> Tokens { get; set; } = Array.Empty<BinanceNftAsset>();
        /// <summary>
        /// Trade time
        /// </summary>
        [JsonPropertyName("tradeTime")]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Trade amount
        /// </summary>
        [JsonPropertyName("tradeAmount")]
        public string TradeAmount { get; set; } = string.Empty;
        /// <summary>
        /// Trade currency
        /// </summary>
        [JsonPropertyName("tradeCurrency")]
        public string TradeCurrency { get; set; } = string.Empty;
    }
}
