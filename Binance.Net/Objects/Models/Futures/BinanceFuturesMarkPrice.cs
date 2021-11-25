using System;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Mark Price and Funding Rate
    /// </summary>
    public class BinanceFuturesMarkPrice : IBinanceFuturesMarkPrice
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
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
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime NextFundingTime { get; set; }
        /// <summary>
        /// Estimated settle price
        /// </summary>
        public decimal? EstimatedSettlePrice { get; set; }

        /// <summary>
        /// Interest rate
        /// </summary>
        public decimal? InterestRate { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Mark price for Coin-M future
    /// </summary>
    public class BinanceFuturesCoinMarkPrice: BinanceFuturesMarkPrice
    {
        /// <summary>
        /// The pair
        /// </summary>
        public string Pair { get; set; } = string.Empty;
    }
}
