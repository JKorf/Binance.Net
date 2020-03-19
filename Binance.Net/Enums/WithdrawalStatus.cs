using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The status of a withdrawal
    /// </summary>
    public enum WithdrawalStatus
    {
        /// <summary>
        /// Email has been send
        /// </summary>
        EmailSend,
        /// <summary>
        /// Withdrawal has been canceled
        /// </summary>
        Canceled,
        /// <summary>
        /// Withdrawal is awaiting approval
        /// </summary>
        AwaitingApproval,
        /// <summary>
        /// Withdrawal has been rejected
        /// </summary>
        Rejected,
        /// <summary>
        /// Withdrawal is processing
        /// </summary>
        Processing,
        /// <summary>
        /// Withdrawal has failed
        /// </summary>
        Failure,
        /// <summary>
        /// Withdrawal has been processed
        /// </summary>
        Completed
    }
}
