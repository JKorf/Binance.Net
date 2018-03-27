using Binance.Net.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Binance.Net.Objects
{
    public class BinanceSymbol
    {
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Name { get; set; }
        /// <summary>
        /// The status of the symbol
        /// </summary>
        [JsonConverter(typeof(SymbolStatusConverter))]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// The base asset
        /// </summary>
        public string BaseAsset { get; set; }
        /// <summary>
        /// The precision of the base asset
        /// </summary>
        public string BaseAssetPrecision { get; set; }
        /// <summary>
        /// The quote asset
        /// </summary>
        public string QuoteAsset { get; set; }
        /// <summary>
        /// The precision of the quote asset
        /// </summary>
        public string QuoteAssetPrecision { get; set; }
        /// <summary>
        /// Allowed order types
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public OrderType[] OrderTypes { get; set; }
        /// <summary>
        /// Ice berg orders allowed
        /// </summary>
        public bool IceBergAllowed { get; set; }
        /// <summary>
        /// Filters for order on this symbol
        /// </summary>
        public BinanceSymbolFilter[] Filters { get; set; }
    }
}
