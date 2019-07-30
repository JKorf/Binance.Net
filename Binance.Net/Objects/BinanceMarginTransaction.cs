using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// The result of transfering, borrow or repay response
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
