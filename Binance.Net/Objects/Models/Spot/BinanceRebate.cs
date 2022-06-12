using System;
using System.Collections.Generic;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Rebates page wrapper
    /// </summary>
    public class BinanceRebateWrapper
    {
        /// <summary>
        /// The current page
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Total number of records
        /// </summary>
        public int TotalRecords { get; set; }
        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Rebate data for this page
        /// </summary>
        public IEnumerable<BinanceRebate> Data { get; set; } = Array.Empty<BinanceRebate>();
    }

    /// <summary>
    /// Rebate info
    /// </summary>
    public class BinanceRebate
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Type of rebate
        /// </summary>
        public RebateType Type { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        public decimal Quantity { get; set; }
        /// <summary>
        /// Last udpate time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
    }
}
