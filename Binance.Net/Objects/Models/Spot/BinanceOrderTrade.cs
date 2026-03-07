namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    public record BinanceOrderTrade
    {
        /// <summary>
        /// ["<c>tradeId</c>"] The trade identifier.
        /// </summary>
        [JsonPropertyName("tradeId")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price of the trade
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>qty</c>"] Quantity of the trade
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>commission</c>"] Fee paid over this trade
        /// </summary>
        [JsonPropertyName("commission")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>commissionAsset</c>"] The asset the fee is paid in.
        /// </summary>
        [JsonPropertyName("commissionAsset")]
        public string FeeAsset { get; set; } = string.Empty;
    }
}

