﻿using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Sub Account Deposit Status
    /// </summary>
    public enum SubAccountDepositStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        [Map("0")]
        Pending,

        /// <summary>
        /// Success
        /// </summary>
        [Map("1")]
        Success,

        /// <summary>
        /// Credited but cannot withdraw
        /// </summary>
        [Map("6")]
        CreditedButCannotWithdraw,
    }
}