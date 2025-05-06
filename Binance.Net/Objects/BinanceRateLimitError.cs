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
        public BinanceRateLimitError(int? code, string message, Exception? exception) : base(code, message, exception)
        {
        }
    }
}
