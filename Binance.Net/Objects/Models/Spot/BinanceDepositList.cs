﻿using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Information about a deposit
    /// </summary>
    public record BinanceDeposit
    {
        /// <summary>
        /// Time the deposit was added to Binance
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("insertTime")]
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
        /// The address of the deposit
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// The tag of the address of the deposit
        /// </summary>
        [JsonPropertyName("addressTag")]
        public string AddressTag { get; set; } = string.Empty;
        /// <summary>
        /// The network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// The  id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The transaction id
        /// </summary>
        [JsonPropertyName("txId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// The status of the deposit
        /// </summary>
        [JsonPropertyName("status")]
        public DepositStatus Status { get; set; }

        /// <summary>
        /// The transfer type
        /// </summary>
        [JsonPropertyName("transferType")]
        public WithdrawDepositTransferType TransferType { get; set; }

        /// <summary>
        /// Confirmations
        /// </summary>
        [JsonPropertyName("confirmTimes")]
        public string Confirmations { get; set; } = string.Empty;
        /// <summary>
        /// Network confirmations for unlocking
        /// </summary>
        [JsonPropertyName("unlockConfirm")]
        public int ConfirmationsForUnlock { get; set; }
        /// <summary>
        /// The wallet type
        /// </summary>
        [JsonPropertyName("walletType")]
        [JsonConverter(typeof(EnumConverter))]
        public WalletType WalletType { get; set; }
    }
}
