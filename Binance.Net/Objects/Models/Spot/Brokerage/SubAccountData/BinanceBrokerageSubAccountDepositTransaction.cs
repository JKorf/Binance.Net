using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Deposit Transaction
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageSubAccountDepositTransaction
    {
        /// <summary>
        /// ["<c>subAccountId</c>"] Sub Account Id
        /// </summary>
        [JsonPropertyName("subAccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>address</c>"] Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>addressTag</c>"] Address Tag
        /// </summary>
        [JsonPropertyName("addressTag")]
        public string AddressTag { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// ["<c>coin</c>"] Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>insertTime</c>"] Date
        /// </summary>
        [JsonPropertyName("insertTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ["<c>network</c>"] Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public SubAccountDepositStatus Status { get; set; }

        /// <summary>
        /// ["<c>txId</c>"] Transaction Id
        /// </summary>
        [JsonPropertyName("txId")]
        public string TransactionId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>sourceAddress</c>"] Source Address
        /// </summary>
        [JsonPropertyName("sourceAddress")]
        public string SourceAddress { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>confirmTimes</c>"] Confirm Times
        /// </summary>
        [JsonPropertyName("confirmTimes")]
        public string ConfirmTimes { get; set; } = string.Empty;
    }
}