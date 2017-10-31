using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Object describing the error that occured
    /// </summary>
    public class BinanceError
    {
        /// <summary>
        /// The code corresponding to the error. If the code is 0 the error was generated client side
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// The error message
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}
