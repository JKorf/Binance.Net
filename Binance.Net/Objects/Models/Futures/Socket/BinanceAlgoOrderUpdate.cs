using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures.Socket
{

    /// <summary>
    /// Algo order update
    /// </summary>
    public record BinanceAlgoOrderUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>T</c>"] The event timestamp.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("T")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ["<c>o</c>"] Order info
        /// </summary>
        [JsonPropertyName("o")]
        public BinanceAlgoOrderUpdateOrder Order { get; set; } = null!;
    }

    /// <summary>
    /// Order info
    /// </summary>
    public record BinanceAlgoOrderUpdateOrder
    {
        /// <summary>
        /// ["<c>s</c>"] The symbol the order is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>at</c>"] Algo type
        /// </summary>
        [JsonPropertyName("at")]
        public string AlgoType { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>aid</c>"] The order id as assigned by Binance
        /// </summary>
        [JsonPropertyName("aid")]
        public long Id { get; set; }

        /// <summary>
        /// ["<c>ai</c>"] Actual order id
        /// </summary>
        [JsonPropertyName("ai")]
        public long? ActualOrderId { get; set; }

        /// <summary>
        /// ["<c>caid</c>"] The order id as assigned by the client
        /// </summary>
        [JsonPropertyName("caid")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string ClientOrderId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>p</c>"] The price of the order
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }

        /// <summary>
        /// ["<c>q</c>"] The original quantity of the order
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// ["<c>ap</c>"] The average fill price of the triggered order
        /// </summary>
        [JsonPropertyName("ap")]
        public decimal? AverageFillPrice { get; set; }

        /// <summary>
        /// ["<c>aq</c>"] The quantity filled of the triggered order
        /// </summary>
        [JsonPropertyName("aq")]
        public decimal? QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>R</c>"] Reduce Only
        /// </summary>
        [JsonPropertyName("R")]
        public bool ReduceOnly { get; set; }

        /// <summary>
        /// ["<c>cp</c>"] If order is for closing a position
        /// </summary>
        [JsonPropertyName("cp")]
        public bool ClosePosition { get; set; }

        /// <summary>
        /// ["<c>S</c>"] The side of the order
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }

        /// <summary>
        /// ["<c>X</c>"] The current status of the order
        /// </summary>
        [JsonPropertyName("X")]
        public AlgoOrderStatus Status { get; set; }

        /// <summary>
        /// ["<c>tp</c>"] Trigger price for the order
        /// </summary>
        [JsonPropertyName("tp")]
        public decimal? TriggerPrice { get; set; }

        /// <summary>
        /// ["<c>f</c>"] For what time the order lasts
        /// </summary>
        [JsonPropertyName("f")]
        public TimeInForce TimeInForce { get; set; }

        /// <summary>
        /// ["<c>o</c>"] The type of the order
        /// </summary>
        [JsonPropertyName("o")]
        public FuturesOrderType Type { get; set; }

        /// <summary>
        /// ["<c>wt</c>"] The working type
        /// </summary>
        [JsonPropertyName("wt")]
        public WorkingType WorkingType { get; set; }

        /// <summary>
        /// ["<c>ps</c>"] The position side of the order
        /// </summary>
        [JsonPropertyName("ps")]
        public PositionSide PositionSide { get; set; }

        /// <summary>
        /// ["<c>pP</c>"] Price protect
        /// </summary>
        [JsonPropertyName("pP")]
        public bool PriceProtect { get; set; }

        /// <summary>
        /// ["<c>pm</c>"] Price match type
        /// </summary>
        [JsonPropertyName("pm")]
        public PriceMatch PriceMatch { get; set; }

        /// <summary>
        /// ["<c>V</c>"] Self trade prevention mode
        /// </summary>
        [JsonPropertyName("V")]
        public SelfTradePreventionMode? SelfTradePreventionMode { get; set; }

        /// <summary>
        /// ["<c>gtd</c>"] Auto cancel at this date
        /// </summary>
        [JsonPropertyName("gtd"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? GoodTillDate { get; set; }
        /// <summary>
        /// ["<c>tt</c>"] Trigger time
        /// </summary>
        [JsonPropertyName("tt"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? TriggerTime { get; set; }
    }
}

