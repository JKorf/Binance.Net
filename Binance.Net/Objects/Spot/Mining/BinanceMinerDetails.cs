using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.Mining
{
    /// <summary>
    /// Miner details
    /// </summary>
    public class BinanceMinerDetails
    {
        /// <summary>
        /// Name of the worker
        /// </summary>
        public string WorkerName { get; set; } = "";

        /// <summary>
        /// Data type
        /// </summary>
        public string Type { get; set; } = "";
        /// <summary>
        /// Hash rate data
        /// </summary>
        public IEnumerable<BinanceHashRate> HashRateDatas { get; set; } = new List<BinanceHashRate>();
    }

    /// <summary>
    /// Hash rate
    /// </summary>
    public class BinanceHashRate
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
        /// <summary>
        /// Hashrate
        /// </summary>
        public decimal HashRate { get; set; }
        /// <summary>
        /// Rejected
        /// </summary>
        public decimal Reject { get; set; }
    }
}
