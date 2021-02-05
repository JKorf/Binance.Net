using System;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.MarketStream
{
    /// <summary>
    /// Mark price update
    /// </summary>
    public class BinanceFuturesStreamMarkPrice: BinanceStreamEvent, IBinanceFuturesMarkPrice
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Mark Price
        /// </summary>
        [JsonProperty("p")]
        public decimal MarkPrice { get; set; }

        /// <summary>
        /// Estimated Settle Price, only useful in the last hour before the settlement starts
        /// </summary>
        [JsonProperty("P")]
        public decimal EstimatedSettlePrice { get; set; }

        /// <summary>
        /// Next Funding Rate
        /// </summary>
        [JsonProperty("r")]
        public decimal? FundingRate { get; set; }
        
        /// <summary>
        /// Next Funding Time
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public DateTime NextFundingTime { get; set; }
    }

    /// <summary>
    /// Mark price update
    /// </summary>
    public class BinanceFuturesUsdtStreamMarkPrice : BinanceFuturesStreamMarkPrice
    {
        /// <summary>
        /// Mark Price
        /// </summary>
        [JsonProperty("i")]
        public decimal IndexPrice { get; set; }
    }

    /// <summary>
    /// Mark price update
    /// </summary>
    public class BinanceFuturesCoinStreamMarkPrice : BinanceFuturesStreamMarkPrice
    {
        /// <summary>
        /// Mark Price
        /// </summary>
        [JsonProperty("P")]
        public decimal EstimatedSettlePrice { get; set; }
    }
}
