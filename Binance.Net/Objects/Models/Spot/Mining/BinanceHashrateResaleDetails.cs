namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Resale list
    /// </summary>
    public record BinanceHashrateResaleDetails
    {
        /// <summary>
        /// Total number of results
        /// </summary>
        [JsonPropertyName("totalNum")]
        public int TotalNum { get; set; }
        /// <summary>
        /// Page size
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        /// <summary>
        /// Transfer details
        /// </summary>
        [JsonPropertyName("profitTransferDetails")]
        public IEnumerable<BinanceHashrateResaleDetailsItem> ProfitTransferDetails { get; set; } = Array.Empty<BinanceHashrateResaleDetailsItem>();
    }

    /// <summary>
    /// Resale item
    /// </summary>
    public record BinanceHashrateResaleDetailsItem
    {
        /// <summary>
        /// Config id
        /// </summary>
        [JsonPropertyName("configId")]
        public long ConfigId { get; set; }
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
        [JsonPropertyName("alsoName")]
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
        [JsonPropertyName("day")]
        public DateTime Day { get; set; }
        /// <summary>
        /// Coin name
        /// </summary>
        [JsonPropertyName("coinName")]
        public string Coin { get; set; } = string.Empty;
        /// <summary>
        /// Transferred income
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
    }
}
