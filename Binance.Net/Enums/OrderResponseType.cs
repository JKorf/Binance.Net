using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Response type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderResponseType>))]
    public enum OrderResponseType
    {
        /// <summary>
        /// Ack only
        /// </summary>
        [Map("ACK")]
        Acknowledge,
        /// <summary>
        /// Resulting order
        /// </summary>
        [Map("RESULT")]
        Result,
        /// <summary>
        /// Full order info, only valid on SPOT orders  
        /// </summary>
        [Map("FULL")]
        Full
    }
}
