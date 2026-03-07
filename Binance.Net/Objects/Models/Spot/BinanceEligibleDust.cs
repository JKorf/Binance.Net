namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Asset dusts that can be converted to BNB
    /// </summary>
    [SerializationModel]
    public record BinanceEligibleDusts
    {
        /// <summary>
        /// ["<c>totalTransferBtc</c>"] Total BTC value
        /// </summary>
        [JsonPropertyName("totalTransferBtc")]
        public decimal TotalTransferBTC { get; set; }
        /// <summary>
        /// ["<c>totalTransferBNB</c>"] Total BNB value
        /// </summary>
        [JsonPropertyName("totalTransferBNB")]
        public decimal TotalTransferBNB { get; set; }
        /// <summary>
        /// ["<c>dribbletPercentage</c>"] Commission fee
        /// </summary>
        [JsonPropertyName("dribbletPercentage")]
        public decimal FeePercentage { get; set; }
        /// <summary>
        /// ["<c>details</c>"] Assets eligible for conversion.
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
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>assetFullName</c>"] Full name of the asset
        /// </summary>
        [JsonPropertyName("assetFullName")]
        public string AssetFullName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amountFree</c>"] Amount free
        /// </summary>
        [JsonPropertyName("amountFree")]
        public decimal QuantityFree { get; set; }
        /// <summary>
        /// ["<c>toBTC</c>"] BTC value
        /// </summary>
        [JsonPropertyName("toBTC")]
        public decimal ToBTC { get; set; }
        /// <summary>
        /// ["<c>toBNB</c>"] BNB value without fee
        /// </summary>
        [JsonPropertyName("toBNB")]
        public decimal ToBNB { get; set; }
        /// <summary>
        /// ["<c>toBNBOffExchange</c>"] BNB value with fee
        /// </summary>
        [JsonPropertyName("toBNBOffExchange")]
        public decimal ToBNBOffExchange { get; set; }
        /// <summary>
        /// ["<c>exchange</c>"] Fee
        /// </summary>
        [JsonPropertyName("exchange")]
        public decimal Fee { get; set; }
    }
}

