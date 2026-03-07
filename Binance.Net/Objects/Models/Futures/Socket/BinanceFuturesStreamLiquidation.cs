using Binance.Net.Enums;
using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// A event received by a Binance websocket
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesStreamLiquidationData : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>o</c>"] The data of the event
        /// </summary>
        [JsonPropertyName("o")]
        public BinanceFuturesStreamLiquidation Data { get; set; } = default!;
    }

    /// <summary>
    /// Liquidation info
    /// </summary>
    public record BinanceFuturesStreamLiquidation : IBinanceFuturesLiquidation
    {
        /// <summary>
        /// ["<c>s</c>"] The symbol.
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>S</c>"] Liquidation side.
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }

        /// <summary>
        /// ["<c>o</c>"] Liquidation order type
        /// </summary>
        [JsonPropertyName("o")]
        public FuturesOrderType Type { get; set; }

        /// <summary>
        /// ["<c>f</c>"] Liquidation Time in Force
        /// </summary>
        [JsonPropertyName("f")]
        public TimeInForce TimeInForce { get; set; }

        /// <summary>
        /// ["<c>q</c>"] Liquidation Original Quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// ["<c>p</c>"] Liquidation order price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }

        /// <summary>
        /// ["<c>ap</c>"] Liquidation Average Price
        /// </summary>
        [JsonPropertyName("ap")]
        public decimal AveragePrice { get; set; }

        /// <summary>
        /// ["<c>X</c>"] Liquidation Order Status
        /// </summary>
        [JsonPropertyName("X")]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// ["<c>l</c>"] Liquidation Last Filled Quantity
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LastQuantityFilled { get; set; }

        /// <summary>
        /// ["<c>z</c>"] Liquidation Accumulated fill quantity
        /// </summary>
        [JsonPropertyName("z")]
        public decimal QuantityFilled { get; set; }

        /// <summary>
        /// ["<c>T</c>"] Liquidation Trade Time
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}

