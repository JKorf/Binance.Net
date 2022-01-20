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
        /// NOTE: string type, because the value van be 'inf' (infinite)
        /// </summary>
        public string MaxNotionalValue { get; set; } = string.Empty;
        /// <summary>
        /// Symbol the request is for
        /// </summary>
        public string? Symbol { get; set; }
    }

}
