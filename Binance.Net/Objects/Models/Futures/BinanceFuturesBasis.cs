using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Basis info
    /// </summary>
    public record BinanceFuturesBasis
    {
        /// <summary>
        /// The pair
        /// </summary>
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// Contract type
        /// </summary>
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Futures price
        /// </summary>
        public decimal FuturesPrice { get; set; }
        /// <summary>
        /// Index price
        /// </summary>
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// Basis
        /// </summary>
        public decimal Basis { get; set; }
        /// <summary>
        /// Basis rate
        /// </summary>
        public decimal BasisRate { get; set; }
        /// <summary>
        /// Annualized basis rate
        /// </summary>
        [JsonPropertyName("annualizedBasisRate")]
        public decimal? AnnualizedBasisRate { get; set; }
        /// <summary>
        /// Data timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
