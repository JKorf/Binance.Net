using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Mark Price and Funding Rate
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesMarkPrice : IBinanceFuturesMarkPrice
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol the information is about
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>markPrice</c>"] The current market price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>indexPrice</c>"] The current index price
        /// </summary>
        [JsonPropertyName("indexPrice")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// ["<c>lastFundingRate</c>"] The last funding rate
        /// </summary>
        [JsonPropertyName("lastFundingRate")]
        public decimal? FundingRate { get; set; }
        /// <summary>
        /// ["<c>nextFundingTime</c>"] The time the funding rate is applied
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("nextFundingTime")]
        public DateTime NextFundingTime { get; set; }
        /// <summary>
        /// ["<c>estimatedSettlePrice</c>"] Estimated settle price
        /// </summary>
        [JsonPropertyName("estimatedSettlePrice")]
        public decimal? EstimatedSettlePrice { get; set; }

        /// <summary>
        /// ["<c>interestRate</c>"] Interest rate
        /// </summary>
        [JsonPropertyName("interestRate")]
        public decimal? InterestRate { get; set; }
        /// <summary>
        /// ["<c>time</c>"] The data timestamp.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Mark price for Coin-M future
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesCoinMarkPrice : BinanceFuturesMarkPrice
    {
        /// <summary>
        /// ["<c>pair</c>"] The pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
    }
}

