using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Transaction
    /// </summary>
    public class BinanceSubAccountTransaction
    {
        /// <summary>
        /// The transaction id
        /// </summary>
        [JsonProperty("txnId")]
        public string TransactionId { get; set; } = string.Empty;
    }
}
