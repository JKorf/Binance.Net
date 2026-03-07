using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Revenue list
    /// </summary>
    [SerializationModel]
    public record BinanceRevenueList
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
        /// ["<c>accountProfits</c>"] Revenue items
        /// </summary>
        [JsonPropertyName("accountProfits")]
        public BinanceRevenueItem[] AccountProfits { get; set; } = Array.Empty<BinanceRevenueItem>();
    }

    /// <summary>
    /// Revenue
    /// </summary>
    public record BinanceRevenueItem
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
        /// ["<c>dayHashRate</c>"] Day hashrate
        /// </summary>
        [JsonPropertyName("dayHashRate")]
        public decimal DayHashRate { get; set; }
        /// <summary>
        /// ["<c>profitAmount</c>"] Profit quantity
        /// </summary>
        [JsonPropertyName("profitAmount")]
        public decimal ProfitQuantity { get; set; }
        /// <summary>
        /// ["<c>hashTransfer</c>"] Hash transfer
        /// </summary>
        [JsonPropertyName("hashTransfer")]
        public decimal? HashTransfer { get; set; }
        /// <summary>
        /// ["<c>transferAmount</c>"] Transfer quantity
        /// </summary>
        [JsonPropertyName("transferAmount")]
        public decimal? TransferQuantity { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public MinerStatus Status { get; set; }
    }
}

