using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using Binance.Net.Converters;
using Binance.Net.Enums;
using System;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// Information about a futures symbol
    /// </summary>
    public class BinanceFuturesSymbol
    {
        /// <summary>
        /// Filters for order on this symbol
        /// </summary>
        public IEnumerable<BinanceFuturesSymbolFilter> Filters { get; set; } = new List<BinanceFuturesSymbolFilter>();
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonConverter(typeof(ContractTypeConverter))]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// The maintenance margin percent
        /// </summary>
        public decimal MaintMarginPercent { get; set; }
        /// <summary>
        /// The price Precision
        /// </summary>
        public int PricePrecision { get; set; }
        /// <summary>
        /// The quantity precision
        /// </summary>
        public int QuantityPrecision { get; set; }
        /// <summary>
        /// The required margin percent
        /// </summary>
        public decimal RequiredMarginPercent { get; set; }
        /// <summary>
        /// The base asset
        /// </summary>
        public string BaseAsset { get; set; } = "";
        /// <summary>
        /// Margin asset
        /// </summary>
        public string MarginAsset { get; set; } = "";
        /// <summary>
        /// The quote asset
        /// </summary>
        public string QuoteAsset { get; set; } = "";
        /// <summary>
        /// The precision of the base asset
        /// </summary>
        public int BaseAssetPrecision { get; set; }
        /// <summary>
        /// The precision of the quote asset
        /// </summary>
        [JsonProperty("quotePrecision")]
        public int QuoteAssetPrecision { get; set; }
        /// <summary>
        /// Allowed order types
        /// </summary>
        [JsonProperty(ItemConverterType = typeof(OrderTypeConverter))]
        public IEnumerable<OrderType> OrderTypes { get; set; } = new List<OrderType>();
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Name { get; set; } = "";
        /// <summary>
        /// Pair
        /// </summary>
        [JsonProperty("pair")]
        public string Pair { get; set; } = "";
        /// <summary>
        /// Delivery Date
        /// </summary>
        [JsonProperty("deliveryDate"), JsonConverter(typeof(TimestampConverter))]
        public DateTime DeliveryDate { get; set; }
        /// <summary>
        /// Delivery Date
        /// </summary>
        [JsonProperty("onboardDate"), JsonConverter(typeof(TimestampConverter))]
        public DateTime ListingDate { get; set; }
        /// <summary>
        /// Trigger protect
        /// </summary>
        public decimal TriggerProtect { get; set; }
        /// <summary>
        /// Currently Empty
        /// </summary>
        [JsonProperty("underlyingType"), JsonConverter(typeof(UnderlyingTypeConverter))]
        public UnderlyingType UnderlyingType { get; set; }
        /// <summary>
        /// Currently Empty
        /// </summary>
        [JsonIgnore]
        public object[] UnderlyingSupType { get; set; }

        /// <summary>
        /// Allowed order time in force
        /// </summary>
        [JsonProperty(ItemConverterType = typeof(TimeInForceConverter))]
        public IEnumerable<TimeInForce> TimeInForce { get; set; } = new List<TimeInForce>();
        /// <summary>
        /// Filter for the max accuracy of the price for this symbol
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolPriceFilter PriceFilter => Filters.OfType<BinanceSymbolPriceFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for max accuracy of the quantity for this symbol
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolLotSizeFilter LotSizeFilter => Filters.OfType<BinanceSymbolLotSizeFilter>().FirstOrDefault();

        /// <summary>
        /// Filter for max accuracy of the quantity for this symbol, specifically for market orders
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolMarketLotSizeFilter MarketLotSizeFilter => Filters.OfType<BinanceSymbolMarketLotSizeFilter>().FirstOrDefault();

        /// <summary>
        /// Filter for max number of orders for this symbol
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolMaxOrdersFilter MaxOrdersFilter => Filters.OfType<BinanceSymbolMaxOrdersFilter>().FirstOrDefault();

        /// <summary>
        /// Filter for max number of orders for this symbol
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolMaxAlgorithmicOrdersFilter MaxAlgoOrdersFilter => Filters.OfType<BinanceSymbolMaxAlgorithmicOrdersFilter>().FirstOrDefault();

        /// <summary>
        /// Filter for the maximum deviation of the price
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolPercentPriceFilter PricePercentFilter => Filters.OfType<BinanceSymbolPercentPriceFilter>().FirstOrDefault();

        /// <summary>
        /// Filter for the maximum deviation of the price
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolMinNotionalFilter MinNotionalFilter => Filters.OfType<BinanceSymbolMinNotionalFilter>().FirstOrDefault();
    }

    /// <summary>
    /// Information about a futures symbol
    /// </summary>
    public class BinanceFuturesUsdtSymbol: BinanceFuturesSymbol
    {
        /// <summary>
        /// The status of the symbol
        /// </summary>
        [JsonConverter(typeof(SymbolStatusConverter))]
        public SymbolStatus Status { get; set; }

        /// <summary>
        /// The status of the symbol
        /// </summary>
        [JsonProperty("settlePlan")]
        public decimal SettlePlan { get; set; }
    }

    /// <summary>
    /// Information about a futures symbol
    /// </summary>
    public class BinanceFuturesCoinSymbol: BinanceFuturesSymbol
    {

        /// <summary>
        /// The status of the symbol
        /// </summary>
        [JsonConverter(typeof(SymbolStatusConverter))]
        [JsonProperty("contractStatus")]
        public SymbolStatus Status { get; set; }

        /// <summary>
        /// Contract size
        /// </summary>
        public int ContractSize { get; set; }

        /// <summary>
        /// Equal quantity precision
        /// </summary>
        [JsonProperty("equalQtyPrecision")]
        public int EqualQuantityPrecision { get; set; }
       
    }
}
