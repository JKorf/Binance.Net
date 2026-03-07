namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Mining coin info
    /// </summary>
    [SerializationModel]
    public record BinanceMiningAlgorithm
    {
        /// <summary>
        /// ["<c>algoName</c>"] The name of the algorithm
        /// </summary>
        [JsonPropertyName("algoName")]
        public string AlgorithmName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>algoId</c>"] The id of the algorithm
        /// </summary>
        [JsonPropertyName("algoId")]
        public int? AlgorithmId { get; set; } = null;
        /// <summary>
        /// ["<c>poolIndex</c>"] The pool index
        /// </summary>
        [JsonPropertyName("poolIndex")]
        public int PoolIndex { get; set; }

        /// <summary>
        /// ["<c>unit</c>"] The unit of measurement
        /// </summary>
        [JsonPropertyName("unit")]
        public string Unit { get; set; } = string.Empty;
    }
}

