using Binance.Net.Enums;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Update data about an order
    /// </summary>
    public record BinanceStreamOrderUpdate: BinanceStreamEvent
    {
        /// <summary>
        /// The id of the order as assigned by Binance
        /// </summary>
        [JsonPropertyName("i")]
        public long Id { get; set; }
        /// <summary>
        /// The symbol the order is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The new client order id
        /// </summary>
        [JsonPropertyName("c")]
        [JsonConverterCtor(typeof(ReplaceConverter), 
            $"{BinanceExchange.ClientOrderIdPrefixSpot}->",
            $"{BinanceExchange.ClientOrderIdPrefixFutures}->")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonPropertyName("o")]
        public SpotOrderType Type { get; set; }
        /// <summary>
        /// The timespan the order is active
        /// </summary>
        [JsonPropertyName("f")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// The stop price of the order
        /// </summary>
        [JsonPropertyName("P")]
        public decimal StopPrice { get; set; }
        /// <summary>
        /// The trailing delta of the order
        /// </summary>
        [JsonPropertyName("d")]
        public int? TrailingDelta { get; set; }
        /// <summary>
        /// Trailing Time; This is only visible if the trailing stop order has been activated.
        /// </summary>
        [JsonPropertyName("D")]
        public DateTime TrailingTime { get; set; }
        /// <summary>
        /// The iceberg quantity of the order
        /// </summary>
        [JsonPropertyName("F")]
        public decimal IcebergQuantity { get; set; }
        /// <summary>
        /// The original client order id
        /// </summary>
        [JsonPropertyName("C")]
        [JsonConverterCtor(typeof(ReplaceConverter), 
            $"{BinanceExchange.ClientOrderIdPrefixSpot}->",
            $"{BinanceExchange.ClientOrderIdPrefixFutures}->")]
        public string? OriginalClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// The execution type
        /// </summary>
        [JsonPropertyName("x")]
        public ExecutionType ExecutionType { get; set; }
        /// <summary>
        /// The status of the order
        /// </summary>
        [JsonPropertyName("X")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// The reason the order was rejected
        /// </summary>
        [JsonPropertyName("r")]
        public OrderRejectReason RejectReason { get; set; }
        /// <summary>
        /// The quantity of the last filled trade of this order
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LastQuantityFilled { get; set; }
        /// <summary>
        /// The quantity of all trades that were filled for this order
        /// </summary>
        [JsonPropertyName("z")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// The price of the last filled trade
        /// </summary>
        [JsonPropertyName("L")]
        public decimal LastPriceFilled { get; set; }
        /// <summary>
        /// The fee paid
        /// </summary>
        [JsonPropertyName("n")]
        public decimal Fee { get; set; }
        /// <summary>
        /// The asset the fee was taken from
        /// </summary>
        [JsonPropertyName("N")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// The time of the update
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// The trade id
        /// </summary>
        [JsonPropertyName("t")]
        public long TradeId { get; set; }
        /// <summary>
        /// Is working
        /// </summary>
        [JsonPropertyName("w")]
        public bool IsWorking { get; set; }
        /// <summary>
        /// Whether the buyer is the maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool BuyerIsMaker { get; set; }
        /// <summary>
        /// Time the order was created
        /// </summary>
        [JsonPropertyName("O"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Cumulative quantity
        /// </summary>
        [JsonPropertyName("Z")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// Quote order quantity
        /// </summary>
        [JsonPropertyName("Q")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// Last quote asset transacted quantity (i.e. LastPrice * LastQuantity)
        /// </summary>
        [JsonPropertyName("Y")]
        public decimal LastQuoteQuantity { get; set; }
        /// <summary>
        /// This id of the corresponding order list. (-1 if not part of an order list)
        /// </summary>
        [JsonPropertyName("g")]
        public long OrderListId { get; set; }
        /// <summary>
        /// The listen key for which the update was
        /// </summary>
        public string ListenKey { get; set; } = string.Empty;

        // These are unused properties, but are mapped to prevent mapping error of lower/upper case
        /// <summary>
        /// Unused
        /// </summary>
        [JsonPropertyName("I")]
        public long I { get; set; }
        /// <summary>
        /// Unused
        /// </summary>
        [JsonPropertyName("M")]
        public bool M { get; set; }
        /// <summary>
        /// Trade group id
        /// </summary>
        [JsonPropertyName("u")]
        public long? TradeGroupId { get; set; }
        /// <summary>
        /// Prevented match id
        /// </summary>
        [JsonPropertyName("v")]
        public long? PreventedMatchId { get; set; }
        /// <summary>
        /// Counter order id
        /// </summary>
        [JsonPropertyName("U")]
        public long? CounterOrderId { get; set; }
        /// <summary>
        /// Prevented quantity
        /// </summary>
        [JsonPropertyName("A")]
        public decimal? PreventedQuantity { get; set; }
        /// <summary>
        /// Last prevented quantity
        /// </summary>
        [JsonPropertyName("B")]
        public decimal? LastPreventedQuantity { get; set; }
        /// <summary>
        /// Prevented match id
        /// </summary>
        [JsonPropertyName("V")]
        [JsonConverter(typeof(EnumConverter<SelfTradePreventionMode>))]
        public SelfTradePreventionMode? SelfTradePreventionMode { get; set; }
        /// <summary>
        /// Working time; when it entered the order book
        /// </summary>
        [JsonPropertyName("W"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? WorkingTime { get; set; }
    }
}
