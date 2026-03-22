using Binance.Net.Enums;
using Binance.Net.Objects.Models.Spot;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Information about a futures symbol
    /// </summary>
    public record BinanceFuturesSymbol
    {
        /// <summary>
        /// ["<c>filters</c>"] Filters for order on this symbol
        /// </summary>
        [JsonPropertyName("filters")]
        public BinanceSymbolFilter[] Filters { get; set; } = Array.Empty<BinanceSymbolFilter>();
        /// <summary>
        /// ["<c>contractType</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contractType")]
        public ContractType? ContractType { get; set; }
        /// <summary>
        /// ["<c>maintMarginPercent</c>"] The maintenance margin percent
        /// </summary>
        [JsonPropertyName("maintMarginPercent")]
        public decimal MaintMarginPercent { get; set; }
        /// <summary>
        /// ["<c>pricePrecision</c>"] The price Precision
        /// </summary>
        [JsonPropertyName("pricePrecision")]
        public int PricePrecision { get; set; }
        /// <summary>
        /// ["<c>quantityPrecision</c>"] The quantity precision
        /// </summary>
        [JsonPropertyName("quantityPrecision")]
        public int QuantityPrecision { get; set; }
        /// <summary>
        /// ["<c>requiredMarginPercent</c>"] The required margin percentage
        /// </summary>
        [JsonPropertyName("requiredMarginPercent")]
        public decimal RequiredMarginPercent { get; set; }
        /// <summary>
        /// ["<c>baseAsset</c>"] The base asset
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>marginAsset</c>"] Margin asset
        /// </summary>
        [JsonPropertyName("marginAsset")]
        public string MarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quoteAsset</c>"] The quote asset
        /// </summary>
        [JsonPropertyName("quoteAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>baseAssetPrecision</c>"] The precision of the base asset
        /// </summary>
        [JsonPropertyName("baseAssetPrecision")]
        public int BaseAssetPrecision { get; set; }
        /// <summary>
        /// ["<c>quotePrecision</c>"] The precision of the quote asset
        /// </summary>
        [JsonPropertyName("quotePrecision")]
        public int QuoteAssetPrecision { get; set; }
        /// <summary>
        /// ["<c>orderTypes</c>"] Allowed order types
        /// </summary>
        [JsonPropertyName("orderTypes")]
        public FuturesOrderType[] OrderTypes { get; set; } = Array.Empty<FuturesOrderType>();
        /// <summary>
        /// ["<c>symbol</c>"] The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>pair</c>"] Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>deliveryDate</c>"] Delivery Date
        /// </summary>
        [JsonPropertyName("deliveryDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime DeliveryDate { get; set; }
        /// <summary>
        /// ["<c>onboardDate</c>"] Delivery Date
        /// </summary>
        [JsonPropertyName("onboardDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime ListingDate { get; set; }
        /// <summary>
        /// ["<c>triggerProtect</c>"] Trigger protect
        /// </summary>
        [JsonPropertyName("triggerProtect")]
        public decimal TriggerProtect { get; set; }
        /// <summary>
        /// ["<c>underlyingType</c>"] Currently Empty
        /// </summary>
        [JsonPropertyName("underlyingType")]
        public UnderlyingType UnderlyingType { get; set; }
        /// <summary>
        /// ["<c>underlyingSubType</c>"] Sub types
        /// </summary>
        [JsonPropertyName("underlyingSubType")]
        public string[] UnderlyingSubType { get; set; } = Array.Empty<string>();

        /// <summary>
        /// ["<c>liquidationFee</c>"] Liquidation fee
        /// </summary>
        [JsonPropertyName("liquidationFee")]
        public decimal LiquidationFee { get; set; }
        /// <summary>
        /// ["<c>marketTakeBound</c>"] The max price difference rate (from mark price) a market order can make
        /// </summary>
        [JsonPropertyName("marketTakeBound")]
        public decimal MarketTakeBound { get; set; }
        /// <summary>
        /// ["<c>maxMoveOrderLimit</c>"]
        /// </summary>
        [JsonPropertyName("maxMoveOrderLimit")]
        public decimal? MaxMoveOrderLimit { get; set; }
        /// <summary>
        /// ["<c>timeInForce</c>"] Allowed order time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce[] TimeInForce { get; set; } = Array.Empty<TimeInForce>();
        /// <summary>
        /// ["<c>permissionSets</c>"] Permission sets
        /// </summary>
        [JsonPropertyName("permissionSets")]
        public string[] PermissionSets { get; set; } = Array.Empty<string>();
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
    [SerializationModel]
    public record BinanceFuturesUsdtSymbol : BinanceFuturesSymbol
    {
        /// <summary>
        /// ["<c>status</c>"] The status of the symbol
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }

        /// <summary>
        /// ["<c>settlePlan</c>"] The status of the symbol
        /// </summary>
        [JsonPropertyName("settlePlan")]
        public decimal SettlePlan { get; set; }
    }

    /// <summary>
    /// Information about a futures symbol
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesCoinSymbol : BinanceFuturesSymbol
    {

        /// <summary>
        /// ["<c>contractStatus</c>"] The status of the symbol
        /// </summary>
        [JsonPropertyName("contractStatus")]
        public SymbolStatus Status { get; set; }

        /// <summary>
        /// ["<c>contractSize</c>"] Contract size
        /// </summary>
        [JsonPropertyName("contractSize")]
        public int ContractSize { get; set; }

        /// <summary>
        /// ["<c>equalQtyPrecision</c>"] Equal quantity precision
        /// </summary>
        [JsonPropertyName("equalQtyPrecision")]
        public int EqualQuantityPrecision { get; set; }

    }
}

