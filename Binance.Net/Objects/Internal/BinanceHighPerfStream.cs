namespace Binance.Net.Objects.Internal
{
    internal class BinanceHighPerfStream<T>
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

        /// <summary>
        /// Id for mapping sub response
        /// </summary>
        [JsonPropertyName("id")]
        public int? Id { get; set; }
    }
}
