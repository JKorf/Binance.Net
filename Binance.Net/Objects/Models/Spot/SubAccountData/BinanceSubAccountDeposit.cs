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
        /// The quantity deposited
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The asset deposited
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Network
        /// </summary>
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// The address of the deposit
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// The address tag
        /// </summary>
        public string AddressTag { get; set; } = string.Empty;
        /// <summary>
        /// The transaction id
        /// </summary>
        [JsonProperty("txId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Confirmation status
        /// </summary>
        public string ConfirmTimes { get; set; } = string.Empty;
        /// <summary>
        /// Transfer type
        /// </summary>
        public int TransferType { get; set; }
        /// <summary>
        /// The status of the deposit
        /// </summary>
        [JsonConverter(typeof(DepositStatusConverter))]
        public DepositStatus Status { get; set; }
    }
}
