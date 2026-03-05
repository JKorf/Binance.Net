using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Open interest
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesOpenInterest
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The open interest value.
        /// </summary>
        [JsonPropertyName("openInterest")]
        public decimal OpenInterest { get; set; }

        /// <summary>
        /// The data timestamp.
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; set; }
    }

    /// <summary>
    /// Open interest
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesCoinOpenInterest : BinanceFuturesOpenInterest
    {
        /// <summary>
        /// The pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// The contract type
        /// </summary>
        [JsonPropertyName("contractType")]
        public ContractType ContractType { get; set; }
    }

}