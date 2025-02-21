using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    internal record BinanceSubAccountAssetTransferHistoryList
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("futuresType")]
        [JsonConverter(typeof(EnumConverter<FuturesAccountType>))]
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
        [JsonPropertyName("from")]
        public string From { get; set; } = string.Empty;

        /// <summary>
        /// To email
        /// </summary>
        [JsonPropertyName("to")]
        public string To { get; set; } = string.Empty;

        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;

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
