using Binance.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Wrapper for list of withdrawals
    /// </summary>
    public class BinanceWithdrawalList
    {
        /// <summary>
        /// The list of withdrawals
        /// </summary>
        [JsonProperty("withdrawList")]
        public List<BinanceWithdrawal> List { get; set; }
        /// <summary>
        /// Boolean indicating if the withdrawal list retrieval was successful
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Message what went wrong if retrieving wasn't successful
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; }
    }

    /// <summary>
    /// Information about a withdrawal
    /// </summary>
    public class BinanceWithdrawal
    {
        /// <summary>
        /// The time the withdrawal was successful
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime SuccessTime { get; set; }
        /// <summary>
        /// The time the withdrawal was applied for
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// The amount of the withdrawal
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// The address the asset was withdrawn to
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// The transaction id of the withdrawal
        /// </summary>
        [JsonProperty("txId")]
        public string TransactionId { get; set; }
        /// <summary>
        /// The asset that was withdrawn
        /// </summary>
        public string Asset { get; set; }
        /// <summary>
        /// The status of the withdrawal
        /// </summary>
        [JsonConverter(typeof(WithdrawalStatusConverter))]
        public WithdrawalStatus Status { get; set; }
    }
}
