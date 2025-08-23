using CryptoExchange.Net.Objects.Errors;

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
        public BinanceRateLimitError(int code, string message) : base(_errorInfo with { ErrorCodes = [code.ToString()], Message = _errorInfo.Message + ": " + message }, null)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        protected BinanceRateLimitError(ErrorInfo info, Exception? exception) : base(info, exception) { }
    }
}
