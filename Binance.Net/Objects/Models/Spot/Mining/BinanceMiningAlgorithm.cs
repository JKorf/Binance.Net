namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Mining coin info
    /// </summary>
    [SerializationModel]
    public record BinanceMiningAlgorithm
    {
        /// <summary>
        /// The name of the algorithm
        /// </summary>
        [JsonPropertyName("algoName")]
        public string AlgorithmName { get; set; } = string.Empty;
        /// <summary>
        /// The id of the algorithm
        /// </summary>
        [JsonPropertyName("algoId")]
        public int? AlgorithmId { get; set; } = null;
        /// <summary>
        /// The pool index
        /// </summary>
        [JsonPropertyName("poolIndex")]
        public int PoolIndex { get; set; }

        /// <summary>
        /// The unit of measurement
        /// </summary>
        [JsonPropertyName("unit")]
        public string Unit { get; set; } = string.Empty;
    }
}
