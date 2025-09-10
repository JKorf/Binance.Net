namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Mining coin info
    /// </summary>
    [SerializationModel]
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
        public int? CoinId { get; set; } = null;
        /// <summary>
        /// The pool index
        /// </summary>
        [JsonPropertyName("poolIndex")]
        public int PoolIndex { get; set; }

        /// <summary>
        /// Algorithm id
        /// </summary>
        [JsonPropertyName("algoId")]
        public int? AlgorithmId { get; set; } = null;
        /// <summary>
        /// Algorithm name
        /// </summary>
        [JsonPropertyName("algoName")]
        public string AlgorithmName { get; set; } = string.Empty;
    }
}
