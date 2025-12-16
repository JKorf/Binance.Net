using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Conditional order info
    /// </summary>
    public record BinanceFuturesConditionalOrder
    {
        /// <summary>
        /// The symbol the order is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Algo type
        /// </summary>
        [JsonPropertyName("algoType")]
        public string AlgoType { get; set; } = string.Empty;

        /// <summary>
        /// The order id as assigned by Binance
        /// </summary>
        [JsonPropertyName("algoId")]
        public long Id { get; set; }

        /// <summary>
        /// Actual order id
        /// </summary>
        [JsonPropertyName("actualOrderId")]
        public long? ActualOrderId { get; set; }

        /// <summary>
        /// The order id as assigned by the client
        /// </summary>
        [JsonPropertyName("clientAlgoId")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string ClientOrderId { get; set; } = string.Empty;

        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Actual price
        /// </summary>
        [JsonPropertyName("actualPrice")]
        public decimal? ActualPrice { get; set; }

        /// <summary>
        /// Take profit trigger price
        /// </summary>
        [JsonPropertyName("tpTriggerPrice")]
        public decimal? TpTriggerPrice { get; set; }
        /// <summary>
        /// Take profit price
        /// </summary>
        [JsonPropertyName("tpPrice")]
        public decimal? TpPrice { get; set; }

        /// <summary>
        /// Stop loss trigger price
        /// </summary>
        [JsonPropertyName("slTriggerPrice")]
        public decimal? SlTriggerPrice { get; set; }
        /// <summary>
        /// Stop loss price
        /// </summary>
        [JsonPropertyName("slPrice")]
        public decimal? SlPrice { get; set; }

        /// <summary>
        /// The original quantity of the order
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Iceberg quantity
        /// </summary>
        [JsonPropertyName("icebergQuantity")]
        public decimal? IcebergQuantity { get; set; }
        /// <summary>
        /// Reduce Only
        /// </summary>
        [JsonPropertyName("reduceOnly")]
        public bool ReduceOnly { get; set; }

        /// <summary>
        /// If order is for closing a position
        /// </summary>
        [JsonPropertyName("closePosition")]
        public bool ClosePosition { get; set; }

        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }

        /// <summary>
        /// The current status of the order
        /// </summary>
        [JsonPropertyName("algoStatus")]
        public AlgoOrderStatus Status { get; set; }

        /// <summary>
        /// Trigger price for the order
        /// </summary>
        [JsonPropertyName("triggerPrice")]
        public decimal? TriggerPrice { get; set; }

        /// <summary>
        /// For what time the order lasts
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonPropertyName("orderType")]
        public FuturesOrderType Type { get; set; }

        /// <summary>
        /// Activation price, only return with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonPropertyName("activatePrice")]
        public decimal? ActivatePrice { get; set; }

        /// <summary>
        /// Callback rate, only return with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonPropertyName("callbackRate")]
        public decimal? CallbackRate { get; set; }

        /// <summary>
        /// The time the order was updated
        /// </summary>
        [JsonPropertyName("updateTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// The time the order was created
        /// </summary>
        [JsonPropertyName("createTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// The trigger time
        /// </summary>
        [JsonPropertyName("triggerTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? TriggerTime { get; set; }

        /// <summary>
        /// The working type
        /// </summary>
        [JsonPropertyName("workingType")]
        public WorkingType WorkingType { get; set; }

        /// <summary>
        /// The position side of the order
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }

        /// <summary>
        /// Price protect
        /// </summary>
        [JsonPropertyName("priceProtect")]
        public bool PriceProtect { get; set; }

        /// <summary>
        /// Price match type
        /// </summary>
        [JsonPropertyName("priceMatch")]
        public PriceMatch PriceMatch { get; set; }

        /// <summary>
        /// Self trade prevention mode
        /// </summary>
        [JsonPropertyName("selfTradePreventionMode")]
        public SelfTradePreventionMode? SelfTradePreventionMode { get; set; }

        /// <summary>
        /// Auto cancel at this date
        /// </summary>
        [JsonPropertyName("goodTillDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? GoodTillDate { get; set; }
    }
}
