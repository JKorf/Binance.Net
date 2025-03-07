﻿using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// The result of query order
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesOrder
    {
        /// <summary>
        /// The symbol the order is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string? Pair { get; set; }

        /// <summary>
        /// The order id as assigned by Binance
        /// </summary>
        [JsonPropertyName("orderId")]
        public long Id { get; set; }
        /// <summary>
        /// The order id as assigned by the client
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string ClientOrderId { get; set; } = string.Empty;

        /// <summary>
        /// Whether or not this order is a liquidation order
        /// </summary>
        [JsonIgnore]
        public bool IsLiquidationOrder => ClientOrderId?.StartsWith("autoclose-") == true;
        /// <summary>
        /// Whether or not this order is an ADL auto close order
        /// </summary>
        [JsonIgnore]
        public bool IsAdlAutoCloseOrder => ClientOrderId?.StartsWith("adl_autoclose-") == true;
        /// <summary>
        /// Whether or not this order is a delisting/delivery settlement order
        /// </summary>
        [JsonIgnore]
        public bool IsSettlementOrder => ClientOrderId?.StartsWith("delivery_autoclose-") == true;

        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// The average price of the order
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// Quantity that has been filled
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Cumulative quantity
        /// </summary>
        [JsonPropertyName("cumQty")]
        public decimal? CumulativeQuantity { get; set; }
        /// <summary>
        /// Cumulative quantity in quote asset ( for USD futures )
        /// </summary>
        [JsonPropertyName("cumQuote")]
        public decimal? QuoteQuantityFilled { get; set; }

        /// <summary>
        /// Cumulative quantity in quote asset ( for Coin futures )
        /// </summary>
        [JsonPropertyName("cumBase")]
        public decimal? BaseQuantityFilled { get; set; }
        /// <summary>
        /// The original quantity of the order
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal Quantity { get; set; }
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
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Stop price for the order
        /// </summary>
        [JsonPropertyName("stopPrice")]
        public decimal? StopPrice { get; set; }

        /// <summary>
        /// For what time the order lasts
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonPropertyName("type")]
        public FuturesOrderType Type { get; set; }

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonPropertyName("origType")]
        public FuturesOrderType OriginalType { get; set; }

        /// <summary>
        /// Activation price, only return with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonPropertyName("activatePrice")]
        public decimal? ActivatePrice { get; set; }

        /// <summary>
        /// Callback rate, only return with TRAILING_STOP_MARKET order
        /// </summary>
        [JsonPropertyName("priceRate")]
        public decimal? CallbackRate { get; set; }

        /// <summary>
        /// The time the order was updated
        /// </summary>
        [JsonPropertyName("updateTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// The time the order was created
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }

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
    }

    /// <summary>
    /// Usdt futures order
    /// </summary>
    [SerializationModel]
    public record BinanceUsdFuturesOrder : BinanceFuturesOrder
    {

        /// <summary>
        /// Auto cancel at this date
        /// </summary>
        [JsonPropertyName("goodTillDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? GoodTillDate { get; set; }
    }
}
