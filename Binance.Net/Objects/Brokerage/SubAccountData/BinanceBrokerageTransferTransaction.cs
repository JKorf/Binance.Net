using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Transfer Transaction
    /// </summary>
    public class BinanceBrokerageTransferTransaction
    {
        /// <summary>
        /// Transaction Id
        /// </summary>
        [JsonProperty("txnId")]
        public string Id { get; set; } = "";
        
        /// <summary>
        /// Client Transfer Id
        /// </summary>
        [JsonProperty("clientTranId")]
        public string ClientTransferId { get; set; } = "";
        
        /// <summary>
        /// From Id
        /// </summary>
        [JsonProperty("fromId")]
        public string FromId { get; set; } = "";
        
        /// <summary>
        /// To Id
        /// </summary>
        [JsonProperty("toId")]
        public string ToId { get; set; } = "";
        
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = "";
        
        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("qty")]
        public decimal Amount { get; set; }
        
        /// <summary>
        /// Date
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Date { get; set; }
    }
}