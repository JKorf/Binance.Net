﻿namespace Binance.Net.Objects.Spot.IsolatedMarginData
{
    /// <summary>
    /// Isolated margin symbol info
    /// </summary>
    public class BinanceIsolatedMarginSymbol
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Base asset
        /// </summary>
        public string Base { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset
        /// </summary>
        public string Quote { get; set; } = string.Empty;
        /// <summary>
        /// Margin trade
        /// </summary>
        public bool IsMarginTrade { get; set; }
        /// <summary>
        /// Is buy allowed
        /// </summary>
        public bool IsBuyAllowed { get; set; }
        /// <summary>
        /// Is sell allowed
        /// </summary>
        public bool IsSellAllowed { get; set; }
    }
}
