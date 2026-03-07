using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Mark price update
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesStreamMarkPrice : BinanceStreamEvent, IBinanceFuturesMarkPrice
    {
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>p</c>"] Mark Price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal MarkPrice { get; set; }

        /// <summary>
        /// ["<c>P</c>"] Estimated Settle Price, only useful in the last hour before the settlement starts
        /// </summary>
        [JsonPropertyName("P")]
        public decimal EstimatedSettlePrice { get; set; }

        /// <summary>
        /// ["<c>r</c>"] Next Funding Rate
        /// </summary>
        [JsonPropertyName("r")]
        public decimal? FundingRate { get; set; }

        /// <summary>
        /// ["<c>T</c>"] Next Funding Time
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime NextFundingTime { get; set; }
    }

    /// <summary>
    /// Mark price update
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesUsdtStreamMarkPrice : BinanceFuturesStreamMarkPrice
    {
        /// <summary>
        /// ["<c>i</c>"] Mark Price
        /// </summary>
        [JsonPropertyName("i")]
        public decimal IndexPrice { get; set; }
    }

    /// <summary>
    /// Mark price update
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesCoinStreamMarkPrice : BinanceFuturesStreamMarkPrice
    {
        /// <summary>
        /// ["<c>P</c>"] Mark Price
        /// </summary>
        [JsonPropertyName("P")]
        public new decimal EstimatedSettlePrice { get; set; }

        /// <summary>
        /// ["<c>i</c>"] Mark Price
        /// </summary>
        [JsonPropertyName("i")]
        public decimal IndexPrice { get; set; }
    }
}

