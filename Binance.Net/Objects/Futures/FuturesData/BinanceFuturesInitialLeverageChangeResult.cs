namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Response to the change in initial leverage request
    /// </summary>
    public class BinanceFuturesInitialLeverageChangeResult
    {
        /// <summary>
        /// New leverage multiplier
        /// </summary>
        public int Leverage { get; set; }
        /// <summary>
        /// Maximum value that can be held
        /// </summary>
        public decimal MaxNotionalValue { get; set; }
        /// <summary>
        /// Symbol the request is for
        /// </summary>
        public string? Symbol { get; set; }
    }

}
