using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Staking transaction type
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<StakingTransactionType>))] public  enum StakingTransactionType
    {
        /// <summary>
        /// Subscription
        /// </summary>
        [Map("SUBSCRIPTION")]
        Subscription,
        /// <summary>
        /// Redemption
        /// </summary>
        [Map("REDEMPTION")]
        Redemption,
        /// <summary>
        /// Interest
        /// </summary>
        [Map("INTEREST")]
        Interest
    }
}
