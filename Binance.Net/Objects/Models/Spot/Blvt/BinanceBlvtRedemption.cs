namespace Binance.Net.Objects.Models.Spot.Blvt
{
    /// <summary>
    /// Redemption info
    /// </summary>
    [SerializationModel]
    public record BinanceBlvtRedemption
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// Token name
        /// </summary>
        [JsonPropertyName("tokenName")]
        public string TokenName { get; set; } = string.Empty;
        /// <summary>
        /// Redemption quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// NAV price of redemption
        /// </summary>
        [JsonPropertyName("nav")]
        public decimal Nav { get; set; }
        /// <summary>
        /// Redemption fee in usdt
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Net redemption value in usdt
        /// </summary>
        [JsonPropertyName("netProceed")]
        public decimal NetProceed { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
