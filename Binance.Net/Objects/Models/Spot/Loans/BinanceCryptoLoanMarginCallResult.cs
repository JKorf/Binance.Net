namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Customize margin call result
    /// </summary>
    [SerializationModel]
    public record BinanceCryptoLoanMarginCallResult
    {
        /// <summary>
        /// ["<c>orderId</c>"] The order identifier.
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>collateralCoin</c>"] Collateral asset
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>preMarginCall</c>"] Pre margin call 
        /// </summary>
        [JsonPropertyName("preMarginCall")]
        public decimal PreMarginCall { get; set; }
        /// <summary>
        /// ["<c>afterMarginCall</c>"] After margin call
        /// </summary>
        [JsonPropertyName("afterMarginCall")]
        public decimal AfterMarginCall { get; set; }
        /// <summary>
        /// ["<c>customizeTime</c>"] The customization timestamp.
        /// </summary>
        [JsonPropertyName("customizeTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}

