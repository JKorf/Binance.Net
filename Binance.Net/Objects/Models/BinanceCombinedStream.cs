namespace Binance.Net.Objects.Models
{
    /// <summary>
    /// Represents the binance result for combined data on a single socket connection
    /// See on https://github.com/binance-exchange/binance-official-api-docs/blob/master/web-socket-streams.md
    /// Combined streams
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record BinanceCombinedStream<T>
    {
        /// <summary>
        /// The stream combined
        /// </summary>
        [JsonPropertyName("stream")]
        public string Stream { get; set; } = string.Empty;

        /// <summary>
        /// The data of stream
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
