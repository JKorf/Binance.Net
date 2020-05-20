using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Query result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class BinanceResult<T>
    {
        /// <summary>
        /// The data
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Result code
        /// </summary>
        public int Code { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; } = "";
    }
}
