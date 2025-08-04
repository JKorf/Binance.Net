namespace Binance.Net.Objects.Sockets
{
    [SerializationModel]
    internal class BinanceSocketQueryResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
