using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Restrictions for order cancelation
    /// </summary>
    [JsonConverter(typeof(EnumConverter<CancelRestriction>))]
    public enum CancelRestriction
    {
        /// <summary>
        /// ["<c>ONLY_NEW</c>"] Cancel will succeed if the order status is New
        /// </summary>
        [Map("ONLY_NEW")]
        OnlyNew,
        /// <summary>
        /// ["<c>ONLY_PARTIALLY_FILLED</c>"] Cancel will succeed if order status is PartiallyFilled
        /// </summary>
        [Map("ONLY_PARTIALLY_FILLED")]
        OnlyPartiallyFilled
    }
}

