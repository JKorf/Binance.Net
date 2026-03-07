namespace Binance.Net.Objects.Models.Spot.GiftCard
{
    /// <summary>
    /// Binance Gift Card token limit data
    /// </summary>
    public record BinanceGiftCardTokenLimit
    {
        /// <summary>
        /// ["<c>coin</c>"] Coin
        /// </summary>
        [JsonPropertyName("coin")]
        public string Coin { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fromMin</c>"] From min
        /// </summary>
        [JsonPropertyName("fromMin")]
        public decimal? FromMin { get; set; } = null;
        /// <summary>
        /// ["<c>fromMax</c>"] From max 
        /// </summary>
        [JsonPropertyName("fromMax")]
        public decimal? FromMax { get; set; } = null;
    }
}

