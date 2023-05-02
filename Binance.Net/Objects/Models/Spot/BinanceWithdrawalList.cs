﻿using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot
{
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
        /// Withdraw order id
        /// </summary>
        public string? WithdrawOrderId { get; set; }
        /// <summary>
        /// The time the withdrawal was applied for
        /// </summary>
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// The quantity of the withdrawal
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
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
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Network that was used
        /// </summary>
        /// <summary>
        /// Confirm times for withdraw
        /// </summary>
        [JsonProperty("confirmNo")]
        public int? ConfirmTimes { get; set; }
        /// <summary>
        /// Transaction private key 
        /// </summary>
        public string txKey { get; set; } = string.Empty;
        /// <summary>
        /// reason for withdrawal failure
        /// </summary>
        public string Info { get; set; } = string.Empty;
        /// <summary>
        /// The wallet type for withdraw
        /// </summary>
        public WalletType? WalletType { get; set; }
        /// <summary>
        /// Complete UTC time when user's asset is deduct from withdrawing, only if status =  6(success)
        /// </summary>
        public DateTime? CompleteTime { get; set; }
        /// <summary>
        /// The status of the withdrawal
        /// </summary>
        [JsonConverter(typeof(WithdrawalStatusConverter))]
        public WithdrawalStatus Status { get; set; }

        /// <summary>
        /// Transfer type: 1 for internal transfer, 0 for external transfer 
        /// </summary>
        [JsonConverter(typeof(WithdrawDepositTransferTypeConverter))]
        public WithdrawDepositTransferType TransferType { get; set; }
    }
}
