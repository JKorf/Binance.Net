namespace Binance.Net.Objects.Spot.MarginData
{
    /// <summary>
    /// The result amount of getting maxBorrowable or maxTransferable 
    /// </summary>
    public class BinanceMarginAmount
    {
        /// <summary>
        /// The amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The borrow limit
        /// </summary>
        public decimal BorrowLimit { get; set; }
    }
}
