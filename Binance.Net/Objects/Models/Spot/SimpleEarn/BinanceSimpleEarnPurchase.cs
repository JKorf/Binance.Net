namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Purchase id
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnPurchase
    {
        /// <summary>
        /// ["<c>success</c>"] Whether the request succeeded.
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// ["<c>purchaseId</c>"] Purchase id
        /// </summary>
        [JsonPropertyName("purchaseId")]
        public long PurchaseId { get; set; }
        /// <summary>
        /// ["<c>positionId</c>"] Position id
        /// </summary>
        [JsonPropertyName("positionId")]
        public string? PositionId { get; set; }
    }
}

