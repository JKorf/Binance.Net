using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Staking transaction type
    /// </summary>
    public enum StakingTransactionType
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
