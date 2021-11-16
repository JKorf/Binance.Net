using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Deposit Transaction
    /// </summary>
    public class BinanceBrokerageSubAccountDepositTransaction
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = string.Empty;
        
        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; } = string.Empty;
        
        /// <summary>
        /// Address Tag
        /// </summary>
        public string AddressTag { get; set; } = string.Empty;
        
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        
        /// <summary>
        /// Date
        /// </summary>
        [JsonProperty("insertTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
        
        /// <summary>
        /// Network
        /// </summary>
        public string Network { get; set; } = string.Empty;
        
        /// <summary>
        /// Status
        /// </summary>
        public BinanceBrokerageSubAccountDepositStatus Status { get; set; }
        
        /// <summary>
        /// Transaction Id
        /// </summary>
        [JsonProperty("txId")]
        public string TransactionId { get; set; } = string.Empty;
        
        /// <summary>
        /// Source Address
        /// </summary>
        public string SourceAddress { get; set; } = string.Empty;
        
        /// <summary>
        /// Confirm Times
        /// </summary>
        public string ConfirmTimes { get; set; } = string.Empty;
    }
}