using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The side of an order
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderSide>))]
    public enum OrderSide
    {
        /// <summary>
        /// ["<c>BUY</c>"] Buy
        /// </summary>
        [Map("BUY")]
        Buy,
        /// <summary>
        /// ["<c>SELL</c>"] Sell
        /// </summary>
        [Map("SELL")]
        Sell
    }
}

