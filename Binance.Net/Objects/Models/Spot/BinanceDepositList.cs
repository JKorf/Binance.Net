using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Information about a deposit
    /// </summary>
    [SerializationModel]
    public record BinanceDeposit
    {
        /// <summary>
        /// ["<c>insertTime</c>"] Time the deposit was added to Binance
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("insertTime")]
        public DateTime InsertTime { get; set; }
        /// <summary>
        /// ["<c>completeTime</c>"] Time the deposit was completed
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("completeTime")]
        public DateTime? CompleteTime { get; set; }
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
        /// ["<c>id</c>"] The deposit identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>txId</c>"] The transaction id
        /// </summary>
        [JsonPropertyName("txId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] The status of the deposit
        /// </summary>
        [JsonPropertyName("status")]
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
        /// ["<c>unlockConfirm</c>"] Network confirmations for unlocking
        /// </summary>
        [JsonPropertyName("unlockConfirm")]
        public int ConfirmationsForUnlock { get; set; }
        /// <summary>
        /// ["<c>walletType</c>"] The wallet type
        /// </summary>
        [JsonPropertyName("walletType")]
        public WalletType WalletType { get; set; }

        /// <summary>
        /// ["<c>sourceAddress</c>"] Transaction source address. Note: Please note that the source address returned may not be accurate due to network-specific characteristics. If multiple source addresses found, only the first address will be returned
        /// </summary>
        [JsonPropertyName("sourceAddress")]
        public string? SourceAddress { get; set; }
        /// <summary>
        /// ["<c>travelRuleStatus</c>"] Travel rule status
        /// </summary>
        [JsonPropertyName("travelRuleStatus")]
        public TravelRuleStatus TravelRuleStatus { get; set; }
    }
}

