namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Information about a trade
    /// </summary>
    [SerializationModel]
    public record BinanceTrade
    {
        /// <summary>
        /// The symbol the trade is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The id of the trade
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// The order id the trade belongs to
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }

        /// <summary>
        /// Id of the order list this order belongs to
        /// </summary>
        [JsonPropertyName("orderListId")]
        public long? OrderListId { get; set; }

        /// <summary>
        /// The price of the trade
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the trade
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The quote quantity of the trade
        /// </summary>
        [JsonPropertyName("quoteQty")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// The fee paid for the trade
        /// </summary>
        [JsonPropertyName("commission")]
        public decimal Fee { get; set; }
        /// <summary>
        /// The asset the fee is paid in
        /// </summary>
        [JsonPropertyName("commissionAsset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// The time the trade was made
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Whether account was the buyer in the trade
        /// </summary>
        [JsonPropertyName("isBuyer")]
        public bool IsBuyer { get; set; }
        /// <summary>
        /// Whether account was the maker in the trade
        /// </summary>
        [JsonPropertyName("isMaker")]
        public bool IsMaker { get; set; }
        /// <summary>
        /// Whether trade was made with the best match
        /// </summary>
        [JsonPropertyName("isBestMatch")]
        public bool IsBestMatch { get; set; }
        /// <summary>
        /// If isolated margin (for margin account orders)
        /// </summary>
        [JsonPropertyName("isIsolated")]
        public bool? IsIsolated { get; set; }
    }
}
