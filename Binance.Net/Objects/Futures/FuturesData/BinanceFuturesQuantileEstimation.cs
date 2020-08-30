﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Quantile estimation
    /// </summary>
    public class BinanceFuturesQuantileEstimation
    {
        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// Quantile
        /// </summary>
        public BinanceFuturesAdlQuantile? AdlQuantile{ get; set; }
    }

    /// <summary>
    /// Quantile info
    /// </summary>
    public class BinanceFuturesAdlQuantile
    {
        /// <summary>
        /// Long position
        /// </summary>
        public int Long { get; set; }
        /// <summary>
        /// Short position
        /// </summary>
        public int Short { get; set; }
        /// <summary>
        /// Hedge
        /// </summary>
        public int Hedge { get; set; }
    }
}
