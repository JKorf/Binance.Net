namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Asset info for dust conversion
    /// </summary>
    [SerializationModel]
    public record BinanceMarginDustAsset
    {
        /// <summary>
        /// Total btc
        /// </summary>
        [JsonPropertyName("totalTransferBtc")]
        public decimal TotalTransferBtc { get; set; }
        /// <summary>
        /// Total bnb
        /// </summary>
        [JsonPropertyName("totalTransferBNB")]
        public decimal TotalTransferBnb { get; set; }
        /// <summary>
        /// Dribblet percentage
        /// </summary>
        [JsonPropertyName("dribbletPercentage")]
        public decimal DribbletPercentage { get; set; }
        /// <summary>
        /// Details
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
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Asset full name
        /// </summary>
        [JsonPropertyName("assetFullName")]
        public string AssetFullName { get; set; } = string.Empty;
        /// <summary>
        /// Quantity fee
        /// </summary>
        [JsonPropertyName("amountFree")]
        public decimal QuantityFree { get; set; }
        /// <summary>
        /// To btc
        /// </summary>
        [JsonPropertyName("toBTC")]
        public decimal ToBtc { get; set; }
        /// <summary>
        /// To bnb
        /// </summary>
        [JsonPropertyName("toBNB")]
        public decimal ToBnb { get; set; }
        /// <summary>
        /// To bnb off exchange
        /// </summary>
        [JsonPropertyName("toBNBOffExchange")]
        public decimal ToBnbOffExchange { get; set; }
        /// <summary>
        /// Exchange
        /// </summary>
        [JsonPropertyName("exchange")]
        public decimal Exchange { get; set; }
    }
}
