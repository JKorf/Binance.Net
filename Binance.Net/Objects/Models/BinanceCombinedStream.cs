using Binance.Net.Converters;
using Binance.Net.Objects.Models;

namespace Binance.Net.Objects.Models
{
    public record BinanceCombinedStream
    {
        /// <summary>
        /// The stream combined
        /// </summary>
        [JsonPropertyName("stream")]
        public string Stream { get; set; } = string.Empty;

    }

    /// <summary>
    /// Represents the binance result for combined data on a single socket connection
    /// See on https://github.com/binance-exchange/binance-official-api-docs/blob/master/web-socket-streams.md
    /// Combined streams
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //[SerializationModel]
    public record BinanceCombinedStream<T> : BinanceCombinedStream
    {
        /// <summary>
        /// The data of stream
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
