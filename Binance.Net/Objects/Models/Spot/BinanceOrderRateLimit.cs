namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Rate limit info
    /// </summary>
    public class BinanceCurrentRateLimit: BinanceRateLimit
    {
        /// <summary>
        /// The current used amount
        /// </summary>
        public int Count { get; set; }
    }
}
