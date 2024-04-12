﻿namespace Binance.Net.Objects.Models.Futures
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
        public IEnumerable<BinanceFuturesAccountAsset> Assets { get; set; } = Array.Empty<BinanceFuturesAccountAsset>();
        /// <summary>
        /// Account positions
        /// </summary>
        public IEnumerable<BinancePositionInfoCoin> Positions { get; set; } = Array.Empty<BinancePositionInfoCoin>();
        /// <summary>
        /// Update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
    }
}
