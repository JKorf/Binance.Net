using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// C2C user trade
    /// </summary>
    public class BinanceC2CUserTrade
    {
        /// <summary>
        /// Order number
        /// </summary>
        public string OrderNumber { get; set; } = string.Empty;
        /// <summary>
        /// Advert number
        /// </summary>
        [JsonProperty("advNo")]
        public string AdvertNumber { get; set; } = string.Empty;
        /// <summary>
        /// Trade type
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide TradeType { get; set; }
        /// <summary>
        /// Crypto asset traded
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Fiat type
        /// </summary>
        public string Fiat { get; set; } = string.Empty;
        /// <summary>
        /// Fiat symbol
        /// </summary>
        public string FiatSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Quantity traded
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Total price of the trade
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// Price per unit
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public C2COrderStatus OrderStatus { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Transaction fee in crypto
        /// </summary>
        [JsonProperty("commission")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Counter part nickname
        /// </summary>
        public string CounterPartNickName { get; set; } = string.Empty;
        /// <summary>
        /// Advertisement role
        /// </summary>
        public string AdvertisementRole { get; set; } = string.Empty;
    }
}
