using CryptoExchange.Net.Objects;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Binance rate limit error
    /// </summary>
    public class BinanceRateLimitError : ServerRateLimitError
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="message"></param>
        public BinanceRateLimitError(string message) : base(message)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public BinanceRateLimitError(int? code, string message, object? data) : base(code, message, data)
        {
        }
    }
}
