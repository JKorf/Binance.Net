using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// Asset info
    /// </summary>
    public class BinanceFuturesUsdtAsset
    {
        /// <summary>
        /// Name of the asset
        /// </summary>
        public string Asset { get; set; } = "";
        /// <summary>
        /// Whether the asset can be used as margin in Multi-Assets mode
        /// </summary>
        public bool MarginAvailable { get; set; }
        /// <summary>
        /// Auto-exchange threshold in Multi-Assets margin mode
        /// </summary>
        public decimal AutoAssetExchange { get; set; }
    }
}
