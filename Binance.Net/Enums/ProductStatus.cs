using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of a product
    /// </summary>
    public enum ProductStatus
    {
        /// <summary>
        /// All products
        /// </summary>
        [Map("ALL")]
        All,
        /// <summary>
        /// Products which are subscribable
        /// </summary>
        [Map("SUBSCRIBABLE")]
        Subscribable,
        /// <summary>
        /// Products which are unsubscribable
        /// </summary>
        [Map("UNSUBSCRIBABLE")]
        Unsubscribable
    }
}
