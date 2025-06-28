namespace Binance.Net.Objects.Models.Spot.GiftCard
{
    /// <summary>
    /// Binance Gift Card data
    /// </summary>
    public record BinaceGiftCardData
    {
        /// <summary>
        /// Reference number
        /// </summary>
        [JsonPropertyName("referenceNo")]
        public string ReferenceNumber { get; set; } = string.Empty;
        /// <summary>
        /// Code
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// Expired time
        /// </summary>
        [JsonPropertyName("expiredTime")]
        public DateTime ExpiredTime { get; set; }
    }
}