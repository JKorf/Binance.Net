using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.BSwap
{
    /// <summary>
    /// Unclaimed rewards info
    /// </summary>
    public class BinanceBSwapUnclaimedRewards
    {
        /// <summary>
        /// Total unclaimed rewards
        /// </summary>
        [JsonProperty("totalUnclaimedRewards")]
        public Dictionary<string, decimal> TotalUnclaimedRewards { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// Details
        /// </summary>
        [JsonProperty("details")]
        public Dictionary<string, Dictionary<string, decimal>> Details { get; set; } = new Dictionary<string, Dictionary<string, decimal>>();
    }
}