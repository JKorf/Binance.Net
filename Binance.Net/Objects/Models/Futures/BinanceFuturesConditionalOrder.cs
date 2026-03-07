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
        /// ["<c>symbol</c>"] The symbol the order is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>algoType</c>"] Algo type
        /// </summary>
        [JsonPropertyName("algoType")]
        public string AlgoType { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>algoId</c>"] The order id as assigned by Binance
        /// </summary>
        [JsonPropertyName("algoId")]
        public long Id { get; set; }

        /// <summary>
        /// ["<c>actualOrderId</c>"] Actual order id
        /// </summary>
        [JsonPropertyName("actualOrderId")]
        public long? ActualOrderId { get; set; }

        /// <summary>
        /// ["<c>clientAlgoId</c>"] The order id as assigned by the client
        /// </summary>
        [JsonPropertyName("clientAlgoId")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string ClientOrderId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>price</c>"] The price of the order
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>actualPrice</c>"] Actual price
        /// </summary>
        [JsonPropertyName("actualPrice")]
        public decimal? ActualPrice { get; set; }

        /// <summary>
        /// ["<c>tpTriggerPrice</c>"] Take profit trigger price
        /// </summary>
        [JsonPropertyName("tpTriggerPrice")]
        public decimal? TpTriggerPrice { get; set; }
        /// <summary>
        /// ["<c>tpPrice</c>"] Take profit price
        /// </summary>
        [JsonPropertyName("tpPrice")]
        public decimal? TpPrice { get; set; }

        /// <summary>
        /// ["<c>slTriggerPrice</c>"] Stop loss trigger price
        /// </summary>
        [JsonPropertyName("slTriggerPrice")]
        public decimal? SlTriggerPrice { get; set; }
        /// <summary>
        /// ["<c>slPrice</c>"] Stop loss price
        /// </summary>
        [JsonPropertyName("slPrice")]
        public decimal? SlPrice { get; set; }

        /// <summary>
        /// ["<c>quantity</c>"] The original quantity of the order
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// ["<c>icebergQuantity</c>"] Iceberg quantity
        /// </summary>
        [JsonPropertyName("icebergQuantity")]
        public decimal? IcebergQuantity { get; set; }
        /// <summary>
        /// ["<c>reduceOnly</c>"] Reduce Only
        /// </summary>
        [JsonPropertyName("reduceOnly")]
        public bool ReduceOnly { get; set; }

        /// <summary>
        /// ["<c>closePosition</c>"] If order is for closing a position
        /// </summary>
        [JsonPropertyName("closePosition")]
        public bool ClosePosition { get; set; }

        /// <summary>
        /// ["<c>side</c>"] The side of the order
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }

        /// <summary>
        /// ["<c>algoStatus</c>"] The current status of the order
        /// </summary>
        [JsonPropertyName("algoStatus")]
        public AlgoOrderStatus Status { get; set; }

        /// <summary>
        /// ["<c>triggerPrice</c>"] Trigger price for the order
        /// </summary>
        [JsonPropertyName("triggerPrice")]
        public decimal? TriggerPrice { get; set; }

        /// <summary>
        /// ["<c>timeInForce</c>"] For what time the order lasts
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }

        /// <summary>
        /// ["<c>orderType</c>"] The type of the order
        /// </summary>
        [JsonPropertyName("orderType")]
        public FuturesOrderType Type { get; set; }

        /// <summary>
        /// ["<c>activatePrice</c>"] Activation price, only return with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonPropertyName("activatePrice")]
        public decimal? ActivatePrice { get; set; }

        /// <summary>
        /// ["<c>callbackRate</c>"] Callback rate, only return with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonPropertyName("callbackRate")]
        public decimal? CallbackRate { get; set; }

        /// <summary>
        /// ["<c>updateTime</c>"] The time the order was updated
        /// </summary>
        [JsonPropertyName("updateTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// ["<c>createTime</c>"] The time the order was created
        /// </summary>
        [JsonPropertyName("createTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// ["<c>triggerTime</c>"] The trigger time
        /// </summary>
        [JsonPropertyName("triggerTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? TriggerTime { get; set; }

        /// <summary>
        /// ["<c>workingType</c>"] The working type
        /// </summary>
        [JsonPropertyName("workingType")]
        public WorkingType WorkingType { get; set; }

        /// <summary>
        /// ["<c>positionSide</c>"] The position side of the order
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }

        /// <summary>
        /// ["<c>priceProtect</c>"] Price protect
        /// </summary>
        [JsonPropertyName("priceProtect")]
        public bool PriceProtect { get; set; }

        /// <summary>
        /// ["<c>priceMatch</c>"] Price match type
        /// </summary>
        [JsonPropertyName("priceMatch")]
        public PriceMatch PriceMatch { get; set; }

        /// <summary>
        /// ["<c>selfTradePreventionMode</c>"] Self trade prevention mode
        /// </summary>
        [JsonPropertyName("selfTradePreventionMode")]
        public SelfTradePreventionMode? SelfTradePreventionMode { get; set; }

        /// <summary>
        /// ["<c>goodTillDate</c>"] Auto cancel at this date
        /// </summary>
        [JsonPropertyName("goodTillDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? GoodTillDate { get; set; }
    }
}

