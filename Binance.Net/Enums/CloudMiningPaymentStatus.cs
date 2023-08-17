﻿using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Cloud mining payment status
    /// </summary>
    public enum CloudMiningPaymentStatus
    {
        /// <summary>
        /// Payment
        /// </summary>
        [Map("248")]
        Payment,
        /// <summary>
        /// Refund
        /// </summary>
        [Map("249")]
        Refund
    }
}
