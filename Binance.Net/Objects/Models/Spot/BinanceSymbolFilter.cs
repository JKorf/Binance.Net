using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// A filter for order placed on a symbol.
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<BinanceSymbolFilter>))]
    public record BinanceSymbolFilter
    {
        /// <summary>
        /// The type of this filter
        /// </summary>
        [JsonPropertyName("filterType")]
        public SymbolFilterType FilterType { get; set; }
    }

    /// <summary>
    /// Price filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<BinanceSymbolPriceFilter>))]
    public record BinanceSymbolPriceFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The minimal price the order can be for
        /// </summary>
        public decimal MinPrice { get; set; }
        /// <summary>
        /// The max price the order can be for
        /// </summary>
        public decimal MaxPrice { get; set; }
        /// <summary>
        /// The tick size of the price. The price can not have more precision as this and can only be incremented in steps of this.
        /// </summary>
        public decimal TickSize { get; set; }
    }

    /// <summary>
    /// Price percentage filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<BinanceSymbolPercentPriceFilter>))]
    public record BinanceSymbolPercentPriceFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The max factor the price can deviate up
        /// </summary>
        public decimal MultiplierUp { get; set; }
        /// <summary>
        /// The max factor the price can deviate down
        /// </summary>
        public decimal MultiplierDown { get; set; }

        /// <summary>
        /// The amount of minutes the average price of trades is calculated over. 0 means the last price is used
        /// </summary>
        public int? MultiplierDecimal { get; set; }
        /// <summary>
        /// The amount of minutes the average price of trades is calculated over. 0 means the last price is used
        /// </summary>
        public int? AveragePriceMinutes { get; set; }
    }

    /// <summary>
    /// Price percentage filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<BinanceSymbolPercentPriceBySideFilter>))]
    public record BinanceSymbolPercentPriceBySideFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The max factor the price can deviate up for buys
        /// </summary>
        public decimal BidMultiplierUp { get; set; }
        /// <summary>
        /// The max factor the price can deviate up for sells
        /// </summary>
        public decimal AskMultiplierUp { get; set; }
        /// <summary>
        /// The max factor the price can deviate down for buys
        /// </summary>
        public decimal BidMultiplierDown { get; set; }
        /// <summary>
        /// The max factor the price can deviate down for sells
        /// </summary>
        public decimal AskMultiplierDown { get; set; }
        /// <summary>
        /// The amount of minutes the average price of trades is calculated over. 0 means the last price is used
        /// </summary>
        public int AveragePriceMinutes { get; set; }
    }

    /// <summary>
    /// Lot size filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<BinanceSymbolLotSizeFilter>))]
    public record BinanceSymbolLotSizeFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The minimal quantity of an order
        /// </summary>
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// The maximum quantity of an order
        /// </summary>
        public decimal MaxQuantity { get; set; }
        /// <summary>
        /// The tick size of the quantity. The quantity can not have more precision as this and can only be incremented in steps of this.
        /// </summary>
        public decimal StepSize { get; set; }
    }

    /// <summary>
    /// Market lot size filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<BinanceSymbolMarketLotSizeFilter>))]
    public record BinanceSymbolMarketLotSizeFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The minimal quantity of an order
        /// </summary>
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// The maximum quantity of an order
        /// </summary>
        public decimal MaxQuantity { get; set; }
        /// <summary>
        /// The tick size of the quantity. The quantity can not have more precision as this and can only be incremented in steps of this.
        /// </summary>
        public decimal StepSize { get; set; }
    }

    /// <summary>
    /// Min notional filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<BinanceSymbolMinNotionalFilter>))]
    public record BinanceSymbolMinNotionalFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The minimal total quote quantity of an order. This is calculated by Price * Quantity.
        /// </summary>
        public decimal MinNotional { get; set; }

        /// <summary>
        /// Whether or not this filter is applied to market orders. If so the average trade price is used.
        /// </summary>
        public bool? ApplyToMarketOrders { get; set; }

        /// <summary>
        /// The amount of minutes the average price of trades is calculated over for market orders. 0 means the last price is used
        /// </summary>
        public int? AveragePriceMinutes { get; set; }
    }

    /// <summary>
    /// Notional filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<BinanceSymbolNotionalFilter>))]
    public record BinanceSymbolNotionalFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The minimal total quote quantity of an order. This is calculated by Price * Quantity.
        /// </summary>
        public decimal MinNotional { get; set; }

        /// <summary>
        /// The maximum total quote quantity of an order, This is calculated by Price * Quantity
        /// </summary>
        public decimal MaxNotional { get; set; }

        /// <summary>
        /// Whether or not the min notional filter is applied to market orders. If so the average trade price is used.
        /// </summary>
        public bool ApplyMinToMarketOrders { get; set; }

        /// <summary>
        /// Whether or not the max notional filter is applied to market orders. If so the average trade price is used.
        /// </summary>
        public bool ApplyMaxToMarketOrders { get; set; }

        /// <summary>
        /// The amount of minutes the average price of trades is calculated over for market orders. 0 means the last price is used
        /// </summary>
        public int AveragePriceMinutes { get; set; }
    }

    /// <summary>
    ///Max orders filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<BinanceSymbolMaxOrdersFilter>))]
    public record BinanceSymbolMaxOrdersFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The max number of orders for this symbol
        /// </summary>
        public int MaxNumberOrders { get; set; }
    }

    /// <summary>
    /// Max algo orders filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<BinanceSymbolMaxAlgorithmicOrdersFilter>))]
    public record BinanceSymbolMaxAlgorithmicOrdersFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The max number of Algorithmic orders for this symbol
        /// </summary>
        public int MaxNumberAlgorithmicOrders { get; set; }
    }

    /// <summary>
    /// Max iceberg parts filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<BinanceSymbolIcebergPartsFilter>))]
    public record BinanceSymbolIcebergPartsFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The max parts of an iceberg order for this symbol.
        /// </summary>
        public int Limit { get; set; }
    }

    /// <summary>
    /// Max position filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<BinanceSymbolMaxPositionFilter>))]
    public record BinanceSymbolMaxPositionFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The MaxPosition filter defines the allowed maximum position an account can have on the base asset of a symbol.
        /// </summary>
        public decimal MaxPosition { get; set; }
    }

    /// <summary>
    /// Trailing delta filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<BinanceSymbolTrailingDeltaFilter>))]
    public record BinanceSymbolTrailingDeltaFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The MinTrailingAboveDelta filter defines the minimum amount in Basis Point or BIPS above the price to activate the order.
        /// </summary>
        public int MinTrailingAboveDelta { get; set; }
        /// <summary>
        /// The MaxTrailingAboveDelta filter defines the maximum amount in Basis Point or BIPS above the price to activate the order.
        /// </summary>
        public int MaxTrailingAboveDelta { get; set; }
        /// <summary>
        /// The MinTrailingBelowDelta filter defines the minimum amount in Basis Point or BIPS below the price to activate the order.
        /// </summary>
        public int MinTrailingBelowDelta { get; set; }
        /// <summary>
        /// The MaxTrailingBelowDelta filter defines the minimum amount in Basis Point or BIPS below the price to activate the order.
        /// </summary>
        public int MaxTrailingBelowDelta { get; set; }
    }
    
    /// <summary>
    /// Max Iceberg Orders Filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<BinanceMaxNumberOfIcebergOrdersFilter>))]
    public record BinanceMaxNumberOfIcebergOrdersFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// Maximum number of iceberg orders for this symbol
        /// </summary>
        public int MaxNumIcebergOrders { get; set; }
    }
}
