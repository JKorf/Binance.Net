using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.BSwap
{
    /// <summary>
    /// Pool liquidity info
    /// </summary>
    public class BinanceBSwapPoolLiquidity
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
        /// Liquidity
        /// </summary>
        public Dictionary<string, decimal> Liquidity { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// Share
        /// </summary>
        public BinancePoolShare Share { get; set; } = new BinancePoolShare();
    }

    /// <summary>
    /// Pool share info
    /// </summary>
    public class BinancePoolShare
    {
        /// <summary>
        /// Share amount
        /// </summary>
        public decimal ShareAmount { get; set; }
        /// <summary>
        /// Share percentage
        /// </summary>
        public decimal SharePercentage { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        public Dictionary<string, decimal> Asset { get; set; } = new Dictionary<string, decimal>();
    }
}
