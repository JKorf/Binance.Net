namespace Binance.Net.Objects
{
    /// <summary>
    /// The result of an Api call
    /// </summary>
    /// <typeparam name="T">The result type</typeparam>
    public class BinanceApiResult<T>
    {
        /// <summary>
        /// Whether the Api call was successful
        /// </summary>
        public bool Success { get; internal set; }
        /// <summary>
        /// The result of the Api call
        /// </summary>
        public T Data { get; internal set; }
        /// <summary>
        /// The error if the call wasn't successful
        /// </summary>
        public BinanceError Error { get; internal set; }
    }
}
