using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Resale list
    /// </summary>
    [SerializationModel]
    public record BinanceHashrateResaleList
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
        /// ["<c>configDetails</c>"] Details
        /// </summary>
        [JsonPropertyName("configDetails")]
        public BinanceHashrateResaleItem[] ResaleItmes { get; set; } = Array.Empty<BinanceHashrateResaleItem>();
    }

    /// <summary>
    /// Resale item
    /// </summary>
    public record BinanceHashrateResaleItem
    {
        /// <summary>
        /// ["<c>configId</c>"] Mining id
        /// </summary>
        [JsonPropertyName("configId")]
        public int ConfigId { get; set; }
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
        /// ["<c>startDay</c>"] Start day
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("startDay")]
        public DateTime StartDay { get; set; }
        /// <summary>
        /// ["<c>endDay</c>"] End day
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("endDay")]
        public DateTime EndDay { get; set; }

        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public HashrateResaleStatus Status { get; set; }
    }
}

