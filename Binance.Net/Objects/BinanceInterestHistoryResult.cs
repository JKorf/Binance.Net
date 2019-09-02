using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Interest history entry info
    /// </summary>
    public class BinanceInterestHistory
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; }
        /// <summary>
        /// The amount of interest
        /// </summary>
        public decimal InterestAmount { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime InterestAccuredTime { get; set; }
        /// <summary>
        /// Interest rate
        /// </summary>
        public decimal InterestRate { get; set; }
        /// <summary>
        /// Principal
        /// </summary>
        public decimal Principal { get; set; }
        /// <summary>
        /// Type of interest
        /// </summary>
        public string Type { get; set; }
    }
}
