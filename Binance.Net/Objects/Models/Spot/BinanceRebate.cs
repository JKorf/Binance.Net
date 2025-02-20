using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Rebates page wrapper
    /// </summary>
    public record BinanceRebateWrapper
    {
        /// <summary>
        /// The current page
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; }
        /// <summary>
        /// Total number of records
        /// </summary>
        [JsonPropertyName("totalRecords")]
        public int TotalRecords { get; set; }
        /// <summary>
        /// Total number of pages
        /// </summary>
        [JsonPropertyName("totalPageNum")]
        public int TotalPages { get; set; }
        /// <summary>
        /// Rebate data for this page
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<BinanceRebate> Data { get; set; } = Array.Empty<BinanceRebate>();
    }

    /// <summary>
    /// Rebate info
    /// </summary>
    public record BinanceRebate
    {
        /// <summary>
        /// The asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Type of rebate
        /// </summary>
        [JsonPropertyName("type")]
        public RebateType Type { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}
