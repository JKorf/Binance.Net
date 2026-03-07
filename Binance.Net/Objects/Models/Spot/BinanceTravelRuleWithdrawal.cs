using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Information about a withdrawal
    /// </summary>
    public record BinanceTravelRuleWithdrawal
    {
        /// <summary>
        /// ["<c>id</c>"] The withdrawal identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>withdrawOrderId</c>"] Withdraw order id
        /// </summary>
        [JsonPropertyName("withdrawOrderId")]
        public string? WithdrawOrderId { get; set; }
        /// <summary>
        /// ["<c>applyTime</c>"] The time the withdrawal was applied for
        /// </summary>
        [JsonPropertyName("applyTime")]
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] The quantity of the withdrawal
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>address</c>"] The address the asset was withdrawn to
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>addressTag</c>"] Tag for the address
        /// </summary>
        [JsonPropertyName("addressTag")]
        public string AddressTag { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>txId</c>"] The transaction id of the withdrawal
        /// </summary>
        [JsonPropertyName("txId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>transactionFee</c>"] Transaction fee for the withdrawal
        /// </summary>
        [JsonPropertyName("transactionFee")]
        public decimal TransactionFee { get; set; }
        /// <summary>
        /// ["<c>coin</c>"] The asset that was withdrawn
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>network</c>"] Network that was used
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>confirmNo</c>"] Confirm times for withdraw
        /// </summary>
        [JsonPropertyName("confirmNo")]
        public int? ConfirmTimes { get; set; }
        /// <summary>
        /// ["<c>withdrawalStatus</c>"] The status of the withdrawal
        /// </summary>
        [JsonPropertyName("withdrawalStatus")]
        public WithdrawalStatus Status { get; set; }
        /// <summary>
        /// ["<c>travelRuleStatus</c>"] The status of the travel rule approval
        /// </summary>
        [JsonPropertyName("travelRuleStatus")]
        public TravelRuleApproveStatus TravelRuleStatus { get; set; }
        /// <summary>
        /// ["<c>transferType</c>"] Transfer type: 1 for internal transfer, 0 for external transfer 
        /// </summary>
        [JsonPropertyName("transferType")]
        public WithdrawDepositTransferType TransferType { get; set; }
        /// <summary>
        /// ["<c>txKey</c>"] Transaction key
        /// </summary>
        [JsonPropertyName("txKey")]
        public string TransactionKey { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>info</c>"] Info
        /// </summary>
        [JsonPropertyName("info")]
        public string Info { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>walletType</c>"] The wallet type the withdrawal was from
        /// </summary>
        [JsonPropertyName("walletType")]
        public WalletType WalletType { get; set; }

        /// <summary>
        /// ["<c>completeTime</c>"] The time the withdrawal was completed
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("completeTime")]
        public DateTime? CompleteTime { get; set; }
        /// <summary>
        /// ["<c>questionnaire</c>"] Travel rule questionnaire
        /// </summary>
        [JsonPropertyName("questionnaire")]
        public Dictionary<string, object> Questionnaire { get; set; } = new();
        /// <summary>
        /// ["<c>trId</c>"] Travel rule id
        /// </summary>
        [JsonPropertyName("trId")]
        public long TravelRuleId { get; set; }
    }
}

