namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// User balance
    /// </summary>
    public record BinanceUserBalance : BinanceBalance
    {
        /// <summary>
        /// Frozen
        /// </summary>
        [JsonPropertyName("freeze")]
        public decimal Freeze { get; set; }
        /// <summary>
        /// Currently withdrawing
        /// </summary>
        [JsonPropertyName("withdrawing")]
        public decimal Withdrawing { get; set; }
        /// <summary>
        /// Ipoable amount
        /// </summary>
        [JsonPropertyName("ipoable")]
        public decimal Ipoable { get; set; }
        /// <summary>
        /// Value in btc
        /// </summary>
        [JsonPropertyName("btcValuation")]
        public decimal BtcValuation { get; set; }
    }
}
