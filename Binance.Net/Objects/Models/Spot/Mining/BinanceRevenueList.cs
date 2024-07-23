﻿using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Mining
{
    /// <summary>
    /// Revenue list
    /// </summary>
    public record BinanceRevenueList
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
        public IEnumerable<BinanceRevenueItem> AccountProfits { get; set; } = Array.Empty<BinanceRevenueItem>();
    }

    /// <summary>
    /// Revenue
    /// </summary>
    public record BinanceRevenueItem
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Coin
        /// </summary>
        [JsonPropertyName("coinName")]
        public string Coin { get; set; } = string.Empty;
        /// <summary>
        /// Earning type
        /// </summary>
        public BinanceEarningType Type { get; set; }
        /// <summary>
        /// Day hashrate
        /// </summary>
        public decimal DayHashRate { get; set; }
        /// <summary>
        /// Profit quantity
        /// </summary>
        [JsonPropertyName("profitAmount")]
        public decimal ProfitQuantity { get; set; }
        /// <summary>
        /// Hash transfer
        /// </summary>
        public decimal? HashTransfer { get; set; }
        /// <summary>
        /// Transfer quantity
        /// </summary>
        [JsonPropertyName("transferAmount")]
        public decimal? TransferQuantity { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public MinerStatus Status { get; set; }
    }
}
