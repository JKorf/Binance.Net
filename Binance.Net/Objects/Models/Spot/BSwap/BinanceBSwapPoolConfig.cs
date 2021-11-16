using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.BSwap
{
    /// <summary>
    /// Swap pool config
    /// </summary>
    public class BinanceBSwapPoolConfig
    {
        /// <summary>
        /// Id
        /// </summary>
        public int PoolId { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string PoolName { get; set; } = string.Empty;
        /// <summary>
        /// Update time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Pool liquidity
        /// </summary>
        public PoolLiquidity Liquidity { get; set; } = default!;
        /// <summary>
        /// Asset configuration
        /// </summary>
        [JsonProperty("assetConfigure")]
        public Dictionary<string, PoolAssetConfig> AssetConfig { get; set; } = new Dictionary<string, PoolAssetConfig>();
    }

    /// <summary>
    /// Asset config
    /// </summary>
    public class PoolAssetConfig
    {
        /// <summary>
        /// Minimal add
        /// </summary>
        public decimal MinAdd { get; set; }
        /// <summary>
        /// Maximal add
        /// </summary>
        public decimal MaxAdd { get; set; }
        /// <summary>
        /// Minimal swap
        /// </summary>
        public decimal MinSwap { get; set; }
        /// <summary>
        /// Maximal swap
        /// </summary>
        public decimal MaxSwap { get; set; }
    }

    /// <summary>
    /// Liquidity info
    /// </summary>
    public class PoolLiquidity
    {
        /// <summary>
        /// Constant a
        /// </summary>
        public string? ConstantA { get; set; }
        /// <summary>
        /// Minimal redeem share
        /// </summary>
        public decimal MinRedeemShare { get; set; }
        /// <summary>
        /// Slippage tolerance
        /// </summary>
        public decimal SlippageTolerance { get; set; }
    }
}
