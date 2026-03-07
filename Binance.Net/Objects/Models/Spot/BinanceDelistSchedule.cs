namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Spot symbol delist info
    /// </summary>
    [SerializationModel]
    public record BinanceDelistSchedule
    {
        /// <summary>
        /// ["<c>delistTime</c>"] The delisting time.
        /// </summary>
        [JsonPropertyName("delistTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime DelistTime { get; set; }

        /// <summary>
        /// ["<c>symbols</c>"] Symbols being delisted
        /// </summary>
        [JsonPropertyName("symbols")]
        public string[] Symbols { get; set; } = [];
    }
}

