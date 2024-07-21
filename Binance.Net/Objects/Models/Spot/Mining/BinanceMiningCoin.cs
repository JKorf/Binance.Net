namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Mining coin info
    /// </summary>
    public record BinanceMiningCoin
    {
        /// <summary>
        /// The name of the coin
        /// </summary>
        [JsonPropertyName("coinName")]
        public string CoinName { get; set; } = string.Empty;
        /// <summary>
        /// The id of the coin
        /// </summary>
        [JsonPropertyName("coinId")]
        public string CoinId { get; set; } = string.Empty;
        /// <summary>
        /// The pool index
        /// </summary>
        public int PoolIndex { get; set; }

        /// <summary>
        /// Algorithm id
        /// </summary>
        [JsonPropertyName("algoId")]
        public string AlgorithmId { get; set; } = string.Empty;
        /// <summary>
        /// Algorithm name
        /// </summary>
        [JsonPropertyName("algoName")]
        public string AlgorithmName { get; set; } = string.Empty;
    }
}
