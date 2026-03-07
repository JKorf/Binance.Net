using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Earning info
    /// </summary>
    [SerializationModel]
    public record BinanceMiningEarnings
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
        /// ["<c>accountProfits</c>"] Profit items
        /// </summary>
        [JsonPropertyName("accountProfits")]
        public BinanceMiningAccountEarning[] AccountProfits { get; set; } = Array.Empty<BinanceMiningAccountEarning>();
    }

    /// <summary>
    /// Earning info
    /// </summary>
    public record BinanceMiningAccountEarning
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
        /// ["<c>puid</c>"] Sub account id
        /// </summary>
        [JsonPropertyName("puid")]
        public long? SubAccountId { get; set; }
        /// <summary>
        /// ["<c>subName</c>"] Mining account
        /// </summary>
        [JsonPropertyName("subName")]
        public string SubName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
    }
}

