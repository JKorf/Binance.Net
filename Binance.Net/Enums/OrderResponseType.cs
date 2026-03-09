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
        /// ["<c>ACK</c>"] Ack only
        /// </summary>
        [Map("ACK")]
        Acknowledge,
        /// <summary>
        /// ["<c>RESULT</c>"] Resulting order
        /// </summary>
        [Map("RESULT")]
        Result,
        /// <summary>
        /// ["<c>FULL</c>"] Full order info, only valid on SPOT orders  
        /// </summary>
        [Map("FULL")]
        Full
    }
}

