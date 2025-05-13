namespace Binance.Net.Objects.Internal
{
    [SerializationModel]
    internal class BinanceExchangeApiWrapper<T>
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("messageDetail")]
        public string MessageDetail { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;

        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
