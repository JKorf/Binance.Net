using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceBookPrice
    {
        public string Symbol { get; set; }
        public double BidPrice { get; set; }
        [JsonProperty("bidQty")]
        public double BidQuantity { get; set; }
        public double AskPrice { get; set; }
        [JsonProperty("askQty")]
        public double AskQuantity { get; set; }
    }
}
