namespace Binance.Net.Objects.Models.Futures.AlgoOrders
{
    /// <summary>
    /// Algo order result
    /// </summary>
    public record BinanceAlgoResult: BinanceResult
    {
        /// <summary>
        /// Algo order id
        /// </summary>
        public long AlgoId { get; set; }
        /// <summary>
        /// Successful
        /// </summary>
        public bool Success { get; set; }
    }
}
