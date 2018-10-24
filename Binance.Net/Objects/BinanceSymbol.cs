using Binance.Net.Converters;
using Newtonsoft.Json;
using System.Linq;

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
        public int BaseAssetPrecision { get; set; }
        /// <summary>
        /// The quote asset
        /// </summary>
        public string QuoteAsset { get; set; }
        /// <summary>
        /// The precision of the quote asset
        /// </summary>
        [JsonProperty("quotePrecision")]
        public int QuoteAssetPrecision { get; set; }
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

        /// <summary>
        /// Filter for max amount of iceberg parts for this symbol
        /// </summary>
        [JsonIgnore]        
        public BinanceSymbolIcebergPartsFilter IceBergPartsFilter => Filters?.OfType<BinanceSymbolIcebergPartsFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for max accuracy of the quantity for this symbol
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolLotSizeFilter LotSizeFilter => Filters?.OfType<BinanceSymbolLotSizeFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for max algoritmical orders for this symbol
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolMaxAlgoritmicalOrdersFilter MaxAlgoritmicalOrdersFilter => Filters?.OfType<BinanceSymbolMaxAlgoritmicalOrdersFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the minimal size of an order for this symbol
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolMinNotionalFilter MinNotionalFilter => Filters?.OfType<BinanceSymbolMinNotionalFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the max accuracy of the price for this symbol
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolPriceFilter PriceFilter => Filters?.OfType<BinanceSymbolPriceFilter>().FirstOrDefault();

    }
}
