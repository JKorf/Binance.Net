using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Parameters for editing an order
    /// </summary>
    public record BinanceFuturesBatchEditOrder
    {
        /// <summary>
        /// Id of the order to edit. This or ClientOrderId should be provided
        /// </summary>
        public long? OrderId { get; set; }
        /// <summary>
        /// Client id of the order to edit. This or OrderId should be provided
        /// </summary>
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Symbol of the order
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Side of the order
        /// </summary>
        public OrderSide Side { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        public decimal Quantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// PriceMatch
        /// </summary>
        public PriceMatch? PriceMatch { get; set; }
    }
}
