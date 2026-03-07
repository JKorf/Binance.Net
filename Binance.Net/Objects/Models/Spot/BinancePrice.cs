namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// The price of a symbol
    /// </summary>
    [SerializationModel]
    public record BinancePrice
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol the price is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] The price of the symbol
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>time</c>"] The data timestamp.
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; set; }
    }

    /// <summary>
    /// Futures-Coin price
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesCoinPrice : BinancePrice
    {
        /// <summary>
        /// ["<c>ps</c>"] Name of the pair
        /// </summary>
        [JsonPropertyName("ps")]
        public string Pair { get; set; } = string.Empty;
    }
}

