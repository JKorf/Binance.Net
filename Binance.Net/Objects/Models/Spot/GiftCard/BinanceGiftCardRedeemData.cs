namespace Binance.Net.Objects.Models.Spot.GiftCard
{
    /// <summary>
    /// Binance Gift Card redeem data
    /// </summary>
    public record BinanceGiftCardRedeemData
    {
        /// <summary>
        /// ["<c>referenceNo</c>"] Reference number
        /// </summary>
        [JsonPropertyName("referenceNo")]
        public string ReferenceNumber { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>identityNo</c>"] Identity Number
        /// </summary>
        [JsonPropertyName("identityNo")]
        public string IdentityNumber { get; set; } = string.Empty;
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