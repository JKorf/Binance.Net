using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Transfer Futures Transactions
    /// </summary>
    public class BinanceBrokerageTransferFuturesTransactions
    {
        /// <summary>
        /// Success
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Futures type
        /// </summary>
        public BinanceBrokerageFuturesType FuturesType { get; set; }
        
        /// <summary>
        /// Transfer
        /// </summary>
        [JsonProperty("transfers")]
        public IEnumerable<BinanceBrokerageTransferFuturesTransaction> Transactions { get; set; } = new List<BinanceBrokerageTransferFuturesTransaction>();
    }
    
    /// <summary>
    /// Transfer Futures Transaction
    /// </summary>
    public class BinanceBrokerageTransferFuturesTransaction
    {
        /// <summary>
        /// From Id
        /// </summary>
        public string FromId { get; set; } = "";
        
        /// <summary>
        /// To Id
        /// </summary>
        public string ToId { get; set; } = "";
        
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = "";
        
        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("qty")]
        public decimal Amount { get; set; }
        
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
        /// Date
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Date { get; set; }
    }
}