namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Purchase id
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnPurchase
    {
        /// <summary>
        /// Whether the request succeeded.
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// Purchase id
        /// </summary>
        [JsonPropertyName("purchaseId")]
        public long PurchaseId { get; set; }
        /// <summary>
        /// Position id
        /// </summary>
        [JsonPropertyName("positionId")]
        public string? PositionId { get; set; }
    }
}
