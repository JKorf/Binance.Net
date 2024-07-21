namespace Binance.Net.Objects.Models.Spot.Blvt
{
    /// <summary>
    /// Redemption info
    /// </summary>
    public record BinanceBlvtRedemption
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Token name
        /// </summary>
        public string TokenName { get; set; } = string.Empty;
        /// <summary>
        /// Redemption quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// NAV price of redemption
        /// </summary>
        public decimal Nav { get; set; }
        /// <summary>
        /// Redemption fee in usdt
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// Net redemption value in usdt
        /// </summary>
        public decimal NetProceed { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
