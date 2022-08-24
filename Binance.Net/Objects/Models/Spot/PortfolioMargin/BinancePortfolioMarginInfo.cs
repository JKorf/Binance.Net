﻿using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Models.Spot.PortfolioMargin
{
    /// <summary>
    /// Portfolio margin account info
    /// </summary>
    public class BinancePortfolioMarginInfo
    {
        /// <summary>
        /// Portfolio margin account maintenance margin rate
        /// </summary>
        [JsonProperty("uniMMR")]
        public decimal UniMaintenanceMarginRate { get; set; }
        /// <summary>
        /// Account equity, in USD
        /// </summary>
        public decimal AccountEquity { get; set; }
        /// <summary>
        /// Portfolio margin account maintenance margin, in USD
        /// </summary>
        [JsonProperty("accountMaintMargin")]
        public decimal AccountMaintenanceMargin { get; set; }
        /// <summary>
        /// Account status
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public PortfolioMarginAccountStatus AccountStatus { get; set; }
    }
}