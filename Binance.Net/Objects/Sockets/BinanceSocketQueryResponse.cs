namespace Binance.Net
{
    internal partial class BinanceSourceGenerationAggregator
    {
        public Binance.Net.Objects.Sockets.BinanceSocketQueryResponse? BinanceSocketQueryResponse;
    }
}

namespace Binance.Net.Objects.Sockets
{
    internal class BinanceSocketQueryResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
