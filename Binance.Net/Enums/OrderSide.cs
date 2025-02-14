using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The side of an order
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<OrderSide>))] public  enum OrderSide
    {
        /// <summary>
        /// Buy
        /// </summary>
        [Map("BUY")]
        Buy,
        /// <summary>
        /// Sell
        /// </summary>
        [Map("SELL")]
        Sell
    }
}
