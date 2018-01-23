using Binance.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    [JsonConverter(typeof(SymbolFilterConverter))]
    public class BinanceSymbolFilter
    {
        public SymbolFilterType FilterType { get; set; }
    }

    public class BinanceSymbolPriceFilter: BinanceSymbolFilter
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal TickSize { get; set; }
    }

    public class BinanceSymbolLotSizeFilter : BinanceSymbolFilter
    {
        public decimal MinQuantity { get; set; }
        public decimal MaxQuantity { get; set; }        
        public decimal StepSize { get; set; }
    }

    public class BinanceSymbolMinNotionalFilter : BinanceSymbolFilter
    {
        public decimal MinNotional { get; set; }
    }
}
