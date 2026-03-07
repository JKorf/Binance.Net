using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Basis info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesBasis
    {
        /// <summary>
        /// ["<c>pair</c>"] The pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contractType</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contractType")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// ["<c>futuresPrice</c>"] Futures price
        /// </summary>
        [JsonPropertyName("futuresPrice")]
        public decimal FuturesPrice { get; set; }
        /// <summary>
        /// ["<c>indexPrice</c>"] Index price
        /// </summary>
        [JsonPropertyName("indexPrice")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// ["<c>basis</c>"] Basis
        /// </summary>
        [JsonPropertyName("basis")]
        public decimal Basis { get; set; }
        /// <summary>
        /// ["<c>basisRate</c>"] Basis rate
        /// </summary>
        [JsonPropertyName("basisRate")]
        public decimal BasisRate { get; set; }
        /// <summary>
        /// ["<c>annualizedBasisRate</c>"] Annualized basis rate
        /// </summary>
        [JsonPropertyName("annualizedBasisRate")]
        public decimal? AnnualizedBasisRate { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Data timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}

