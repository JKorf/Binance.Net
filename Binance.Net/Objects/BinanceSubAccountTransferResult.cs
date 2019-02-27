using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceSubAccountTransferResult
    {
        /// <summary>
        /// Whether the transfer was successful
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// The transaction id of the transfer
        /// </summary>
        [JsonProperty("txnId")]
        public string TransactionId { get; set; }
    }
}
