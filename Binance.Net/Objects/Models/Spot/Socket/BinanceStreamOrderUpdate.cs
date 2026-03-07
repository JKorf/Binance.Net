using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Update data about an order
    /// </summary>
    public record BinanceStreamOrderUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>i</c>"] The id of the order as assigned by Binance
        /// </summary>
        [JsonPropertyName("i")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>s</c>"] The symbol the order is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>c</c>"] The new client order id
        /// </summary>
        [JsonPropertyName("c")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>S</c>"] The side of the order
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>o</c>"] The type of the order
        /// </summary>
        [JsonPropertyName("o")]
        public SpotOrderType Type { get; set; }
        /// <summary>
        /// ["<c>f</c>"] The timespan the order is active
        /// </summary>
        [JsonPropertyName("f")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>q</c>"] The quantity of the order
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>p</c>"] The price of the order
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>P</c>"] The stop price of the order
        /// </summary>
        [JsonPropertyName("P")]
        public decimal StopPrice { get; set; }
        /// <summary>
        /// ["<c>d</c>"] The trailing delta of the order
        /// </summary>
        [JsonPropertyName("d")]
        public int? TrailingDelta { get; set; }
        /// <summary>
        /// ["<c>D</c>"] Trailing Time; This is only visible if the trailing stop order has been activated.
        /// </summary>
        [JsonPropertyName("D")]
        public DateTime TrailingTime { get; set; }
        /// <summary>
        /// ["<c>F</c>"] The iceberg quantity of the order
        /// </summary>
        [JsonPropertyName("F")]
        public decimal IcebergQuantity { get; set; }
        /// <summary>
        /// ["<c>C</c>"] The original client order id
        /// </summary>
        [JsonPropertyName("C")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string? OriginalClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>x</c>"] The execution type
        /// </summary>
        [JsonPropertyName("x")]
        public ExecutionType ExecutionType { get; set; }
        /// <summary>
        /// ["<c>X</c>"] The status of the order
        /// </summary>
        [JsonPropertyName("X")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>r</c>"] The reason the order was rejected
        /// </summary>
        [JsonPropertyName("r")]
        public OrderRejectReason RejectReason { get; set; }
        /// <summary>
        /// ["<c>l</c>"] The quantity of the last filled trade of this order
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LastQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>z</c>"] The quantity of all trades that were filled for this order
        /// </summary>
        [JsonPropertyName("z")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>L</c>"] The price of the last filled trade
        /// </summary>
        [JsonPropertyName("L")]
        public decimal LastPriceFilled { get; set; }
        /// <summary>
        /// ["<c>n</c>"] The fee paid
        /// </summary>
        [JsonPropertyName("n")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>N</c>"] The asset the fee was taken from
        /// </summary>
        [JsonPropertyName("N")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>T</c>"] The time of the update
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>t</c>"] The trade id
        /// </summary>
        [JsonPropertyName("t")]
        public long TradeId { get; set; }
        /// <summary>
        /// ["<c>w</c>"] Is working
        /// </summary>
        [JsonPropertyName("w")]
        public bool IsWorking { get; set; }
        /// <summary>
        /// ["<c>m</c>"] Whether the buyer is the maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool BuyerIsMaker { get; set; }
        /// <summary>
        /// ["<c>O</c>"] Time the order was created
        /// </summary>
        [JsonPropertyName("O"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>Z</c>"] Cumulative quantity
        /// </summary>
        [JsonPropertyName("Z")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>Q</c>"] Quote order quantity
        /// </summary>
        [JsonPropertyName("Q")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>Y</c>"] Last quote asset transacted quantity (i.e. LastPrice * LastQuantity)
        /// </summary>
        [JsonPropertyName("Y")]
        public decimal LastQuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>g</c>"] This id of the corresponding order list. (-1 if not part of an order list)
        /// </summary>
        [JsonPropertyName("g")]
        public long OrderListId { get; set; }
        /// <summary>
        /// API key this update was for.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        // These are unused properties, but are mapped to prevent mapping error of lower/upper case
        /// <summary>
        /// ["<c>I</c>"] Unused
        /// </summary>
        [JsonPropertyName("I")]
        public long I { get; set; }
        /// <summary>
        /// ["<c>M</c>"] Unused
        /// </summary>
        [JsonPropertyName("M")]
        public bool M { get; set; }
        /// <summary>
        /// ["<c>u</c>"] Trade group id
        /// </summary>
        [JsonPropertyName("u")]
        public long? TradeGroupId { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Prevented match id
        /// </summary>
        [JsonPropertyName("v")]
        public long? PreventedMatchId { get; set; }
        /// <summary>
        /// ["<c>U</c>"] Counter order id
        /// </summary>
        [JsonPropertyName("U")]
        public long? CounterOrderId { get; set; }
        /// <summary>
        /// ["<c>A</c>"] Prevented quantity
        /// </summary>
        [JsonPropertyName("A")]
        public decimal? PreventedQuantity { get; set; }
        /// <summary>
        /// ["<c>B</c>"] Last prevented quantity
        /// </summary>
        [JsonPropertyName("B")]
        public decimal? LastPreventedQuantity { get; set; }
        /// <summary>
        /// ["<c>V</c>"] Self-trade prevention mode.
        /// </summary>
        [JsonPropertyName("V")]
        public SelfTradePreventionMode? SelfTradePreventionMode { get; set; }
        /// <summary>
        /// ["<c>W</c>"] Working time; when it entered the order book
        /// </summary>
        [JsonPropertyName("W"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? WorkingTime { get; set; }
    }
}

