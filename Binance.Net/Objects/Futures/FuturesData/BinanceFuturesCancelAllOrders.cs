using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// The result of cancel all orders
    /// </summary>
    public class BinanceFuturesCancelAllOrders
    {
        /// <summary>
        /// The execution code
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// The execution message
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; } = string.Empty;
    }
}
