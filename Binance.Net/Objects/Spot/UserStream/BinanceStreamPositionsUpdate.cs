using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.UserStream
{
    /// <summary>
    /// Positions update
    /// </summary>
    public class BinanceStreamPositionsUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// Time of last account update
        /// </summary>
        [JsonProperty("u"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
        /// <summary>
        /// Balances
        /// </summary>
        [JsonProperty("B")]
        public IEnumerable<BinanceStreamBalance> Balances { get; set; } = Array.Empty<BinanceStreamBalance>();
    }
}
