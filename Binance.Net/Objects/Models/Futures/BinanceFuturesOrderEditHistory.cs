using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// The history of order edits
    /// </summary>
    public record BinanceFuturesOrderEditHistory
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
        /// The id of the amendment
        /// </summary>
        [JsonPropertyName("amendmentId")]
        public long AmendmentId { get; set; }
        /// <summary>
        /// The order id as assigned by Binance
        /// </summary>
        [JsonPropertyName("orderId")]
        public long Id { get; set; }
        /// <summary>
        /// The order id as assigned by the client
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        [JsonConverterCtor(typeof(ReplaceConverter), 
            $"{BinanceExchange.ClientOrderIdPrefixSpot}->",
            $"{BinanceExchange.ClientOrderIdPrefixFutures}->")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Edit time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Edit info
        /// </summary>
        [JsonPropertyName("amendment")]
        public BinanceFuturesOrderChanges EditInfo { get; set; } = null!;
        /// <summary>
        /// Price match
        /// </summary>
        [JsonPropertyName("priceMatch"), JsonConverter(typeof(EnumConverter<PriceMatch>))]
        public PriceMatch PriceMatch { get; set; }

    }

    /// <summary>
    /// Order changes
    /// </summary>
    public record BinanceFuturesOrderChanges
    {
        /// <summary>
        /// Price change
        /// </summary>
        [JsonPropertyName("price")]
        public BinanceFuturesOrderChange Price { get; set; } = null!;
        /// <summary>
        /// Quantity change
        /// </summary>
        [JsonPropertyName("origQty")]
        public BinanceFuturesOrderChange Quantity { get; set; } = null!;

        /// <summary>
        /// Amount of times changed
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
        /// Before edit
        /// </summary>
        [JsonPropertyName("before")]
        public decimal Before { get; set; }
        /// <summary>
        /// After edit
        /// </summary>
        [JsonPropertyName("after")]
        public decimal After { get; set; }
    }
}
