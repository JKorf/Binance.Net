namespace Binance.Net.Objects.Models.Futures.AlgoOrders
{
    /// <summary>
    /// Algo order result
    /// </summary>
    public class BinanceAlgoOrderResult: BinanceResult
    {
        /// <summary>
        /// Order id
        /// </summary>
        public string ClientAlgoId { get; set; } = string.Empty;
        /// <summary>
        /// Successful
        /// </summary>
        public bool Success { get; set; }
    }
}
