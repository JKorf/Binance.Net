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
        public string SubAccountId { get; set; } = "";
        
        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; } = "";
        
        /// <summary>
        /// Address Tag
        /// </summary>
        public string AddressTag { get; set; } = "";
        
        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }
        
        /// <summary>
        /// Coin
        /// </summary>
        public string Coin { get; set; } = "";
        
        /// <summary>
        /// Date
        /// </summary>
        [JsonProperty("insertTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Network
        /// </summary>
        public string Network { get; set; } = "";
        
        /// <summary>
        /// Status
        /// </summary>
        public BinanceBrokerageSubAccountDepositStatus Status { get; set; }
        
        /// <summary>
        /// Transaction Id
        /// </summary>
        [JsonProperty("txId")]
        public string TransactionId { get; set; } = "";
        
        /// <summary>
        /// Source Address
        /// </summary>
        public string SourceAddress { get; set; } = "";
        
        /// <summary>
        /// Confirm Times
        /// </summary>
        public string ConfirmTimes { get; set; } = "";
    }
}