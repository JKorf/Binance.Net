using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Symbol info
    /// </summary>
    public record BinanceSymbol
    {
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The status of the symbol
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// The base asset
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// The precision of the base asset
        /// </summary>
        [JsonPropertyName("baseAssetPrecision")]
        public int BaseAssetPrecision { get; set; }
        /// <summary>
        /// The quote asset
        /// </summary>
        [JsonPropertyName("quoteAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// The precision of the quote asset
        /// </summary>
        [JsonPropertyName("quotePrecision")]
        public int QuoteAssetPrecision { get; set; }

        /// <summary>
        /// Allowed order types
        /// </summary>
        [JsonPropertyName("orderTypes")]
        public IEnumerable<SpotOrderType> OrderTypes { get; set; } = Array.Empty<SpotOrderType>();
        /// <summary>
        /// Iceberg orders allowed
        /// </summary>
        [JsonPropertyName("icebergAllowed")]
        public bool IcebergAllowed { get; set; }
        /// <summary>
        /// Cancel replace allowed
        /// </summary>
        [JsonPropertyName("cancelReplaceAllowed")]
        public bool CancelReplaceAllowed { get; set; }
        /// <summary>
        /// Spot trading orders allowed
        /// </summary>
        [JsonPropertyName("isSpotTradingAllowed")]
        public bool IsSpotTradingAllowed { get; set; }
        /// <summary>
        /// Trailing stop orders are allowed
        /// </summary>
        [JsonPropertyName("allowTrailingStop")]
        public bool AllowTrailingStop { get; set; }
        /// <summary>
        /// Margin trading orders allowed
        /// </summary>
        [JsonPropertyName("isMarginTradingAllowed")]
        public bool IsMarginTradingAllowed { get; set; }
        /// <summary>
        /// If OCO(One Cancels Other) orders are allowed
        /// </summary>
        [JsonPropertyName("ocoAllowed")]
        public bool OCOAllowed { get; set; }
        /// <summary>
        /// If OTO(One Triggers Other) orders are allowed
        /// </summary>
        [JsonPropertyName("otoAllowed")]
        public bool OTOAllowed { get; set; }
        /// <summary>
        /// Whether or not it is allowed to specify the quantity of a market order in the quote asset
        /// </summary>
        [JsonPropertyName("quoteOrderQtyMarketAllowed")]
        public bool QuoteOrderQuantityMarketAllowed { get; set; }
        /// <summary>
        /// The precision of the base asset fee
        /// </summary>
        [JsonPropertyName("baseCommissionPrecision")]
        public int BaseFeePrecision { get; set; }
        /// <summary>
        /// The precision of the quote asset fee
        /// </summary>
        [JsonPropertyName("quoteCommissionPrecision")]
        public int QuoteFeePrecision { get; set; }
        /// <summary>
        /// Permissions types
        /// </summary>
        [JsonPropertyName("permissions")]
        public IEnumerable<PermissionType> Permissions { get; set; } = Array.Empty<PermissionType>();
        /// <summary>
        /// Permission sets
        /// </summary>
        [JsonPropertyName("permissionSets"), JsonConverter(typeof(PermissionTypeConverter))]
        public IEnumerable<IEnumerable<PermissionType>> PermissionSets { get; set; } = Array.Empty<IEnumerable<PermissionType>>();

        /// <summary>
        /// Filters for order on this symbol
        /// </summary>
        [JsonPropertyName("filters")]
        public IEnumerable<BinanceSymbolFilter> Filters { get; set; } = Array.Empty<BinanceSymbolFilter>();
        /// <summary>
        /// Default self trade prevention
        /// </summary>
        [JsonPropertyName("defaultSelfTradePreventionMode")]
        [JsonConverter(typeof(EnumConverter))]
        public SelfTradePreventionMode DefaultSelfTradePreventionMode { get; set; }
        /// <summary>
        /// Allowed self trade prevention modes
        /// </summary>
        [JsonPropertyName("allowedSelfTradePreventionModes")]
        public IEnumerable<SelfTradePreventionMode> AllowedSelfTradePreventionModes { get; set; } = Array.Empty<SelfTradePreventionMode>();
        /// <summary>
        /// Filter for max amount of iceberg parts for this symbol
        /// </summary>
        [JsonIgnore]        
        public BinanceSymbolIcebergPartsFilter? IceBergPartsFilter => Filters.OfType<BinanceSymbolIcebergPartsFilter>().FirstOrDefault();
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
        /// Filter for max algorithmic orders for this symbol
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolMaxAlgorithmicOrdersFilter? MaxAlgorithmicOrdersFilter => Filters.OfType<BinanceSymbolMaxAlgorithmicOrdersFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the minimal quote quantity of an order for this symbol
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolMinNotionalFilter? MinNotionalFilter => Filters.OfType<BinanceSymbolMinNotionalFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the minimal quote quantity of an order for this symbol
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolNotionalFilter? NotionalFilter => Filters.OfType<BinanceSymbolNotionalFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the max accuracy of the price for this symbol
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolPriceFilter? PriceFilter => Filters.OfType<BinanceSymbolPriceFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the maximum deviation of the price
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolPercentPriceFilter? PricePercentFilter => Filters.OfType<BinanceSymbolPercentPriceFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the maximum deviation of the price per side
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolPercentPriceBySideFilter? PricePercentByPriceFilter => Filters.OfType<BinanceSymbolPercentPriceBySideFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the maximum position on a symbol
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolMaxPositionFilter? MaxPositionFilter => Filters.OfType<BinanceSymbolMaxPositionFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the trailing delta values
        /// </summary>
        [JsonIgnore]
        public BinanceSymbolTrailingDeltaFilter? TrailingDeltaFilter => Filters.OfType<BinanceSymbolTrailingDeltaFilter>().FirstOrDefault();
    }
}
