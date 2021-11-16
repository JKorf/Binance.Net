using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    internal class BinanceSubAccountAsset
    {
        public bool Success { get; set; } = true;
        [JsonProperty("msg")]
        public string Message { get; set; } = string.Empty;
        public IEnumerable<BinanceBalance> Balances { get; set; } = Array.Empty<BinanceBalance>();
    }
}
