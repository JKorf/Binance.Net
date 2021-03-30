using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Spot.Mining
{
    /// <summary>
    /// Resale list
    /// </summary>
    public class BinanceHashrateResaleList
    {
        /// <summary>
        /// Total number of results
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Revenue items
        /// </summary>
        public IEnumerable<BinanceOtherRevenueItem> AccountProfits { get; set; } = new List<BinanceOtherRevenueItem>();
    }

    /// <summary>
    /// Resale item
    /// </summary>
    public class BinanceHashrateResaleItem
    {
        /// <summary>
        /// Mining id
        /// </summary>
        public int ConfigId { get; set; }
        /// <summary>
        /// From user
        /// </summary>
        public string PoolUserName { get; set; } = "";
        /// <summary>
        /// To user
        /// </summary>
        public string ToPoolUserName { get; set; } = "";
        /// <summary>
        /// Algorithm
        /// </summary>
        public string AlgoName { get; set; } = "";
        /// <summary>
        /// Hash rate
        /// </summary>
        public decimal Hashrate { get; set; }
        /// <summary>
        /// Start day
        /// </summary>
        [JsonConverter(typeof(TimestampStringConverter))]
        public DateTime StartDay { get; set; }
        /// <summary>
        /// End day
        /// </summary>
        [JsonConverter(typeof(TimestampStringConverter))]
        public DateTime EndDay { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(HashrateResaleStatusConverter))]
        public HashrateResaleStatus Status { get; set; }
    }
}
