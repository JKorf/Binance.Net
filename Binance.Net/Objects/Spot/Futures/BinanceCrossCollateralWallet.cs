﻿using System;
using System.Collections.Generic;
using System.Text;

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
        /// The asset
        /// </summary>
        public string Asset { get; set; } = "";
        /// <summary>
        /// Cross collaterals
        /// </summary>
        public IEnumerable<BinanceCrossCollateralWalletEntry> CrossCollaterals { get; set; } = new List<BinanceCrossCollateralWalletEntry>();
    }

    /// <summary>
    /// Cross collateral data
    /// </summary>
    public class BinanceCrossCollateralWalletEntry
    {
        /// <summary>
        /// Collateral coin
        /// </summary>
        public string CollateralCoin { get; set; } = "";
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
    }
}
