﻿using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of a transfer between spot and futures account
    /// </summary>
    public enum FuturesTransferStatus
    {
        /// <summary>
        /// Pending to execute
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// Successfully transferred
        /// </summary>
        [Map("CONFIRMED")]
        Confirmed,
        /// <summary>
        /// Execution failed
        /// </summary>
        [Map("FAILED")]
        Failed
    }
}
