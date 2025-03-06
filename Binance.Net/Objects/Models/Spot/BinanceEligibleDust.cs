namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Asset dusts that can be converted to BNB
    /// </summary>
    public record BinanceEligibleDusts
    {
        /// <summary>
        /// Total BTC value
        /// </summary>
        [JsonPropertyName("totalTransferBtc")]
        public decimal TotalTransferBTC { get; set; }
        /// <summary>
        /// Total BNB value
        /// </summary>
        [JsonPropertyName("totalTransferBNB")]
        public decimal TotalTransferBNB { get; set; }
        /// <summary>
        /// Commission fee
        /// </summary>
        [JsonPropertyName("dribbletPercentage")]
        public decimal FeePercentage { get; set; }
        /// <summary>
        /// Assets
        /// </summary>
        [JsonPropertyName("details")]
        public BinanceEligibleDust[] Details { get; set; } = Array.Empty<BinanceEligibleDust>();
    }

    /// <summary>
    /// Asset which can be converted
    /// </summary>
    public record BinanceEligibleDust
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Full name of the asset
        /// </summary>
        [JsonPropertyName("assetFullName")]
        public string AssetFullName { get; set; } = string.Empty;
        /// <summary>
        /// Amount free
        /// </summary>
        [JsonPropertyName("amountFree")]
        public decimal QuantityFree { get; set; }
        /// <summary>
        /// BTC value
        /// </summary>
        [JsonPropertyName("toBTC")]
        public decimal ToBTC { get; set; }
        /// <summary>
        /// BNB value without fee
        /// </summary>
        [JsonPropertyName("toBNB")]
        public decimal ToBNB { get; set; }
        /// <summary>
        /// BNB value with fee
        /// </summary>
        [JsonPropertyName("toBNBOffExchange")]
        public decimal ToBNBOffExchange { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("exchange")]
        public decimal Fee { get; set; }
    }
}
