using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Pay order type
    /// </summary>
    public enum PayOrderType
    {
        /// <summary>
        /// C2B Merchant Acquiring Payment
        /// </summary>
        [Map("PAY")]
        Pay,
        /// <summary>
        /// C2B Merchant Acquiring Payment, refund
        /// </summary>
        [Map("PAY_REFUND")]
        PayRefund,
        /// <summary>
        /// C2C Transfer Payment
        /// </summary>
        [Map("C2C")]
        C2C,
        /// <summary>
        /// Crypto box
        /// </summary>
        [Map("CRYPTO_BOX")]
        CryptoBox,
        /// <summary>
        /// Crypto box, refund
        /// </summary>
        [Map("CRYPTO_BOX_RF")]
        CryptoBoxRefund,
        /// <summary>
        /// Transfer to new Binance user
        /// </summary>
        [Map("C2C_HOLDING")]
        C2CHolding,
        /// <summary>
        /// Transfer to new Binance user, refund
        /// </summary>
        [Map("C2C_HOLDING_RF")]
        C2CHoldingRefund,
        /// <summary>
        /// B2C Disbursement Payment
        /// </summary>
        [Map("PAYOUT")]
        Payout
    }
}
