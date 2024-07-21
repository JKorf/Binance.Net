using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Mark Price and Funding Rate
    /// </summary>
    public record BinanceFuturesMarkPrice : IBinanceFuturesMarkPrice
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The current market price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// The current index price
        /// </summary>
        [JsonPropertyName("indexPrice")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// The last funding rate
        /// </summary>
        [JsonPropertyName("lastFundingRate")]
        public decimal? FundingRate { get; set; }
        /// <summary>
        /// The time the funding rate is applied
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("nextFundingTime")]
        public DateTime NextFundingTime { get; set; }
        /// <summary>
        /// Estimated settle price
        /// </summary>
        [JsonPropertyName("estimatedSettlePrice")]
        public decimal? EstimatedSettlePrice { get; set; }

        /// <summary>
        /// Interest rate
        /// </summary>
        [JsonPropertyName("interestRate")]
        public decimal? InterestRate { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Mark price for Coin-M future
    /// </summary>
    public record BinanceFuturesCoinMarkPrice: BinanceFuturesMarkPrice
    {
        /// <summary>
        /// The pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
    }
}
