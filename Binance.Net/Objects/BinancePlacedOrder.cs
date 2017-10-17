using Binance.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects
{
    public class BinancePlacedOrder
    {
        public string Symbol { get; set; }
        public int OrderId { get; set; }
        public string ClientOrderId { get; set; }
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime TransactTime { get; set; }
    }
}
