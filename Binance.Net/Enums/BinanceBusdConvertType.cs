using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Busd convert type
    /// </summary>
    public enum BinanceBusdConvertType
    {
        /// <summary>
        /// Auto convert when deposit
        /// </summary>
        [Map("11")]
        AutoConvertDeposit,
        /// <summary>
        /// Auto convert when withdraw
        /// </summary>
        [Map("32")]
        AutoConvertWithdrawal,
        /// <summary>
        /// In case withdraw failed
        /// </summary>
        [Map("34")]
        WithdrawalFailed,
        /// <summary>
        /// Convert via sapi call
        /// </summary>
        [Map("244")]
        ApiConvert,
        /// <summary>
        /// Busd auto convert job
        /// </summary>
        [Map("254")]
        AutoConvert
    }
}
