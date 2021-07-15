using System;
using System.Collections.Generic;
using Binance.Net.Objects.Spot.SpotData;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.SubAccountData
{
    internal class BinanceSubAccountAsset
    {
        public bool Success { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; } = string.Empty;
        public IEnumerable<BinanceBalance> Balances { get; set; } = Array.Empty<BinanceBalance>();
    }
}
