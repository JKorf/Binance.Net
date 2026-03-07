namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// User balance
    /// </summary>
    [SerializationModel]
    public record BinanceUserBalance : BinanceBalance
    {
        /// <summary>
        /// ["<c>freeze</c>"] Frozen
        /// </summary>
        [JsonPropertyName("freeze")]
        public decimal Freeze { get; set; }
        /// <summary>
        /// ["<c>withdrawing</c>"] Currently withdrawing
        /// </summary>
        [JsonPropertyName("withdrawing")]
        public decimal Withdrawing { get; set; }
        /// <summary>
        /// ["<c>ipoable</c>"] Ipoable amount
        /// </summary>
        [JsonPropertyName("ipoable")]
        public decimal Ipoable { get; set; }
        /// <summary>
        /// ["<c>btcValuation</c>"] Value in btc
        /// </summary>
        [JsonPropertyName("btcValuation")]
        public decimal BtcValuation { get; set; }
    }
}

