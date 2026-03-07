using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Travel rule deposit
    /// </summary>
    public record BinanceTravelRuleDeposit
    {
        /// <summary>
        /// ["<c>insertTime</c>"] Time the deposit was added to Binance
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("insertTime")]
        public DateTime InsertTime { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] The quantity deposited
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>coin</c>"] The asset deposited
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>address</c>"] The address of the deposit
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>addressTag</c>"] The tag of the address of the deposit
        /// </summary>
        [JsonPropertyName("addressTag")]
        public string AddressTag { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>network</c>"] The network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>depositId</c>"] The deposit identifier.
        /// </summary>
        [JsonPropertyName("depositId")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>txId</c>"] The transaction id
        /// </summary>
        [JsonPropertyName("txId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>depositStatus</c>"] The status of the deposit
        /// </summary>
        [JsonPropertyName("depositStatus")]
        public DepositStatus Status { get; set; }

        /// <summary>
        /// ["<c>transferType</c>"] The transfer type
        /// </summary>
        [JsonPropertyName("transferType")]
        public WithdrawDepositTransferType TransferType { get; set; }

        /// <summary>
        /// ["<c>confirmTimes</c>"] Confirmations
        /// </summary>
        [JsonPropertyName("confirmTimes")]
        public string Confirmations { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>requireQuestionnaire</c>"] Requires questionnaire
        /// </summary>
        [JsonPropertyName("requireQuestionnaire")]
        public bool RequireQuestionnaire { get; set; }
        /// <summary>
        /// ["<c>questionnaire</c>"] Questionnaire answers
        /// </summary>
        [JsonPropertyName("questionnaire")]
        public Dictionary<string, object>? Questionnaire { get; set; }
    }
}

