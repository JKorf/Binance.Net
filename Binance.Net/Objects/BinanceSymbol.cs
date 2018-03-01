using Binance.Net.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

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
        public OrderType[] OrderTypes { get; set; }
        public bool IceBergAllowed { get; set; }
        public BinanceSymbolFilter[] Filters { get; set; }
    }
}
