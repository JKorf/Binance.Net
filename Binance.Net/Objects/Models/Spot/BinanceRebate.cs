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
        public int Page { get; set; }
        /// <summary>
        /// Total number of records
        /// </summary>
        public int TotalRecords { get; set; }
        /// <summary>
        /// Total number of pages
        /// </summary>
        [JsonProperty("totalPageNum")]
        public int TotalPages { get; set; }
        /// <summary>
        /// Rebate data for this page
        /// </summary>
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
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Type of rebate
        /// </summary>
        public RebateType Type { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Last udpate time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
    }
}
