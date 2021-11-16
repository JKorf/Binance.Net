namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Result of setting a countdown timer
    /// </summary>
    public class BinanceFuturesCountDownResult
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Count down time in milliseconds
        /// </summary>
        public int CountDownTime { get; set; }
    }
}
