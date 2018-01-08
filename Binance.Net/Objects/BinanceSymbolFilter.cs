using Binance.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceSymbolFilter
    {
        [JsonConverter(typeof(SymbolFilterConverter))]
        public SymbolFilterType FilterType { get; set; }
        [JsonProperty("MinQty")]
        public decimal MinQuantity { get; set; }
        [JsonProperty("MaxQty")]
        public decimal MaxQuantity { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal TickSize { get; set; }
        public decimal StepSize { get; set; }
        public decimal MinNotional { get; set; }
    }
}
