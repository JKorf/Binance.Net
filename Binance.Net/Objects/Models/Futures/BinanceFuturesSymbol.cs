﻿using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Information about a futures symbol
    /// </summary>
    public record BinanceFuturesSymbol
    {
        /// <summary>
        /// Filters for order on this symbol
        /// </summary>
        public IEnumerable<BinanceFuturesSymbolFilter> Filters { get; set; } = Array.Empty<BinanceFuturesSymbolFilter>();
        /// <summary>
        /// Contract type
        /// </summary>
        public ContractType? ContractType { get; set; }
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
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Margin asset
        /// </summary>
        public string MarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// The quote asset
        /// </summary>
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// The precision of the base asset
        /// </summary>
        public int BaseAssetPrecision { get; set; }
        /// <summary>
        /// The precision of the quote asset
        /// </summary>
        [JsonPropertyName("quotePrecision")]
        public int QuoteAssetPrecision { get; set; }
        /// <summary>
        /// Allowed order types
        /// </summary>
        public IEnumerable<FuturesOrderType> OrderTypes { get; set; } = Array.Empty<FuturesOrderType>();
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// Delivery Date
        /// </summary>
        [JsonPropertyName("deliveryDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime DeliveryDate { get; set; }
        /// <summary>
        /// Delivery Date
        /// </summary>
        [JsonPropertyName("onboardDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime ListingDate { get; set; }
        /// <summary>
        /// Trigger protect
        /// </summary>
        public decimal TriggerProtect { get; set; }
        /// <summary>
        /// Currently Empty
        /// </summary>
        [JsonPropertyName("underlyingType")]
        public UnderlyingType UnderlyingType { get; set; }
        /// <summary>
        /// Sub types
        /// </summary>
        public IEnumerable<string> UnderlyingSubType { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Liquidation fee
        /// </summary>
        public decimal LiquidationFee { get; set; }
        /// <summary>
        /// The max price difference rate (from mark price) a market order can make
        /// </summary>
        public decimal MarketTakeBound { get; set; }

        /// <summary>
        /// Currently Empty
        /// </summary>
        [JsonIgnore]
        public object[] UnderlyingSupType { get; set; } = Array.Empty<object>();

        /// <summary>
        /// Allowed order time in force
        /// </summary>
        public IEnumerable<TimeInForce> TimeInForce { get; set; } = Array.Empty<TimeInForce>();
        /// <summary>
        /// Filter for the max accuracy of the price for this symbol
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolPriceFilter? PriceFilter => Filters.OfType<BinanceSymbolPriceFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for max accuracy of the quantity for this symbol
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolLotSizeFilter? LotSizeFilter => Filters.OfType<BinanceSymbolLotSizeFilter>().FirstOrDefault();

        /// <summary>
        /// Filter for max accuracy of the quantity for this symbol, specifically for market orders
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolMarketLotSizeFilter? MarketLotSizeFilter => Filters.OfType<BinanceSymbolMarketLotSizeFilter>().FirstOrDefault();

        /// <summary>
        /// Filter for max number of orders for this symbol
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolMaxOrdersFilter? MaxOrdersFilter => Filters.OfType<BinanceSymbolMaxOrdersFilter>().FirstOrDefault();

        /// <summary>
        /// Filter for max number of orders for this symbol
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolMaxAlgorithmicOrdersFilter? MaxAlgoOrdersFilter => Filters.OfType<BinanceSymbolMaxAlgorithmicOrdersFilter>().FirstOrDefault();

        /// <summary>
        /// Filter for the maximum deviation of the price
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolPercentPriceFilter? PricePercentFilter => Filters.OfType<BinanceSymbolPercentPriceFilter>().FirstOrDefault();

        /// <summary>
        /// Filter for the maximum deviation of the price
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolMinNotionalFilter? MinNotionalFilter => Filters.OfType<BinanceSymbolMinNotionalFilter>().FirstOrDefault();
    }

    /// <summary>
    /// Information about a futures symbol
    /// </summary>
    public record BinanceFuturesUsdtSymbol: BinanceFuturesSymbol
    {
        /// <summary>
        /// The status of the symbol
        /// </summary>
        public SymbolStatus Status { get; set; }

        /// <summary>
        /// The status of the symbol
        /// </summary>
        [JsonPropertyName("settlePlan")]
        public decimal SettlePlan { get; set; }
    }

    /// <summary>
    /// Information about a futures symbol
    /// </summary>
    public record BinanceFuturesCoinSymbol: BinanceFuturesSymbol
    {

        /// <summary>
        /// The status of the symbol
        /// </summary>
        [JsonPropertyName("contractStatus")]
        public SymbolStatus Status { get; set; }

        /// <summary>
        /// Contract size
        /// </summary>
        public int ContractSize { get; set; }

        /// <summary>
        /// Equal quantity precision
        /// </summary>
        [JsonPropertyName("equalQtyPrecision")]
        public int EqualQuantityPrecision { get; set; }
       
    }
}
