using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects
{
    public class BinanceOrderTrade
    {
        public decimal Price { get; set; }
        [JsonProperty("qty")]
        public decimal Quantity { get; set; }
        public decimal Commission { get; set; }
        public string CommissionAsset { get; set; }
    }
}
