using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Transaction status
    /// </summary>
    public enum AutoInvestOneTimeTransactionStatus
    {
        /// <summary>
        /// Success
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// Converting
        /// </summary>
        [Map("CONVERTING")]
        Converting
    }
}
