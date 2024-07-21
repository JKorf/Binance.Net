using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    internal record BinanceSubAccountUniversalTransfersList
    {
        /// <summary>
        /// Transactions
        /// </summary>
        [JsonPropertyName("result")]
        public IEnumerable<BinanceSubAccountUniversalTransferTransaction> Transactions { get; set; } =
            new List<BinanceSubAccountUniversalTransferTransaction>();

    }

    /// <summary>
    /// Binance sub account universal transaction
    /// </summary>
    public record BinanceSubAccountUniversalTransferTransaction
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }

        /// <summary>
        /// From email
        /// </summary>
        public string FromEmail { get; set; } = "";

        /// <summary>
        /// To email
        /// </summary>
        public string ToEmail { get; set; } = "";

        /// <summary>
        /// From account type
        /// </summary>
        public TransferAccountType FromAccountType { get; set; }

        /// <summary>
        /// To account type
        /// </summary>
        public TransferAccountType ToAccountType { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = "";

        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = "";

        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// The time the universal transaction was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createTimeStamp")]
        public DateTime CreateTime { get; set; }
    }
}
