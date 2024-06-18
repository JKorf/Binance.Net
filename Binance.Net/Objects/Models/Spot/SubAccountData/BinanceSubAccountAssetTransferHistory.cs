using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    internal record BinanceSubAccountAssetTransferHistoryList
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("futuresType")]
        [JsonConverter(typeof(EnumConverter))]
        public FuturesAccountType AccountType { get; set; }

        /// <summary>
        /// Transfers
        /// </summary>
        [JsonProperty("transfers")]
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
        [JsonProperty("tranId")]
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
        [JsonProperty("qty")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// The time transaction was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
    }
}
