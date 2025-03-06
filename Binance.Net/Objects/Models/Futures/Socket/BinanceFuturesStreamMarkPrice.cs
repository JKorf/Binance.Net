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
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Mark Price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal MarkPrice { get; set; }

        /// <summary>
        /// Estimated Settle Price, only useful in the last hour before the settlement starts
        /// </summary>
        [JsonPropertyName("P")]
        public decimal EstimatedSettlePrice { get; set; }

        /// <summary>
        /// Next Funding Rate
        /// </summary>
        [JsonPropertyName("r")]
        public decimal? FundingRate { get; set; }

        /// <summary>
        /// Next Funding Time
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
        /// Mark Price
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
        /// Mark Price
        /// </summary>
        [JsonPropertyName("P")]
        public new decimal EstimatedSettlePrice { get; set; }

        /// <summary>
        /// Mark Price
        /// </summary>
        [JsonPropertyName("i")]
        public decimal IndexPrice { get; set; }
    }
}
