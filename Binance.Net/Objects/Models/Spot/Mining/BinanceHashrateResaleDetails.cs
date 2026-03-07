namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Resale list
    /// </summary>
    [SerializationModel]
    public record BinanceHashrateResaleDetails
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
        /// ["<c>profitTransferDetails</c>"] Transfer details
        /// </summary>
        [JsonPropertyName("profitTransferDetails")]
        public BinanceHashrateResaleDetailsItem[] ProfitTransferDetails { get; set; } = Array.Empty<BinanceHashrateResaleDetailsItem>();
    }

    /// <summary>
    /// Resale item
    /// </summary>
    public record BinanceHashrateResaleDetailsItem
    {
        /// <summary>
        /// ["<c>configId</c>"] Config id
        /// </summary>
        [JsonPropertyName("configId")]
        public long ConfigId { get; set; }
        /// <summary>
        /// ["<c>poolUsername</c>"] From user
        /// </summary>
        [JsonPropertyName("poolUsername")]
        public string PoolUserName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>toPoolUsername</c>"] To user
        /// </summary>
        [JsonPropertyName("toPoolUsername")]
        public string ToPoolUserName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>algoName</c>"] Algorithm
        /// </summary>
        [JsonPropertyName("algoName")]
        public string AlgoName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>hashRate</c>"] Hash rate
        /// </summary>
        [JsonPropertyName("hashRate")]
        public decimal Hashrate { get; set; }
        /// <summary>
        /// ["<c>day</c>"] Start day
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("day")]
        public DateTime Day { get; set; }
        /// <summary>
        /// ["<c>coinName</c>"] Coin name
        /// </summary>
        [JsonPropertyName("coinName")]
        public string Coin { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Transferred income
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
    }
}

