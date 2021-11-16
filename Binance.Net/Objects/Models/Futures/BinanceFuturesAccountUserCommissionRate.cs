namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// User commission rate
    /// </summary>
    public class BinanceFuturesAccountUserCommissionRate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Maker commission rate
        /// </summary>
        public decimal MakerCommissionRate { get; set; }
        /// <summary>
        /// Taker commission rate
        /// </summary>
        public decimal TakerCommissionRate { get; set; }
    }
}
