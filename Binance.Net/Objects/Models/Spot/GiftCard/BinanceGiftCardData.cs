namespace Binance.Net.Objects.Models.Spot.GiftCard
{
    /// <summary>
    /// Binance Gift Card data
    /// </summary>
    public record BinaceGiftCardData
    {
        /// <summary>
        /// ["<c>referenceNo</c>"] Reference number
        /// </summary>
        [JsonPropertyName("referenceNo")]
        public string ReferenceNumber { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>code</c>"] Code
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>expiredTime</c>"] Expired time
        /// </summary>
        [JsonPropertyName("expiredTime")]
        public DateTime ExpiredTime { get; set; }
    }
}