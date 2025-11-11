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
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("T")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Order info
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
        /// The symbol the order is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Algo type
        /// </summary>
        [JsonPropertyName("at")]
        public string AlgoType { get; set; } = string.Empty;

        /// <summary>
        /// The order id as assigned by Binance
        /// </summary>
        [JsonPropertyName("aid")]
        public long Id { get; set; }

        /// <summary>
        /// Actual order id
        /// </summary>
        [JsonPropertyName("ai")]
        public long? ActualOrderId { get; set; }

        /// <summary>
        /// The order id as assigned by the client
        /// </summary>
        [JsonPropertyName("caid")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string ClientOrderId { get; set; } = string.Empty;

        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }

        /// <summary>
        /// The original quantity of the order
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// The average fill price of the triggered order
        /// </summary>
        [JsonPropertyName("ap")]
        public decimal? AverageFillPrice { get; set; }

        /// <summary>
        /// The quantity filled of the triggered order
        /// </summary>
        [JsonPropertyName("aq")]
        public decimal? QuantityFilled { get; set; }
        /// <summary>
        /// Reduce Only
        /// </summary>
        [JsonPropertyName("R")]
        public bool ReduceOnly { get; set; }

        /// <summary>
        /// If order is for closing a position
        /// </summary>
        [JsonPropertyName("cp")]
        public bool ClosePosition { get; set; }

        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }

        /// <summary>
        /// The current status of the order
        /// </summary>
        [JsonPropertyName("X")]
        public AlgoOrderStatus Status { get; set; }

        /// <summary>
        /// Trigger price for the order
        /// </summary>
        [JsonPropertyName("tp")]
        public decimal? TriggerPrice { get; set; }

        /// <summary>
        /// For what time the order lasts
        /// </summary>
        [JsonPropertyName("f")]
        public TimeInForce TimeInForce { get; set; }

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonPropertyName("o")]
        public FuturesOrderType Type { get; set; }

        /// <summary>
        /// The working type
        /// </summary>
        [JsonPropertyName("wt")]
        public WorkingType WorkingType { get; set; }

        /// <summary>
        /// The position side of the order
        /// </summary>
        [JsonPropertyName("ps")]
        public PositionSide PositionSide { get; set; }

        /// <summary>
        /// Price protect
        /// </summary>
        [JsonPropertyName("pP")]
        public bool PriceProtect { get; set; }

        /// <summary>
        /// Price match type
        /// </summary>
        [JsonPropertyName("pm")]
        public PriceMatch PriceMatch { get; set; }

        /// <summary>
        /// Self trade prevention mode
        /// </summary>
        [JsonPropertyName("V")]
        public SelfTradePreventionMode? SelfTradePreventionMode { get; set; }

        /// <summary>
        /// Auto cancel at this date
        /// </summary>
        [JsonPropertyName("gtd"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? GoodTillDate { get; set; }
        /// <summary>
        /// Trigger time
        /// </summary>
        [JsonPropertyName("tt"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? TriggerTime { get; set; }
    }
}
