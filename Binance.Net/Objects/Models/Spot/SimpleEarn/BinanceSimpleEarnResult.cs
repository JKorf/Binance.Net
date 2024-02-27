using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn result
    /// </summary>
    public class BinanceSimpleEarnResult
    {
        /// <summary>
        /// Result
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
