using Newtonsoft.Json;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Transfer Futures Result
    /// </summary>
    public class BinanceBrokerageTransferFuturesResult
    {
        /// <summary>
        /// Transaction Id
        /// </summary>
        [JsonProperty("txnId")]
        public string Id { get; set; } = "";
        
        /// <summary>
        /// Success
        /// </summary>
        public bool Success { get; set; }
    }
}