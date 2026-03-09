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
        /// ["<c>0</c>"] Completed
        /// </summary>
        [Map("0")]
        Completed,
        /// <summary>
        /// ["<c>1</c>"] Pending
        /// </summary>
        [Map("1")]
        Pending,
        /// <summary>
        /// ["<c>2</c>"] Failed
        /// </summary>
        [Map("2")]
        Failed,

        /// <summary>
        /// ["<c>4</c>"] Currently unknown what this status represents
        /// </summary>
        [Map("4")]
        Unknown
    }
}

