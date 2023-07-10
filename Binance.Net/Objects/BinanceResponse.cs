using System.Collections.Generic;
using Binance.Net.Objects.Models.Spot;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Binance response
    /// </summary>
    /// <typeparam name="T">Type of the data</typeparam>
    public class BinanceResponse<T>
    {
        /// <summary>
        /// Data result
        /// </summary>
        public T Result { get; set; } = default!;
        /// <summary>
        /// Rate limit info
        /// </summary>
        public IEnumerable<BinanceCurrentRateLimit> Ratelimits { get; set; } = new List<BinanceCurrentRateLimit>();
    }
}
