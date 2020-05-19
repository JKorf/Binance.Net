using System;
using System.Collections.Generic;
using System.Text;
using Binance.Net.Objects.Spot.SpotData;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.SubAccountData
{
    internal class BinanceSubAccountAsset
    {
        public bool Success { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; } = "";
        public List<BinanceBalance> Balances { get; set; } = new List<BinanceBalance>();
    }
}
