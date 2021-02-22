using System;
using System.Collections.Generic;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Objects.Shared;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.SpotData
{
    /// <summary>
    /// The result of placing a new OCO order
    /// </summary>
    public class BinanceOrderOcoList
    {
        /// <summary>
        /// The id of the order list
        /// </summary>
        public long OrderListId { get; set; }
        /// <summary>
        /// The contingency type
        /// </summary>
        public string ContingencyType { get; set; } = "";
        /// <summary>
        /// The order list status
        /// </summary>
        [JsonConverter(typeof(ListStatusTypeConverter))]
        public ListStatusType ListStatusType { get; set; }
        /// <summary>
        /// The order status
        /// </summary>
        [JsonConverter(typeof(ListOrderStatusConverter))]
        public ListOrderStatus ListOrderStatus { get; set; }
        /// <summary>
        /// The client id of the order list
        /// </summary>
        public string ListClientOrderId { get; set; } = "";
        /// <summary>
        /// The transaction time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// The symbol of the order list
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The order in this list
        /// </summary>
        public IEnumerable<BinanceOrderId> Orders { get; set; } = new List<BinanceOrderId>();
        /// <summary>
        /// The order details
        /// </summary>
        [JsonOptionalProperty]
        public IEnumerable<BinancePlacedOcoOrder> OrderReports { get; set; } = new List<BinancePlacedOcoOrder>();
    }

    /// <summary>
    /// Order reference
    /// </summary>
    public class BinanceOrderId
    {
        /// <summary>
        /// The symbol of the order
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The id of the order
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// The client order id
        /// </summary>
        public string ClientOrderId { get; set; } = "";
    }

    /// <summary>
    /// The result of placing a new order
    /// </summary>
    public class BinancePlacedOcoOrder: BinanceOrderBase
    {
        /// <summary>
        /// The time the order was placed
        /// </summary>
        [JsonOptionalProperty]
        [JsonProperty("transactTime"), JsonConverter(typeof(TimestampConverter))]
        public new DateTime CreateTime { get; set; }
    }
}
