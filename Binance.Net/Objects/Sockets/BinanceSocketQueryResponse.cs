using Newtonsoft.Json;

namespace Binance.Net.Objects.Sockets
{
    internal class BinanceSocketQueryResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
