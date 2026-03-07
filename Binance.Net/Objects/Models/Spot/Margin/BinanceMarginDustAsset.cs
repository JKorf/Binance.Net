namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Asset info for dust conversion
    /// </summary>
    [SerializationModel]
    public record BinanceMarginDustAsset
    {
        /// <summary>
        /// ["<c>totalTransferBtc</c>"] Total btc
        /// </summary>
        [JsonPropertyName("totalTransferBtc")]
        public decimal TotalTransferBtc { get; set; }
        /// <summary>
        /// ["<c>totalTransferBNB</c>"] Total bnb
        /// </summary>
        [JsonPropertyName("totalTransferBNB")]
        public decimal TotalTransferBnb { get; set; }
        /// <summary>
        /// ["<c>dribbletPercentage</c>"] Dribblet percentage
        /// </summary>
        [JsonPropertyName("dribbletPercentage")]
        public decimal DribbletPercentage { get; set; }
        /// <summary>
        /// ["<c>details</c>"] Details
        /// </summary>
        [JsonPropertyName("details")]
        public BinanceMarginDustAssetDetails[] Details { get; set; } = Array.Empty<BinanceMarginDustAssetDetails>();
    }

    /// <summary>
    /// Asset dust details
    /// </summary>
    public record BinanceMarginDustAssetDetails
    {
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>assetFullName</c>"] Asset full name
        /// </summary>
        [JsonPropertyName("assetFullName")]
        public string AssetFullName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amountFree</c>"] Quantity fee
        /// </summary>
        [JsonPropertyName("amountFree")]
        public decimal QuantityFree { get; set; }
        /// <summary>
        /// ["<c>toBTC</c>"] To btc
        /// </summary>
        [JsonPropertyName("toBTC")]
        public decimal ToBtc { get; set; }
        /// <summary>
        /// ["<c>toBNB</c>"] To bnb
        /// </summary>
        [JsonPropertyName("toBNB")]
        public decimal ToBnb { get; set; }
        /// <summary>
        /// ["<c>toBNBOffExchange</c>"] To bnb off exchange
        /// </summary>
        [JsonPropertyName("toBNBOffExchange")]
        public decimal ToBnbOffExchange { get; set; }
        /// <summary>
        /// ["<c>exchange</c>"] Exchange
        /// </summary>
        [JsonPropertyName("exchange")]
        public decimal Exchange { get; set; }
    }
}

