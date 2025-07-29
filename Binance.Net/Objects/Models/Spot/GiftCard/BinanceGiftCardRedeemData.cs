namespace Binance.Net.Objects.Models.Spot.GiftCard
{
    /// <summary>
    /// Binance Gift Card redeem data
    /// </summary>
    public record BinanceGiftCardRedeemData
    {
        /// <summary>
        /// Reference number
        /// </summary>
        [JsonPropertyName("referenceNo")]
        public string ReferenceNumber { get; set; } = string.Empty;
        /// <summary>
        /// Identity Number
        /// </summary>
        [JsonPropertyName("identityNo")]
        public string IdentityNumber { get; set; } = string.Empty;
        /// <summary>
        /// Token
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; } = null;
    }
}