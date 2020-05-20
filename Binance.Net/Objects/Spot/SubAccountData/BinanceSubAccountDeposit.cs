using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.SubAccountData
{
    /// <summary>
    /// Information about a deposit
    /// </summary>
    public class BinanceSubAccountDeposit
    {
        /// <summary>
        /// Time the deposit was added to Binance
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime InsertTime { get; set; }
        /// <summary>
        /// The amount deposited
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// The coin deposited
        /// </summary>
        public string Coin { get; set; } = "";
        /// <summary>
        /// The address of the deposit
        /// </summary>
        public string Address { get; set; } = "";
        /// <summary>
        /// The transaction id
        /// </summary>
        [JsonProperty("txId")]
        public string TransactionId { get; set; } = "";
        /// <summary>
        /// The status of the deposit
        /// </summary>
        [JsonConverter(typeof(DepositStatusConverter))]
        public DepositStatus Status { get; set; }
    }
}
