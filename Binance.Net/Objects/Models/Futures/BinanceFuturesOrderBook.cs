﻿using Binance.Net.Objects.Models.Spot;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// The order book for a asset
    /// </summary>
    public class BinanceFuturesOrderBook : BinanceOrderBook
    {
        /// <summary>
        /// Pair
        /// </summary>
        public string? Pair { get; set; } = string.Empty;
        /// <summary>
        /// The symbol of the order book 
        /// </summary>
        public new string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The symbol of the order book 
        /// </summary>
        [JsonProperty("E"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime MessageTime { get; set; }

        /// <summary>
        /// The ID of the last update
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTime { get; set; }
    }
}
