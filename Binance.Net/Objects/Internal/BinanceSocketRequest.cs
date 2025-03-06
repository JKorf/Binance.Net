namespace Binance.Net.Objects.Internal
{
    internal class BinanceSocketMessage
    {
        [JsonPropertyName("method")]
        public string Method { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    [SerializationModel]
    internal class BinanceSocketRequest : BinanceSocketMessage
    {
        [JsonPropertyName("params")]
        public string[] Params { get; set; } = Array.Empty<string>();
    }

    [SerializationModel]
    internal class BinanceSocketQuery : BinanceSocketMessage
    {
        [JsonPropertyName("params")]
        public Dictionary<string, object> Params { get; set; } = new Dictionary<string, object>();
    }
}
