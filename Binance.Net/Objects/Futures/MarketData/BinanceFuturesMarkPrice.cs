using Newtonsoft.Json;
using CryptoExchange.Net.Converters;
using System;
using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// Mark Price and Funding Rate
    /// </summary>
    public class BinanceFuturesMarkPrice : IBinanceFuturesMarkPrice
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The current market price
        /// </summary>
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// The current index price
        /// </summary>
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// The last funding rate
        /// </summary>
        [JsonProperty("lastFundingRate")]
        public decimal? FundingRate { get; set; }
        /// <summary>
        /// The time the funding rate is applied
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime NextFundingTime { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
    }

    /// <summary>
    /// Mark price for Coin-M future
    /// </summary>
    public class BinanceFuturesCoinMarkPrice: BinanceFuturesMarkPrice
    {
        /// <summary>
        /// The pair
        /// </summary>
        public string Pair { get; set; } = "";
        /// <summary>
        /// Estimated settle price
        /// </summary>
        public decimal EstimatedSettlePrice { get; set; }
    }
}
