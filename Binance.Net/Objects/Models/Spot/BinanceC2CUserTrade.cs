using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// C2C user trade
    /// </summary>
    [SerializationModel]
    public record BinanceC2CUserTrade
    {
        /// <summary>
        /// ["<c>orderNumber</c>"] The order number.
        /// </summary>
        [JsonPropertyName("orderNumber")]
        public string OrderNumber { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>advNo</c>"] The advertisement number.
        /// </summary>
        [JsonPropertyName("advNo")]
        public string AdvertNumber { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tradeType</c>"] Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public OrderSide TradeType { get; set; }
        /// <summary>
        /// ["<c>asset</c>"] Crypto asset traded
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fiat</c>"] Fiat type
        /// </summary>
        [JsonPropertyName("fiat")]
        public string Fiat { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fiatSymbol</c>"] Fiat symbol
        /// </summary>
        [JsonPropertyName("fiatSymbol")]
        public string FiatSymbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Quantity traded
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>totalPrice</c>"] Total price of the trade
        /// </summary>
        [JsonPropertyName("totalPrice")]
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// ["<c>unitPrice</c>"] Price per unit
        /// </summary>
        [JsonPropertyName("unitPrice")]
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// ["<c>orderStatus</c>"] Order status
        /// </summary>
        [JsonPropertyName("orderStatus")]
        public C2COrderStatus OrderStatus { get; set; }
        /// <summary>
        /// ["<c>createTime</c>"] Creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>commission</c>"] Transaction fee in crypto
        /// </summary>
        [JsonPropertyName("commission")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>counterPartNickName</c>"] Counter part nickname
        /// </summary>
        [JsonPropertyName("counterPartNickName")]
        public string CounterPartNickName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>advertisementRole</c>"] Advertisement role
        /// </summary>
        [JsonPropertyName("advertisementRole")]
        public string AdvertisementRole { get; set; } = string.Empty;
    }
}

