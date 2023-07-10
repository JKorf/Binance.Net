using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Internal
{
    internal class BinanceSocketMessage
    {
        [JsonProperty("method")]
        public string Method { get; set; } = "";

        [JsonProperty("id")]
        public int Id { get; set; }
    }

    internal class BinanceSocketRequest : BinanceSocketMessage
    {
        [JsonProperty("params")]
        public string[] Params { get; set; } = Array.Empty<string>();
    }

    internal class BinanceSocketQuery : BinanceSocketMessage
    {
        [JsonProperty("params")]
        public Dictionary<string, object> Params { get; set; } = new Dictionary<string, object>();
    }
}
