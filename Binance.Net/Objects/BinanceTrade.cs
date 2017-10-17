using Binance.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects
{
    public class BinanceTrade
    {
        public long Id { get; set; }
        public double Price { get; set; }
        [JsonProperty("qty")]
        public double Quantity { get; set; }
        public double Commission { get; set; }
        public string CommissionAsset { get; set; }
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
        public bool IsBuyer { get; set; }
        public bool IsMaker { get; set; }
        public bool IsBestMatch { get; set; }
    }
}
