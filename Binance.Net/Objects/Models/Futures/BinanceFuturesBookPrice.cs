using Binance.Net.Objects.Models.Spot;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Book price
    /// </summary>
    public class BinanceFuturesBookPrice: BinanceBookPrice
    {
        /// <summary>
        /// Pair
        /// </summary>
        public string Pair { get; set; } = string.Empty;
    }
}
