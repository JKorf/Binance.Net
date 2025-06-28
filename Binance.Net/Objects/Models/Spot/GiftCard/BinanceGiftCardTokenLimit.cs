namespace Binance.Net.Objects.Models.Spot.GiftCard
{
    /// <summary>
    /// Binance Gift Card token limit data
    /// </summary>
    public record BinanceGiftCardTokenLimit
    {
        /// <summary>
        /// Coin
        /// </summary>
        [JsonPropertyName("coin")]
        public string Coin { get; set; } = string.Empty;
        /// <summary>
        /// From min
        /// </summary>
        [JsonPropertyName("fromMin")]
        public decimal? FromMin { get; set; } = null;
        /// <summary>
        /// From max 
        /// </summary>
        [JsonPropertyName("fromMax")]
        public decimal? FromMax { get; set; } = null;
    }
}
