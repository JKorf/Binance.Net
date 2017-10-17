using System.Collections.Generic;
using Newtonsoft.Json;
using Binance.Net.Converters;

namespace Binance.Net.Objects
{
    public class BinanceOrderBook
    {
        [JsonProperty("lastUpdateId")]
        public int LastUpdateId { get; set; }
        public List<BinanceOrderBookEntry> Bids { get; set; }
        public List<BinanceOrderBookEntry> Asks { get; set; }
    }

    [JsonConverter(typeof(OrderBookEntryConverter))]
    public class BinanceOrderBookEntry
    {
        public double Price { get; set; }
        public double Quantity { get; set; }
    }
}
