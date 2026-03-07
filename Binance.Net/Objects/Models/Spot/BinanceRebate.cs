using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Rebates page wrapper
    /// </summary>
    [SerializationModel]
    public record BinanceRebateWrapper
    {
        /// <summary>
        /// ["<c>page</c>"] The current page
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; }
        /// <summary>
        /// ["<c>totalRecords</c>"] Total number of records
        /// </summary>
        [JsonPropertyName("totalRecords")]
        public int TotalRecords { get; set; }
        /// <summary>
        /// ["<c>totalPageNum</c>"] Total number of pages
        /// </summary>
        [JsonPropertyName("totalPageNum")]
        public int TotalPages { get; set; }
        /// <summary>
        /// ["<c>data</c>"] Rebate data for this page
        /// </summary>
        [JsonPropertyName("data")]
        public BinanceRebate[] Data { get; set; } = Array.Empty<BinanceRebate>();
    }

    /// <summary>
    /// Rebate info
    /// </summary>
    public record BinanceRebate
    {
        /// <summary>
        /// ["<c>asset</c>"] The asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] Type of rebate
        /// </summary>
        [JsonPropertyName("type")]
        public RebateType Type { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] The last update time.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}

