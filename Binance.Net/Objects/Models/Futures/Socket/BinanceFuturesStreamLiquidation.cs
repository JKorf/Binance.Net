using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// A event received by a Binance websocket
    /// </summary>
    public record BinanceFuturesStreamLiquidationData : BinanceStreamEvent
    {
        /// <summary>
        /// The data of the event
        /// </summary>
        [JsonPropertyName("o")]
        public BinanceFuturesStreamLiquidation Data { get; set; } = default!;
    }

    /// <summary>
    /// 
    /// </summary>
    public record BinanceFuturesStreamLiquidation : IBinanceFuturesLiquidation
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Liquidation Sided
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
        
        /// <summary>
        /// Liquidation order type
        /// </summary>
        [JsonPropertyName("o")]
        public FuturesOrderType Type { get; set; }
        
        /// <summary>
        /// Liquidation Time in Force
        /// </summary>
        [JsonPropertyName("f")]
        public TimeInForce TimeInForce { get; set; }
        
        /// <summary>
        /// Liquidation Original Quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        
        /// <summary>
        /// Liquidation order price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        
        /// <summary>
        /// Liquidation Average Price
        /// </summary>
        [JsonPropertyName("ap")]
        public decimal AveragePrice { get; set; }
        
        /// <summary>
        /// Liquidation Order Status
        /// </summary>
        [JsonPropertyName("X")]
        public OrderStatus Status { get; set; }
        
        /// <summary>
        /// Liquidation Last Filled Quantity
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LastQuantityFilled { get; set; }
        
        /// <summary>
        /// Liquidation Accumulated fill quantity
        /// </summary>
        [JsonPropertyName("z")]
        public decimal QuantityFilled { get; set; }
        
        /// <summary>
        /// Liquidation Trade Time
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
