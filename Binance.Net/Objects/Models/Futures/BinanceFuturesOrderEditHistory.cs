using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// The history of order edits
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesOrderEditHistory
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol the order is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>pair</c>"] Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string? Pair { get; set; }

        /// <summary>
        /// ["<c>amendmentId</c>"] The id of the amendment
        /// </summary>
        [JsonPropertyName("amendmentId")]
        public long AmendmentId { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] The order id as assigned by Binance
        /// </summary>
        [JsonPropertyName("orderId")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>clientOrderId</c>"] The order id as assigned by the client
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Edit time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>amendment</c>"] Edit info
        /// </summary>
        [JsonPropertyName("amendment")]
        public BinanceFuturesOrderChanges EditInfo { get; set; } = null!;
        /// <summary>
        /// ["<c>priceMatch</c>"] Price match
        /// </summary>
        [JsonPropertyName("priceMatch")]
        public PriceMatch PriceMatch { get; set; }

    }

    /// <summary>
    /// Order changes
    /// </summary>
    public record BinanceFuturesOrderChanges
    {
        /// <summary>
        /// ["<c>price</c>"] Price change
        /// </summary>
        [JsonPropertyName("price")]
        public BinanceFuturesOrderChange Price { get; set; } = null!;
        /// <summary>
        /// ["<c>origQty</c>"] Quantity change
        /// </summary>
        [JsonPropertyName("origQty")]
        public BinanceFuturesOrderChange Quantity { get; set; } = null!;

        /// <summary>
        /// ["<c>count</c>"] Amount of times changed
        /// </summary>
        [JsonPropertyName("count")]
        public int EditCount { get; set; }
    }

    /// <summary>
    /// Change info
    /// </summary>
    public record BinanceFuturesOrderChange
    {
        /// <summary>
        /// ["<c>before</c>"] Before edit
        /// </summary>
        [JsonPropertyName("before")]
        public decimal Before { get; set; }
        /// <summary>
        /// ["<c>after</c>"] After edit
        /// </summary>
        [JsonPropertyName("after")]
        public decimal After { get; set; }
    }
}

