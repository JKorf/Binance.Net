using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Earning info
    /// </summary>
    public record BinanceMiningEarnings
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
        /// Profit items
        /// </summary>
        [JsonPropertyName("accountProfits")]
        public IEnumerable<BinanceMiningAccountEarning> AccountProfits { get; set; } = Array.Empty<BinanceMiningAccountEarning>();
    }

    /// <summary>
    /// Earning info
    /// </summary>
    public record BinanceMiningAccountEarning
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Coin
        /// </summary>
        [JsonPropertyName("coinName")]
        public string Coin { get; set; } = string.Empty;
        /// <summary>
        /// Earning type
        /// </summary>
        [JsonPropertyName("type")]
        public EarningType Type { get; set; }
        /// <summary>
        /// Sub account id
        /// </summary>
        [JsonPropertyName("puid")]
        public long? SubAccountId { get; set; }
        /// <summary>
        /// Mining account
        /// </summary>
        [JsonPropertyName("subName")]
        public string SubName { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
    }
}
