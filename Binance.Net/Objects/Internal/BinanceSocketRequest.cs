namespace Binance.Net.Objects.Internal
{
    internal class BinanceSocketMessage
    {
        [JsonPropertyName("method")]
        public string Method { get; set; } = "";

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    internal class BinanceSocketRequest : BinanceSocketMessage
    {
        [JsonPropertyName("params")]
        public string[] Params { get; set; } = Array.Empty<string>();
    }

    internal class BinanceSocketQuery : BinanceSocketMessage
    {
        [JsonPropertyName("params")]
        public Dictionary<string, object> Params { get; set; } = new Dictionary<string, object>();
    }
}
