using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Other
{
    internal class BinanceSocketRequest
    {
        [JsonProperty("method")]
        public string Method { get; set; }
        [JsonProperty("params")]
        public string[] Params { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
