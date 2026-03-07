using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Open Interest History info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesCoinOpenInterestHistory
    {
        /// <summary>
        /// ["<c>pair</c>"] The symbol the information is about
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>contractType</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contractType")]
        public ContractType ContractType { get; set; }

        /// <summary>
        /// ["<c>sumOpenInterest</c>"] Total open interest
        /// </summary>
        [JsonPropertyName("sumOpenInterest")]
        public decimal SumOpenInterest { get; set; }

        /// <summary>
        /// ["<c>sumOpenInterestValue</c>"] Total open interest value
        /// </summary>
        [JsonPropertyName("sumOpenInterestValue")]
        public decimal SumOpenInterestValue { get; set; }

        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; set; }
    }
}

