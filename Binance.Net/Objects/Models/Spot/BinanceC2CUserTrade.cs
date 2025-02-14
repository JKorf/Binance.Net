using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// C2C user trade
    /// </summary>
    public record BinanceC2CUserTrade
    {
        /// <summary>
        /// Order number
        /// </summary>
        [JsonPropertyName("orderNumber")]
        public string OrderNumber { get; set; } = string.Empty;
        /// <summary>
        /// Advert number
        /// </summary>
        [JsonPropertyName("advNo")]
        public string AdvertNumber { get; set; } = string.Empty;
        /// <summary>
        /// Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public OrderSide TradeType { get; set; }
        /// <summary>
        /// Crypto asset traded
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Fiat type
        /// </summary>
        [JsonPropertyName("fiat")]
        public string Fiat { get; set; } = string.Empty;
        /// <summary>
        /// Fiat symbol
        /// </summary>
        [JsonPropertyName("fiatSymbol")]
        public string FiatSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Quantity traded
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Total price of the trade
        /// </summary>
        [JsonPropertyName("totalPrice")]
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// Price per unit
        /// </summary>
        [JsonPropertyName("unitPrice")]
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonConverter(typeof(PocAOTEnumConverter<C2COrderStatus>))]
        [JsonPropertyName("orderStatus")]
        public C2COrderStatus OrderStatus { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Transaction fee in crypto
        /// </summary>
        [JsonPropertyName("commission")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Counter part nickname
        /// </summary>
        [JsonPropertyName("counterPartNickName")]
        public string CounterPartNickName { get; set; } = string.Empty;
        /// <summary>
        /// Advertisement role
        /// </summary>
        [JsonPropertyName("advertisementRole")]
        public string AdvertisementRole { get; set; } = string.Empty;
    }
}
