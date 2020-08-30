﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Binance.Net.Interfaces;
using System;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects.Spot.MarketData
{
    /// <summary>
    /// The order book for a asset
    /// </summary>
    public class BinanceOrderBook : IBinanceOrderBook
    {
        /// <summary>
        /// The symbol of the order book 
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// The ID of the last update
        /// </summary>
        [JsonProperty("lastUpdateId")]
        public long LastUpdateId { get; set; }

        /// <summary>
        /// The id of this update, can be synced with BinanceClient.Spot.GetOrderBook to update the order book
        /// </summary>
        [JsonProperty("U")]
        public long? FirstUpdateId { get; set; }

        /// <summary>
        /// The list of bids
        /// </summary>
        public IEnumerable<BinanceOrderBookEntry> Bids { get; set; } = new List<BinanceOrderBookEntry>();

        /// <summary>
        /// The list of asks
        /// </summary>
        public IEnumerable<BinanceOrderBookEntry> Asks { get; set; } = new List<BinanceOrderBookEntry>();
    }
}
