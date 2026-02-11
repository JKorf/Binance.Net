using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Travel rule approval status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TravelRuleApproveStatus>))]
    public enum TravelRuleApproveStatus
    {
        /// <summary>
        /// Completed
        /// </summary>
        [Map("0")]
        Completed,
        /// <summary>
        /// Pending
        /// </summary>
        [Map("1")]
        Pending,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("2")]
        Failed,

        /// <summary>
        /// Currently unknown what this status represents
        /// </summary>
        [Map("4")]
        Unknown
    }
}
