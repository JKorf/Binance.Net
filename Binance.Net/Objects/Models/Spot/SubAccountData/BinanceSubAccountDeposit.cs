using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Information about a deposit
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountDeposit
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
        /// ["<c>network</c>"] Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>address</c>"] The address of the deposit
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>addressTag</c>"] The address tag
        /// </summary>
        [JsonPropertyName("addressTag")]
        public string AddressTag { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>txId</c>"] The transaction id
        /// </summary>
        [JsonPropertyName("txId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>confirmTimes</c>"] Confirmation status
        /// </summary>
        [JsonPropertyName("confirmTimes")]
        public string ConfirmTimes { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>transferType</c>"] Transfer type
        /// </summary>
        [JsonPropertyName("transferType")]
        public int TransferType { get; set; }
        /// <summary>
        /// ["<c>status</c>"] The status of the deposit
        /// </summary>
        [JsonPropertyName("status")]
        public DepositStatus Status { get; set; }
    }
}

