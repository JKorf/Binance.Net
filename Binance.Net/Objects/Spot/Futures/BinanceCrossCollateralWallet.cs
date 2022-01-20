﻿using System;
using System.Collections.Generic;

namespace Binance.Net.Objects.Spot.Futures
{
    /// <summary>
    /// Cross colateral wallet info
    /// </summary>
    public class BinanceCrossCollateralWallet
    {
        /// <summary>
        /// Total cross collateral
        /// </summary>
        public decimal TotalCrossCollateral { get; set; }
        /// <summary>
        /// Total borrowed
        /// </summary>
        public decimal TotalBorrowed { get; set; }
        /// <summary>
        /// Total interest
        /// </summary>
        public decimal TotalInterest { get; set; }
        /// <summary>
        /// Interest free limit
        /// </summary>
        public decimal InterestFreeLimit { get; set; }
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Cross collaterals
        /// </summary>
        public IEnumerable<BinanceCrossCollateralWalletEntry> CrossCollaterals { get; set; } = Array.Empty<BinanceCrossCollateralWalletEntry>();
    }

    /// <summary>
    /// Cross collateral data
    /// </summary>
    public class BinanceCrossCollateralWalletEntry
    {
        /// <summary>
        /// Loan coin
        /// </summary>
        public string LoanCoin { get; set; } = string.Empty;
        /// <summary>
        /// Collateral coin
        /// </summary>
        public string CollateralCoin { get; set; } = string.Empty;
        /// <summary>
        /// Amount locked
        /// </summary>
        public decimal Locked { get; set; }
        /// <summary>
        /// Loan amount
        /// </summary>
        public decimal LoanAmount { get; set; }
        /// <summary>
        /// Current collateral rate
        /// </summary>
        public decimal CurrentCollateralRate { get; set; }
        /// <summary>
        /// Used interest free limit
        /// </summary>
        public decimal InterestFreeLimitUsed { get; set; }
        /// <summary>
        /// Principal interest
        /// </summary>
        public decimal PrincipalForInterest { get; set; }
        /// <summary>
        /// Interest
        /// </summary>
        public decimal Interest { get; set; }
    }
}
