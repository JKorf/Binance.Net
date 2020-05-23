﻿using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.Mining
{
    /// <summary>
    /// Mining coin info
    /// </summary>
    public class BinanceMiningCoin
    {
        /// <summary>
        /// The name of the coin
        /// </summary>
        [JsonProperty("coinName")]
        public string CoinName { get; set; } = "";
        /// <summary>
        /// The id of the coin
        /// </summary>
        [JsonProperty("coinId")]
        public string CoinId { get; set; } = "";
        /// <summary>
        /// The pool index
        /// </summary>
        public int PoolIndex { get; set; }

        /// <summary>
        /// Algorithm id
        /// </summary>
        [JsonProperty("algoId")]
        public string AlgorithmId { get; set; } = "";
        /// <summary>
        /// Algorithm name
        /// </summary>
        [JsonProperty("algoName")]
        public string AlgorithmName { get; set; } = "";
    }
}
