using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Revenue list
    /// </summary>
    [SerializationModel]
    public record BinanceOtherRevenueList
    {
        /// <summary>
        /// ["<c>totalNum</c>"] Total number of results
        /// </summary>
        [JsonPropertyName("totalNum")]
        public int TotalNum { get; set; }
        /// <summary>
        /// ["<c>pageSize</c>"] Page size
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        /// <summary>
        /// ["<c>otherProfits</c>"] Revenue items
        /// </summary>
        [JsonPropertyName("otherProfits")]
        public BinanceOtherRevenueItem[] OtherProfits { get; set; } = Array.Empty<BinanceOtherRevenueItem>();
    }

    /// <summary>
    /// Revenue
    /// </summary>
    public record BinanceOtherRevenueItem
    {
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>coinName</c>"] Coin
        /// </summary>
        [JsonPropertyName("coinName")]
        public string Coin { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] Earning type
        /// </summary>
        [JsonPropertyName("type")]
        public EarningType Type { get; set; }
        /// <summary>
        /// ["<c>profitAmount</c>"] Profit quantity
        /// </summary>
        [JsonPropertyName("profitAmount")]
        public decimal ProfitQuantity { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public MinerStatus Status { get; set; }
    }
}

