﻿using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Information about a deposit
    /// </summary>
    public record BinanceSubAccountDeposit
    {
        /// <summary>
        /// Time the deposit was added to Binance
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime InsertTime { get; set; }
        /// <summary>
        /// The quantity deposited
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The asset deposited
        /// </summary>
        [JsonPropertyName("coin")]
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
        [JsonPropertyName("txId")]
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
        public DepositStatus Status { get; set; }
    }
}
