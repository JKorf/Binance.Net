using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Recent trade info
    /// </summary>
    [SerializationModel]
    public abstract record BinanceRecentTrade : IBinanceRecentTrade
    {
        /// <summary>
        /// The id of the trade
        /// </summary>
        [JsonPropertyName("id")]
        public long OrderId { get; set; }
        /// <summary>
        /// The price of the trade
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <inheritdoc />
        public abstract decimal BaseQuantity { get; set; }
        /// <inheritdoc />
        public abstract decimal QuoteQuantity { get; set; }
        /// <summary>
        /// The timestamp of the trade
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Whether the buyer is maker
        /// </summary>
        [JsonPropertyName("isBuyerMaker")]
        public bool BuyerIsMaker { get; set; }
        /// <summary>
        /// Whether this was a Retail Price Improvement trade
        /// </summary>
        [JsonPropertyName("IsRPITrade")]
        public bool? IsRpiTrade { get; set; }
        /// <summary>
        /// Whether the trade was made at the best match
        /// </summary>
        [JsonPropertyName("isBestMatch")]
        public bool IsBestMatch { get; set; }
    }

    /// <summary>
    /// Recent trade with quote quantity
    /// </summary>
    [SerializationModel]
    public record BinanceRecentTradeQuote : BinanceRecentTrade
    {
        /// <inheritdoc />
        [JsonPropertyName("quoteQty")]
        public override decimal QuoteQuantity { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("qty")]
        public override decimal BaseQuantity { get; set; }
    }

    /// <summary>
    /// Recent trade with base quantity
    /// </summary>
    [SerializationModel]
    public record BinanceRecentTradeBase : BinanceRecentTrade
    {
        /// <inheritdoc />
        [JsonPropertyName("qty")]
        public override decimal QuoteQuantity { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("baseQty")]
        public override decimal BaseQuantity { get; set; }
    }
}
