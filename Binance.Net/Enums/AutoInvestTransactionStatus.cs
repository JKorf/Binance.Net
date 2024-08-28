using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Transaction status
    /// </summary>
    public enum AutoInvestTransactionStatus
    {
        /// <summary>
        /// Success
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("FAILED")]
        Failed,
        /// <summary>
        /// Pending
        /// </summary>
        [Map("PENDING")]
        Pending
    }
}
