using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Account info
    /// </summary>
    public class BinanceFuturesCoinAccountInfo
    {
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
        /// Update tier
        /// </summary>
        public int UpdateTier { get; set; }

        /// <summary>
        /// Account assets
        /// </summary>
        public IEnumerable<BinanceFuturesAccountAsset> Assets { get; set; } = new List<BinanceFuturesAccountAsset>();
        /// <summary>
        /// Account positions
        /// </summary>
        public IEnumerable<BinancePositionInfoCoin> Positions { get; set; } = new List<BinancePositionInfoCoin>();
    }
}
