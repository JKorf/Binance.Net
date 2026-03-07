namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Represents information about a trade
    /// </summary>
    [SerializationModel]
    public record BinanceTrade
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol the trade is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>id</c>"] The trade identifier.
        /// </summary>
        [JsonPropertyName("id")]        
        public long Id { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] The order id the trade belongs to
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }

        /// <summary>
        /// ["<c>orderListId</c>"] Id of the order list this order belongs to
        /// </summary>
        [JsonPropertyName("orderListId")]
        public long? OrderListId { get; set; }

        /// <summary>
        /// ["<c>price</c>"] The price of the trade
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>qty</c>"] The quantity of the trade
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>quoteQty</c>"] The quote quantity of the trade
        /// </summary>
        [JsonPropertyName("quoteQty")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>commission</c>"] The fee paid for the trade
        /// </summary>
        [JsonPropertyName("commission")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>commissionAsset</c>"] The asset the fee is paid in
        /// </summary>
        [JsonPropertyName("commissionAsset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>time</c>"] The time the trade was made
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>isBuyer</c>"] Whether account was the buyer in the trade
        /// </summary>
        [JsonPropertyName("isBuyer")]
        public bool IsBuyer { get; set; }
        /// <summary>
        /// ["<c>isMaker</c>"] Whether account was the maker in the trade
        /// </summary>
        [JsonPropertyName("isMaker")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// ["<c>isBestMatch</c>"] Whether trade was made with the best match
        /// </summary>
        [JsonPropertyName("isBestMatch")]
        public bool IsBestMatch { get; set; }
        /// <summary>
        /// ["<c>isIsolated</c>"] If isolated margin (for margin account orders)
        /// </summary>
        [JsonPropertyName("isIsolated")]        
        public bool? IsIsolated { get; set; }
    }
}

