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
        public int ConfigId { get; set; }
        /// <summary>
        /// From user
        /// </summary>
        public string PoolUserName { get; set; } = string.Empty;
        /// <summary>
        /// To user
        /// </summary>
        public string ToPoolUserName { get; set; } = string.Empty;
        /// <summary>
        /// Algorithm
        /// </summary>
        public string AlgoName { get; set; } = string.Empty;
        /// <summary>
        /// Hash rate
        /// </summary>
        public decimal Hashrate { get; set; }
        /// <summary>
        /// Start day
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime StartDay { get; set; }
        /// <summary>
        /// End day
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime EndDay { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public HashrateResaleStatus Status { get; set; }
    }
}
