namespace Binance.Net.Enums
{
    /// <summary>
    /// Response type
    /// </summary>
    public enum OrderResponseType
    {
        /// <summary>
        /// Ack only
        /// </summary>
        Acknowledge,
        /// <summary>
        /// Resulting order
        /// </summary>
        Result,
        /// <summary>
        /// Full order info, only valid on SPOT orders  
        /// </summary>
        Full
    }
}
