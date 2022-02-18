using System;
using System.Collections.Generic;

namespace Binance.Net.Objects.Models.Spot.BSwap
{
    /// <summary>
    /// Swap pool info
    /// </summary>
    public class BinanceBSwapPool
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
        /// Assets in the pool
        /// </summary>
        public IEnumerable<string> Assets { get; set; } = Array.Empty<string>();
    }
}
