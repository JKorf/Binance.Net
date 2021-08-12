namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Price statistics of the last 24 hours
    /// </summary>
    public interface IBinanceTick : IBinance24HPrice
    {
        /// <summary>
        /// The close price 24 hours ago
        /// </summary>
        decimal PrevDayClosePrice { get; set; }
        /// <summary>
        /// The best bid price in the order book
        /// </summary>
        decimal BidPrice { get; set; }
        /// <summary>
        /// The size of the best bid price in the order book
        /// </summary>
        decimal BidQuantity { get; set; }
        /// <summary>
        /// The best ask price in the order book
        /// </summary>
        decimal AskPrice { get; set; }
        /// <summary>
        /// The size of the best ask price in the order book
        /// </summary>
        decimal AskQuantity { get; set; }
    }
}
