using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Busd convert type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BusdConvertType>))]
    public enum BusdConvertType
    {
        /// <summary>
        /// ["<c>11</c>"] Auto convert when deposit
        /// </summary>
        [Map("11")]
        AutoConvertDeposit,
        /// <summary>
        /// ["<c>32</c>"] Auto convert when withdraw
        /// </summary>
        [Map("32")]
        AutoConvertWithdrawal,
        /// <summary>
        /// ["<c>34</c>"] In case withdraw failed
        /// </summary>
        [Map("34")]
        WithdrawalFailed,
        /// <summary>
        /// ["<c>244</c>"] Convert via sapi call
        /// </summary>
        [Map("244")]
        ApiConvert,
        /// <summary>
        /// ["<c>254</c>"] Busd auto convert job
        /// </summary>
        [Map("254")]
        AutoConvert
    }
}

