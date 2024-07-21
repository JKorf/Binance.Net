using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    internal record BinanceSubAccountAssetTransferHistoryList
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("futuresType")]
        [JsonConverter(typeof(EnumConverter))]
        public FuturesAccountType AccountType { get; set; }

        /// <summary>
        /// Transfers
        /// </summary>
        [JsonPropertyName("transfers")]
        public IEnumerable<BinanceSubAccountAssetTransferHistory> Transfers { get; set; } =
            new List<BinanceSubAccountAssetTransferHistory>();

    }

    /// <summary>
    /// Binance sub account transfer
    /// </summary>
    public record BinanceSubAccountAssetTransferHistory
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }

        /// <summary>
        /// From email
        /// </summary>
        public string From { get; set; } = "";

        /// <summary>
        /// To email
        /// </summary>
        public string To { get; set; } = "";

        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = "";

        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// The time transaction was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }
}
