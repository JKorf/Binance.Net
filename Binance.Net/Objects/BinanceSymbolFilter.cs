using Binance.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// A filter for order placed on a symbol. Can be either a <see cref="BinanceSymbolPriceFilter"/>, <see cref="BinanceSymbolLotSizeFilter"/> or <see cref="BinanceSymbolMinNotionalFilter"/>
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverter))]
    public class BinanceSymbolFilter
    {
        /// <summary>
        /// The type of this filter
        /// </summary>
        public SymbolFilterType FilterType { get; set; }
    }

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

    public class BinanceSymbolMinNotionalFilter : BinanceSymbolFilter
    {
        /// <summary>
        /// The minimal total size of an order. This is calculated by Price * Quantity.
        /// </summary>
        public decimal MinNotional { get; set; }
    }
}
