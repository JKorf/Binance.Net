namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Price statistics of the last 24 hours
    /// </summary>
    [SerializationModel]
    public interface IBinanceTick : IBinance24HPrice
    {
        /// <summary>
        /// The close price 24 hours ago
        /// </summary>
        decimal PrevDayClosePrice { get; set; }
        /// <summary>
        /// The best bid price in the order book
        /// </summary>
        decimal BestBidPrice { get; set; }
        /// <summary>
        /// The quantity of the best bid price in the order book
        /// </summary>
        decimal BestBidQuantity { get; set; }
        /// <summary>
        /// The best ask price in the order book
        /// </summary>
        decimal BestAskPrice { get; set; }
        /// <summary>
        /// The quantity of the best ask price in the order book
        /// </summary>
        decimal BestAskQuantity { get; set; }
    }
}
