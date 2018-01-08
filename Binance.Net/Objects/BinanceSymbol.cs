using Binance.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects
{
    public class BinanceSymbol
    {
        [JsonProperty("symbol")]
        public string SymbolName { get; set; }
        [JsonConverter(typeof(SymbolStatusConverter))]
        public SymbolStatus Status { get; set; }
        public string BaseAsset { get; set; }
        public string BaseAssetPrecision { get; set; }
        public string QuoteAsset { get; set; }
        public string QuotePrecision { get; set; }
        [JsonConverter(typeof(OrderTypeConverter))]
        public List<OrderType> OrderTypes { get; set; }
        public bool IceBergAllowed { get; set; }
        public List<BinanceSymbolFilter> Filters { get; set; }
    }
}
