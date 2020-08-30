using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.MarginData
{
    /// <summary>
    /// The result of transferring
    /// </summary>
    public class BinanceMarginTransaction
    {
        /// <summary>
        /// The Transaction id as assigned by Binance
        /// </summary>
        [JsonProperty("TranId")]
        public long TransactionId { get; set; }
    }
}
