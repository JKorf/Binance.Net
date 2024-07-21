using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Resale list
    /// </summary>
    public record BinanceHashrateResaleList
    {
        /// <summary>
        /// Total number of results
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Details
        /// </summary>
        [JsonPropertyName("configDetails")]
        public IEnumerable<BinanceHashrateResaleItem> ResaleItmes { get; set; } = Array.Empty<BinanceHashrateResaleItem>();
    }

    /// <summary>
    /// Resale item
    /// </summary>
    public record BinanceHashrateResaleItem
    {
        /// <summary>
        /// Mining id
        /// </summary>
        [JsonPropertyName("configId")]
        public int ConfigId { get; set; }
        /// <summary>
        /// From user
        /// </summary>
        [JsonPropertyName("poolUsername")]
        public string PoolUserName { get; set; } = string.Empty;
        /// <summary>
        /// To user
        /// </summary>
        [JsonPropertyName("toPoolUsername")]
        public string ToPoolUserName { get; set; } = string.Empty;
        /// <summary>
        /// Algorithm
        /// </summary>
        [JsonPropertyName("algoName")]
        public string AlgoName { get; set; } = string.Empty;
        /// <summary>
        /// Hash rate
        /// </summary>
        [JsonPropertyName("hashRate")]
        public decimal Hashrate { get; set; }
        /// <summary>
        /// Start day
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("startDay")]
        public DateTime StartDay { get; set; }
        /// <summary>
        /// End day
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("endDay")]
        public DateTime EndDay { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public HashrateResaleStatus Status { get; set; }
    }
}
