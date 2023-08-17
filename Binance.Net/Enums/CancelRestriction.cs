using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Restrictions for order cancelation
    /// </summary>
    public enum CancelRestriction
    {
        /// <summary>
        /// Cancel will succeed if the order status is New
        /// </summary>
        [Map("ONLY_NEW")]
        OnlyNew,
        /// <summary>
        /// Cancel will succeed if order status is PartiallyFilled
        /// </summary>
        [Map("ONLY_PARTIALLY_FILLED")]
        OnlyPartiallyFilled
    }
}
