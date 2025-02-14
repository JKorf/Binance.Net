using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Information about a withdrawal
    /// </summary>
    public record BinanceWithdrawal
    {
        /// <summary>
        /// The id of the withdrawal
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Withdraw order id
        /// </summary>
        [JsonPropertyName("withdrawOrderId")]
        public string? WithdrawOrderId { get; set; }
        /// <summary>
        /// The time the withdrawal was applied for
        /// </summary>
        [JsonPropertyName("applyTime")]
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// The quantity of the withdrawal
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The address the asset was withdrawn to
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Tag for the address
        /// </summary>
        [JsonPropertyName("addressTag")]
        public string AddressTag { get; set; } = string.Empty;
        /// <summary>
        /// The transaction id of the withdrawal
        /// </summary>
        [JsonPropertyName("txId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Transaction fee for the withdrawal
        /// </summary>
        [JsonPropertyName("transactionFee")]
        public decimal TransactionFee { get; set; }
        /// <summary>
        /// The asset that was withdrawn
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Network that was used
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Confirm times for withdraw
        /// </summary>
        [JsonPropertyName("confirmNo")]
        public int? ConfirmTimes { get; set; }
        /// <summary>
        /// The status of the withdrawal
        /// </summary>
        [JsonPropertyName("status")]
        public WithdrawalStatus Status { get; set; }

        /// <summary>
        /// Transfer type: 1 for internal transfer, 0 for external transfer 
        /// </summary>
        [JsonPropertyName("transferType")]
        public WithdrawDepositTransferType TransferType { get; set; }
        /// <summary>
        /// Transaction key
        /// </summary>
        [JsonPropertyName("txKey")]
        public string TransactionKey { get; set; } = string.Empty;
        /// <summary>
        /// Info
        /// </summary>
        [JsonPropertyName("info")]
        public string Info { get; set; } = string.Empty;
        /// <summary>
        /// The wallet type the withdrawal was from
        /// </summary>
        [JsonConverter(typeof(PocAOTEnumConverter<WalletType>))]
        [JsonPropertyName("walletType")]
        public WalletType WalletType { get; set; }

        /// <summary>
        /// The time the withdrawal was completed
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("completeTime")]
        public DateTime? CompleteTime { get; set; }
    }
}
