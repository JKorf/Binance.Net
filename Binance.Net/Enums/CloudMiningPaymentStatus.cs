using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Cloud mining payment status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<CloudMiningPaymentStatus>))]
    public enum CloudMiningPaymentStatus
    {
        /// <summary>
        /// ["<c>248</c>"] Payment
        /// </summary>
        [Map("248")]
        Payment,
        /// <summary>
        /// ["<c>249</c>"] Refund
        /// </summary>
        [Map("249")]
        Refund
    }
}

