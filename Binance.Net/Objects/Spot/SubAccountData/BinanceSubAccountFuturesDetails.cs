﻿using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.SubAccountData
{
    /// <summary>
    /// Sub account futures details
    /// </summary>
    public class BinanceSubAccountFuturesDetails
    {
        /// <summary>
        /// Email of the sub account
        /// </summary>
        public string Email { get; set; } = "";
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = "";
        /// <summary>
        /// List of asset details
        /// </summary>
        public IEnumerable<BinanceSubAccountFuturesAsset> Assets { get; set; } = new List<BinanceSubAccountFuturesAsset>();
        /// <summary>
        /// Can deposit
        /// </summary>
        public bool CanDeposit { get; set; }
        /// <summary>
        /// Can trade
        /// </summary>
        public bool CanTrade { get; set; }
        /// <summary>
        /// Can withdraw
        /// </summary>
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// Fee tier
        /// </summary>
        public int FeeTier { get; set; }
        /// <summary>
        /// Max amount which can be withdrawn
        /// </summary>
        public decimal MaxWithdrawAmount { get; set; }
        /// <summary>
        /// Total initial margin
        /// </summary>
        public decimal TotalInitialMargin { get; set; }
        /// <summary>
        /// Total maintenance margin
        /// </summary>
        public decimal TotalMaintenanceMargin { get; set; }
        /// <summary>
        /// Total margin balance
        /// </summary>
        public decimal TotalMarginBalance { get; set; }
        /// <summary>
        /// Total open order initial margin
        /// </summary>
        public decimal TotalOpenOrderInitialMargin { get; set; }
        /// <summary>
        /// Total position initial margin
        /// </summary>
        public decimal TotalPositionInitialMargin { get; set; }
        /// <summary>
        /// Total unrealized profit
        /// </summary>
        public decimal TotalUnrealizedProfit { get; set; }
        /// <summary>
        /// Total wallet balance
        /// </summary>
        public decimal TotalWalletBalance { get; set; }
        /// <summary>
        /// Time of the data
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
    }
    
    /// <summary>
    /// Sub account future asset details
    /// </summary>
    public class BinanceSubAccountFuturesAsset
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = "";
        /// <summary>
        /// Initial margin
        /// </summary>
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// Maintenance margin
        /// </summary>
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// Margin balance
        /// </summary>
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// Max amount which can be withdrawn
        /// </summary>
        public decimal MaxWithdrawAmount { get; set; }
        /// <summary>
        /// Open order initial margin
        /// </summary>
        public decimal OpenOrderInitialMargin { get; set; }
        /// <summary>
        /// Position initial margin
        /// </summary>
        public decimal PositionInitialMargin { get; set; }
        /// <summary>
        /// Unrealized profit
        /// </summary>
        public decimal UnrealizedProfit { get; set; }
        /// <summary>
        /// Wallet balance
        /// </summary>
        public decimal WalletBalance { get; set; }
    }
}
