using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Result of placing a withdrawal
    /// </summary>
    public class BinanceWithdrawalPlaced
    {
        /// <summary>
        /// Boolean indicating the success of submitting the withdrawal
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Message describing what went wrong if not successful
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}
