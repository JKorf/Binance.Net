using System;
using System.Collections.Generic;
using Binance.Net.Converters;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{   //TODO
    /// <summary>
    /// Order list info
    /// </summary>
    public class BinanceStreamMarginOrderList: BinanceStreamEvent
    {
        /// <summary>
        /// The id of the order list
        /// </summary>
        [JsonProperty("g")]
        public long OrderListId { get; set; }
        /// <summary>
        /// The contingency type
        /// </summary>
        [JsonProperty("c")]
        public string ContingencyType { get; set; } = "";
        /// <summary>
        /// The order list status
        /// </summary>
        [JsonConverter(typeof(ListStatusTypeConverter))]
        [JsonProperty("l")]
        public ListStatusType ListStatusType { get; set; }
        /// <summary>
        /// The order status
        /// </summary>
        [JsonConverter(typeof(ListOrderStatusConverter))]
        [JsonProperty("L")]
        public ListOrderStatus ListOrderStatus { get; set; }
        /// <summary>
        /// The client id of the order list
        /// </summary>
        [JsonProperty("C")]
        public string ListClientOrderId { get; set; } = "";
        /// <summary>
        /// The transaction time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        [JsonProperty("T")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// The symbol of the order list
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The order in this list
        /// </summary>
        [JsonProperty("O")]
        public IEnumerable<BinanceStreamMarginOrderId> Orders { get; set; } = new List<BinanceStreamMarginOrderId>();
    }

    /// <summary>
    /// Order reference
    /// </summary>
    public class BinanceStreamMarginOrderId
    {
        /// <summary>
        /// The symbol of the order
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonProperty("i")]
        public long OrderId { get; set; }
        /// <summary>
        /// The client order id
        /// </summary>
        [JsonProperty("c")]
        public string ClientOrderId { get; set; } = "";
    }
}
