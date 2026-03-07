using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Update data about a trade
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesStreamTradeUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>T</c>"] The transaction time.
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// ["<c>s</c>"] The symbol the order is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
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
        /// ["<c>m</c>"] Whether the buyer is the maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool BuyerIsMaker { get; set; }
        /// <summary>
        /// ["<c>c</c>"] The new client order id
        /// </summary>
        /// <remarks>
        /// special client order id:
        ///     starts with "autoclose-": liquidation order
        ///     "adl_autoclose": ADL auto close order
        ///     "settlement_autoclose-": settlement order for delisting or delivery
        /// </remarks>
        [JsonPropertyName("c")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>S</c>"] The side of the order
        /// </summary>
        [JsonPropertyName("S")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>L</c>"] The price of the last filled trade
        /// </summary>
        [JsonPropertyName("L")]
        public decimal PriceLastFilledTrade { get; set; }
        /// <summary>
        /// ["<c>l</c>"] The quantity of the last filled trade of this order
        /// </summary>
        [JsonPropertyName("l")]
        public decimal QuantityOfLastFilledTrade { get; set; }
        /// <summary>
        /// ["<c>t</c>"] The trade id
        /// </summary>
        [JsonPropertyName("t")]
        public long TradeId { get; set; }
        /// <summary>
        /// ["<c>i</c>"] The id of the order as assigned by Binance
        /// </summary>
        [JsonPropertyName("i")]
        public long OrderId { get; set; }
    }
}

