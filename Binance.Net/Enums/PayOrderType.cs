using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Pay order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PayOrderType>))]
    public enum PayOrderType
    {
        /// <summary>
        /// ["<c>PAY</c>"] C2B Merchant Acquiring Payment
        /// </summary>
        [Map("PAY")]
        Pay,
        /// <summary>
        /// ["<c>PAY_REFUND</c>"] C2B Merchant Acquiring Payment, refund
        /// </summary>
        [Map("PAY_REFUND")]
        PayRefund,
        /// <summary>
        /// ["<c>C2C</c>"] C2C Transfer Payment
        /// </summary>
        [Map("C2C")]
        C2C,
        /// <summary>
        /// ["<c>CRYPTO_BOX</c>"] Crypto box
        /// </summary>
        [Map("CRYPTO_BOX")]
        CryptoBox,
        /// <summary>
        /// ["<c>CRYPTO_BOX_RF</c>"] Crypto box, refund
        /// </summary>
        [Map("CRYPTO_BOX_RF")]
        CryptoBoxRefund,
        /// <summary>
        /// ["<c>C2C_HOLDING</c>"] Transfer to new Binance user
        /// </summary>
        [Map("C2C_HOLDING")]
        C2CHolding,
        /// <summary>
        /// ["<c>C2C_HOLDING_RF</c>"] Transfer to new Binance user, refund
        /// </summary>
        [Map("C2C_HOLDING_RF")]
        C2CHoldingRefund,
        /// <summary>
        /// ["<c>PAYOUT</c>"] B2C Disbursement Payment
        /// </summary>
        [Map("PAYOUT")]
        Payout
    }
}

