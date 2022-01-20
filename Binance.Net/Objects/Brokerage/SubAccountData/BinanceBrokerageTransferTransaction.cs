using Binance.Net.Converters;
using Binance.Net.Enums;
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
        public string Id { get; set; } = string.Empty;
        
        /// <summary>
        /// Client Transfer Id
        /// </summary>
        [JsonProperty("clientTranId")]
        public string ClientTransferId { get; set; } = string.Empty;
        
        /// <summary>
        /// From Id
        /// </summary>
        public string FromId { get; set; } = string.Empty;
        
        /// <summary>
        /// To Id
        /// </summary>
        public string ToId { get; set; } = string.Empty;
        
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        
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
        
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(BrokerageTransferTransactionStatusConverter))]
        public BrokerageTransferTransactionStatus Status { get; set; }
    }
}