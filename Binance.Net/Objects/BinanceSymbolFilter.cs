using Binance.Net.Converters;
using Binance.Net.Enums;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// A filter for order placed on a symbol. Can be either a <see cref="BinanceSymbolPriceFilter"/>, <see cref="BinanceSymbolLotSizeFilter"/>, <see cref="BinanceSymbolMinNotionalFilter"/>, <see cref="BinanceSymbolMaxAlgorithmicOrdersFilter"/> or <see cref="BinanceSymbolIcebergPartsFilter"/> or <see cref="BinanceSymbolMaxPositionFilter"/>
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverter))]
    public class BinanceSymbolFilter
    {
        /// <summary>
        /// The type of this filter
        /// </summary>
        public SymbolFilterType FilterType { get; set; }
    }

    /// <summary>
    /// Price filter
    /// </summary>
    public class BinanceSymbolPriceFilter: BinanceSymbolFilter
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
    public class BinanceSymbolPercentPriceFilter : BinanceSymbolFilter
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
        public int AveragePriceMinutes { get; set; }
    }

    /// <summary>
    /// Price percentage filter
    /// </summary>
    public class BinanceFuturesSymbolPercentPriceFilter : BinanceSymbolFilter
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
        public int MultiplierDecimal { get; set; }
    }

    /// <summary>
    /// Lot size filter
    /// </summary>
    public class BinanceSymbolLotSizeFilter : BinanceSymbolFilter
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
    public class BinanceSymbolMarketLotSizeFilter : BinanceSymbolFilter
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
    public class BinanceSymbolMinNotionalFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The minimal total size of an order. This is calculated by Price * Quantity.
        /// </summary>
        public decimal MinNotional { get; set; }

        /// <summary>
        /// Whether or not this filter is applied to market orders. If so the average trade price is used.
        /// </summary>
        public bool ApplyToMarketOrders { get; set; }

        /// <summary>
        /// The amount of minutes the average price of trades is calculated over for market orders. 0 means the last price is used
        /// </summary>
        public int AveragePriceMinutes { get; set; }
    }

    /// <summary>
    /// Nax orders filter
    /// </summary>
    public class BinanceSymbolMaxOrdersFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The max number of orders for this symbol
        /// </summary>
        public int MaxNumberOrders { get; set; }
    }

    /// <summary>
    /// Max iceberg orders filter
    /// </summary>
    public class BinanceSymbolMaxIcebergOrdersFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The max number of iceberg orders for this symbol
        /// </summary>
        public int MaxNumberIcebergOrders { get; set; }
    }

    /// <summary>
    /// Max algo orders filter
    /// </summary>
    public class BinanceSymbolMaxAlgorithmicOrdersFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The max number of Algorithmic orders for this symbol
        /// </summary>
        public int MaxNumberAlgorithmicOrders { get; set; }
    }

    /// <summary>
    /// Max iceberg parts filter
    /// </summary>
    public class BinanceSymbolIcebergPartsFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The max parts of an iceberg order for this symbol.
        /// </summary>
        public int Limit { get; set; }
    }

    /// <summary>
    /// The max position filter
    /// </summary>
    public class BinanceSymbolMaxPositionFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The max position an account can have on the base asset of a symbol.
        /// </summary>
        public decimal MaxPosition { get; set; }
    }
}
