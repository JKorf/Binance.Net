using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Reward type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SolRewardType>))]
    public enum SolRewardType
    {
        /// <summary>
        /// ["<c>CLAIM</c>"] Claim
        /// </summary>
        [Map("CLAIM")]
        Claim,
        /// <summary>
        /// ["<c>DISTRIBUTE</c>"] Distribute
        /// </summary>
        [Map("DISTRIBUTE")]
        Distribute
    }
}

