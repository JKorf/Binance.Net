using Binance.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects
{
    /// <summary>
    /// The result of placing a new order
    /// </summary>
    public class BinancePlacedOrder
    {
        /// <summary>
        /// The symbol the order is for
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// The order id as assigned by Binance
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// The order id as assigned by the client
        /// </summary>
        public string ClientOrderId { get; set; }
        /// <summary>
        /// The time the order was placed
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime TransactTime { get; set; }
    }
}
