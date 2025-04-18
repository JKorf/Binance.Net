namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Customize margin call result
    /// </summary>
    [SerializationModel]
    public record BinanceCryptoLoanMarginCallResult
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Collateral asset
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Pre margin call 
        /// </summary>
        [JsonPropertyName("preMarginCall")]
        public decimal PreMarginCall { get; set; }
        /// <summary>
        /// After margin call
        /// </summary>
        [JsonPropertyName("afterMarginCall")]
        public decimal AfterMarginCall { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("customizeTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
