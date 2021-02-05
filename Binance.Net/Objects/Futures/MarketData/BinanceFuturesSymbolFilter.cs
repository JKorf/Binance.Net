using Binance.Net.Converters;
using Binance.Net.Enums;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// A filter for order placed on a symbol. Can be either a <see cref="BinanceSymbolPriceFilter"/>, <see cref="BinanceSymbolLotSizeFilter"/>, <see cref="BinanceSymbolMaxAlgorithmicOrdersFilter"/>
    /// </summary>
    [JsonConverter(typeof(SymbolFuturesFilterConverter))]
    public class BinanceFuturesSymbolFilter
    {
        /// <summary>
        /// The type of this filter
        /// </summary>
        public SymbolFilterType FilterType { get; set; }
    }

    /// <summary>
    /// Price filter
    /// </summary>
    public class BinanceSymbolPriceFilter : BinanceFuturesSymbolFilter
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
    /// Lot size filter
    /// </summary>
    public class BinanceSymbolLotSizeFilter : BinanceFuturesSymbolFilter
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
    public class BinanceSymbolMarketLotSizeFilter : BinanceFuturesSymbolFilter
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
    ///Max orders filter
    /// </summary>
    public class BinanceSymbolMaxOrdersFilter : BinanceFuturesSymbolFilter
    {
        /// <summary>
        /// The max number of orders for this symbol
        /// </summary>
        public int MaxNumberOrders { get; set; }
    }

    /// <summary>
    /// Max algo orders filter
    /// </summary>
    public class BinanceSymbolMaxAlgorithmicOrdersFilter : BinanceFuturesSymbolFilter
    {
        /// <summary>
        /// The max number of Algorithmic orders for this symbol
        /// </summary>
        public int MaxNumberAlgorithmicOrders { get; set; }
    }

    /// <summary>
    /// Price percentage filter
    /// </summary>
    public class BinanceSymbolPercentPriceFilter : BinanceFuturesSymbolFilter
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
    /// Min notional filter
    /// </summary>
    public class BinanceSymbolMinNotionalFilter : BinanceFuturesSymbolFilter
    {
        /// <summary>
        /// The minimal total size of an order. This is calculated by Price * Quantity.
        /// </summary>
        public decimal MinNotional { get; set; }
    }
}
