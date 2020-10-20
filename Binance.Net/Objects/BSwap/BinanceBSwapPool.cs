using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.BSwap
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
        public string PoolName { get; set; } = "";
        /// <summary>
        /// Assets in the pool
        /// </summary>
        public IEnumerable<string> Assets { get; set; } = new List<string>();
    }
}
