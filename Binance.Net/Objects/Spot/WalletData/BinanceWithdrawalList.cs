using Binance.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Spot.WalletData
{
    /// <summary>
    /// Wrapper for list of withdrawals
    /// </summary>
    internal class BinanceWithdrawalList
    {
        /// <summary>
        /// The list of withdrawals
        /// </summary>
        [JsonProperty("withdrawList")]
        public IEnumerable<BinanceWithdrawal>? List { get; set; }
        /// <summary>
        /// Boolean indicating if the withdrawal list retrieval was successful
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Message what went wrong if retrieving wasn't successful
        /// </summary>
        [JsonProperty("msg")]
        [JsonOptionalProperty]
        public string? Message { get; set; }
    }

    /// <summary>
    /// Information about a withdrawal
    /// </summary>
    public class BinanceWithdrawal
    {
        /// <summary>
        /// The id of the withdrawal
        /// </summary>
        public string Id { get; set; } = string.Empty;
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
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Tag for the address
        /// </summary>
        public string AddressTag { get; set; } = string.Empty;
        /// <summary>
        /// The transaction id of the withdrawal
        /// </summary>
        [JsonProperty("txId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Transaction fee for the withdrawal
        /// </summary>
        public decimal TransactionFee { get; set; }
        /// <summary>
        /// The asset that was withdrawn
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The status of the withdrawal
        /// </summary>
        [JsonConverter(typeof(WithdrawalStatusConverter))]
        public WithdrawalStatus Status { get; set; }
    }
}
