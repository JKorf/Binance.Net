using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Response to the change in initial leverage request
    /// </summary>
    public class BinanceFuturesInitialLeverageChangeResult
    {
        /// <summary>
        /// New leverage multiplier
        /// </summary>
        public int Leverage { get; set; }

        /// <summary>
        /// Maximum value that can be held
        /// NOTE: string type, because the value van be 'inf' (infinite)
        /// </summary>
        public string? MaxNotionalValue { get; set; }
        
        /// <summary>
        /// Max quantity
        /// </summary>
        [JsonProperty("maxQty")]
        public string? MaxQuantity { get; set; }
        /// <summary>
        /// Symbol the request is for
        /// </summary>
        public string? Symbol { get; set; }
    }

}
