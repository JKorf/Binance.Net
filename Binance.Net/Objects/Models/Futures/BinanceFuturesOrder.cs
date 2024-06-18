using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// The result of query order
    /// </summary>
    public record BinanceFuturesOrder
    {
        /// <summary>
        /// The symbol the order is for
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Pair
        /// </summary>
        [JsonProperty("pair")]
        public string? Pair { get; set; }

        /// <summary>
        /// The order id as assigned by Binance
        /// </summary>
        [JsonProperty("orderId")]
        public long Id { get; set; }
        /// <summary>
        /// The order id as assigned by the client
        /// </summary>
        [JsonProperty("clientOrderId")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// The average price of the order
        /// </summary>
        [JsonProperty("avgPrice")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// Quantity that has been filled
        /// </summary>
        [JsonProperty("executedQty")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Cumulative quantity
        /// </summary>
        [JsonProperty("cumQty")]
        public decimal? CummulativeQuantity { get; set; }
        /// <summary>
        /// Cumulative quantity in quote asset ( for USD futures )
        /// </summary>
        [JsonProperty("cumQuote")]
        public decimal? QuoteQuantityFilled { get; set; }

        /// <summary>
        /// Cumulative quantity in quote asset ( for Coin futures )
        /// </summary>
        [JsonProperty("cumBase")]
        public decimal? BaseQuantityFilled { get; set; }
        /// <summary>
        /// The original quantity of the order
        /// </summary>
        [JsonProperty("origQty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Reduce Only
        /// </summary>
        [JsonProperty("reduceOnly")]
        public bool ReduceOnly { get; set; }

        /// <summary>
        /// If order is for closing a position
        /// </summary>
        [JsonProperty("closePosition")]
        public bool ClosePosition { get; set; }

        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }

        /// <summary>
        /// The current status of the order
        /// </summary>
        [JsonProperty("status"), JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Stop price for the order
        /// </summary>
        [JsonProperty("stopPrice")]
        public decimal? StopPrice { get; set; }

        /// <summary>
        /// For what time the order lasts
        /// </summary>
        [JsonProperty("timeInForce"), JsonConverter(typeof(EnumConverter))]
        public TimeInForce TimeInForce { get; set; }

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(FuturesOrderTypeConverter))]
        public FuturesOrderType Type { get; set; }

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonProperty("origType"), JsonConverter(typeof(FuturesOrderTypeConverter))]
        public FuturesOrderType OriginalType { get; set; }

        /// <summary>
        /// Activation price, only return with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonProperty("activatePrice")]
        public decimal? ActivatePrice { get; set; }

        /// <summary>
        /// Callback rate, only return with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonProperty("priceRate")]
        public decimal? CallbackRate { get; set; }

        /// <summary>
        /// The time the order was updated
        /// </summary>
        [JsonProperty("updateTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// The time the order was created
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// The working type
        /// </summary>
        [JsonProperty("workingType"), JsonConverter(typeof(WorkingTypeConverter))]
        public WorkingType WorkingType { get; set; }

        /// <summary>
        /// The position side of the order
        /// </summary>
        [JsonProperty("positionSide"), JsonConverter(typeof(PositionSideConverter))]
        public PositionSide PositionSide { get; set; }
        
        /// <summary>
        /// Price protect
        /// </summary>
        [JsonProperty("priceProtect")]
        public bool PriceProtect { get; set; }
    }

    /// <summary>
    /// Usdt futures order
    /// </summary>
    public record BinanceUsdFuturesOrder : BinanceFuturesOrder
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("priceMatch"), JsonConverter(typeof(EnumConverter))]
        public PriceMatch PriceMatch { get; set; }

        /// <summary>
        /// Self trade prevention mode
        /// </summary>
        [JsonProperty("selfTradePreventionMode"), JsonConverter(typeof(EnumConverter))]
        public SelfTradePreventionMode SelfTradePreventionMode { get; set; }

        /// <summary>
        /// Auto cancel at this date
        /// </summary>
        [JsonProperty("goodTillDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? GoodTillDate { get; set; }
    }
}
