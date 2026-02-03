namespace Binance.Net.Objects.Models
{
    /// <summary>
    /// Websocket API event wrapper
    /// </summary>
    public record BinanceWebsocketApiWrapper
    {
        /// <summary>
        /// Subscription id
        /// </summary>
        [JsonPropertyName("subscriptionId")]
        public long SubscriptionId { get; set; }

    }

    /// <summary>
    /// Represents the binance result for combined data on a single socket connection
    /// See on https://github.com/binance-exchange/binance-official-api-docs/blob/master/web-socket-streams.md
    /// Combined streams
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //[SerializationModel]
    public record BinanceWebsocketApiWrapper<T> : BinanceWebsocketApiWrapper
    {
        /// <summary>
        /// The data of stream
        /// </summary>
        [JsonPropertyName("event")]
        public T Event { get; set; } = default!;
    }
}
