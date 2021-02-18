using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Transfer Transaction Universal
    /// </summary>
    public class BinanceBrokerageTransferTransactionUniversal
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
        /// To id
        /// </summary>
        public string ToId { get; set; } = "";
        
        /// <summary>
        /// From account type
        /// </summary>
        [JsonConverter(typeof(BrokerageAccountTypeConverter))]
        public BrokerageAccountType FromAccountType { get; set; }
        
        /// <summary>
        /// To account type
        /// </summary>
        [JsonConverter(typeof(BrokerageAccountTypeConverter))]
        public BrokerageAccountType ToAccountType { get; set; }
        
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