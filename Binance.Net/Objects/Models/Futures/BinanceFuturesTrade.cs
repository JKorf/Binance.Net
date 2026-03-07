using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesTrade
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>buyer</c>"] Whether the user was the buyer.
        /// </summary>
        [JsonPropertyName("buyer")]
        public bool Buyer { get; set; }
        /// <summary>
        /// ["<c>commission</c>"] Paid fee
        /// </summary>
        [JsonPropertyName("commission")]
        public decimal Fee { get; set; }

        /// <summary>
        /// ["<c>commissionAsset</c>"] Asset the fee is paid in
        /// </summary>
        [JsonPropertyName("commissionAsset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>id</c>"] Trade id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>maker</c>"] Whether the user was the maker.
        /// </summary>
        [JsonPropertyName("maker")]
        public bool Maker { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>qty</c>"] Quantity
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>realizedPnl</c>"] Realized pnl
        /// </summary>
        [JsonPropertyName("realizedPnl")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Trade details
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesUsdtTrade : BinanceFuturesTrade
    {
        /// <summary>
        /// ["<c>quoteQty</c>"] Quote quantity
        /// </summary>
        [JsonPropertyName("quoteQty")]
        public decimal QuoteQuantity { get; set; }
    }

    /// <summary>
    /// Trade details
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesCoinTrade : BinanceFuturesTrade
    {
        /// <summary>
        /// ["<c>pair</c>"] The pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>marginAsset</c>"] The margin asset
        /// </summary>
        [JsonPropertyName("marginAsset")]
        public string MarginAsset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>baseQty</c>"] Base quantity
        /// </summary>
        [JsonPropertyName("baseQty")]
        public decimal BaseQuantity { get; set; }
    }
}

