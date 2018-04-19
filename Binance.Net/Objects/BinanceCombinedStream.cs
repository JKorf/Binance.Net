using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Represents the binance result for combined data on a single socket connection
    /// See on https://github.com/binance-exchange/binance-official-api-docs/blob/master/web-socket-streams.md
    /// Combined streams
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinanceCombinedStream<T>
    {
        /// <summary>
        /// The stream combined
        /// </summary>
        [JsonProperty("stream")]
        public string Stream { get; set; }

        /// <summary>
        /// The data of stream
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
