﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Spot.Futures
{
    /// <summary>
    /// After adjustment rate
    /// </summary>
    public class BinanceCrossCollateralAfterAdjust
    {
        /// <summary>
        /// The rate after adjustment
        /// </summary>
        public decimal AfterCollateralRate { get; set; }
    }
}
