using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Reward type
    /// </summary>
    public enum SolRewardType
    {
        /// <summary>
        /// Claim
        /// </summary>
        [Map("CLAIM")]
        Claim,
        /// <summary>
        /// Distribute
        /// </summary>
        [Map("DISTRIBUTE")]
        Distribute
    }
}
