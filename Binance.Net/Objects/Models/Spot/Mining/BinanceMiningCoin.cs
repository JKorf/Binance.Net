namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Mining coin info
    /// </summary>
    [SerializationModel]
    public record BinanceMiningCoin
    {
        /// <summary>
        /// ["<c>coinName</c>"] The name of the coin
        /// </summary>
        [JsonPropertyName("coinName")]
        public string CoinName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>coinId</c>"] The id of the coin
        /// </summary>
        [JsonPropertyName("coinId")]
        public int? CoinId { get; set; } = null;
        /// <summary>
        /// ["<c>poolIndex</c>"] The pool index
        /// </summary>
        [JsonPropertyName("poolIndex")]
        public int PoolIndex { get; set; }

        /// <summary>
        /// ["<c>algoId</c>"] Algorithm id
        /// </summary>
        [JsonPropertyName("algoId")]
        public int? AlgorithmId { get; set; } = null;
        /// <summary>
        /// ["<c>algoName</c>"] Algorithm name
        /// </summary>
        [JsonPropertyName("algoName")]
        public string AlgorithmName { get; set; } = string.Empty;
    }
}

