using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Update data about a trade
    /// </summary>
    public record BinanceFuturesStreamTradeUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// The symbol the order is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
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
        /// Whether the buyer is the maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool BuyerIsMaker { get; set; }
        /// <summary>
        /// The new client order id
        /// </summary>
        /// <remarks>
        /// special client order id:
        ///     starts with "autoclose-": liquidation order
        ///     "adl_autoclose": ADL auto close order
        ///     "settlement_autoclose-": settlement order for delisting or delivery
        /// </remarks>
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
        /// The price of the last filled trade
        /// </summary>
        [JsonPropertyName("L")]
        public decimal PriceLastFilledTrade { get; set; }
        /// <summary>
        /// The quantity of the last filled trade of this order
        /// </summary>
        [JsonPropertyName("l")]
        public decimal QuantityOfLastFilledTrade { get; set; }
        /// <summary>
        /// The trade id
        /// </summary>
        [JsonPropertyName("t")]
        public long TradeId { get; set; }
        /// <summary>
        /// The id of the order as assigned by Binance
        /// </summary>
        [JsonPropertyName("i")]
        public long OrderId { get; set; }
    }
}
