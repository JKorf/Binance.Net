namespace Binance.Net.Objects.Models.Spot.GiftCard
{
    /// <summary>
    /// Binance Gift Card validity data
    /// </summary>
    public record BinanceGiftCardValidity
    {
        /// <summary>
        /// ["<c>valid</c>"] Is gift card valid
        /// </summary>
        [JsonPropertyName("valid")]
        public bool Valid { get; set; }
        /// <summary>
        /// ["<c>token</c>"] Token
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; } = null;
    }
}