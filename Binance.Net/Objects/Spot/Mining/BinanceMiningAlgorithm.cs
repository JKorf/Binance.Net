using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.Mining
{
    /// <summary>
    /// Mining coin info
    /// </summary>
    public class BinanceMiningAlgorithm
    {
        /// <summary>
        /// The name of the algorithm
        /// </summary>
        [JsonProperty("algoName")]
        public string AlgorithmName { get; set; } = string.Empty;
        /// <summary>
        /// The id of the algorithm
        /// </summary>
        [JsonProperty("algoId")]
        public string AlgorithmId { get; set; } = string.Empty;
        /// <summary>
        /// The pool index
        /// </summary>
        public int PoolIndex { get; set; }

        /// <summary>
        /// The unit of measurement
        /// </summary>
        public string Unit { get; set; } = string.Empty;
    }
}
