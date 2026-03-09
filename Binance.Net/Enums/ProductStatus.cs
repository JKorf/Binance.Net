using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of a product
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ProductStatus>))]
    public enum ProductStatus
    {
        /// <summary>
        /// ["<c>ALL</c>"] All products
        /// </summary>
        [Map("ALL")]
        All,
        /// <summary>
        /// ["<c>SUBSCRIBABLE</c>"] Products which are subscribable
        /// </summary>
        [Map("SUBSCRIBABLE")]
        Subscribable,
        /// <summary>
        /// ["<c>UNSUBSCRIBABLE</c>"] Products which are unsubscribable
        /// </summary>
        [Map("UNSUBSCRIBABLE")]
        Unsubscribable
    }
}

