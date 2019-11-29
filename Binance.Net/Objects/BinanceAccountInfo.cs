﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Information about an account
    /// </summary>
    public class BinanceAccountInfo
    {
        /// <summary>
        /// Commission percentage to pay when making trades
        /// </summary>
        public decimal MakerCommission { get; set; }
        /// <summary>
        /// Commission percentage to pay when taking trades
        /// </summary>
        public decimal TakerCommission { get; set; }
        /// <summary>
        /// Commission percentage to buy when buying
        /// </summary>
        public decimal BuyerCommission { get; set; }
        /// <summary>
        /// Commission percentage to buy when selling
        /// </summary>
        public decimal SellerCommission { get; set; }
        /// <summary>
        /// Boolean indicating if this account can trade
        /// </summary>
        public bool CanTrade { get; set; }
        /// <summary>
        /// Boolean indicating if this account can withdraw
        /// </summary>
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// Boolean indicating if this account can deposit
        /// </summary>
        public bool CanDeposit { get; set; }
        /// <summary>
        /// The time of the update
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// The type of account
        /// </summary>
        // TODO make enum when api docs are updated
        public string AccountType { get; set; } = "";
        /// <summary>
        /// List of assets with their current balances
        /// </summary>
        public IEnumerable<BinanceBalance> Balances { get; set; } = new List<BinanceBalance>();
    }

    /// <summary>
    /// Information about an asset balance
    /// </summary>
    public class BinanceBalance
    {
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        public string Asset { get; set; } = "";
        /// <summary>
        /// The amount that isn't locked in a trade
        /// </summary>
        public decimal Free { get; set; }
        /// <summary>
        /// The amount that is currently locked in a trade
        /// </summary>
        public decimal Locked { get; set; }
        /// <summary>
        /// The total balance of this asset (Free + Locked)
        /// </summary>
        public decimal Total => Free + Locked;
    }
}
