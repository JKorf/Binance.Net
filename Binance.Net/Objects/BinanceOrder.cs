using System;
using Binance.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceOrder
    {
        public string Symbol { get; set; }
        public long OrderId { get; set; }
        public string ClientOrderId { get; set; }
        public double Price { get; set; }
        [JsonProperty("origQty")]
        public double OriginalQuantity { get; set; }
        [JsonProperty("executedQty")]
        public double ExecutedQuantity { get; set; }
        [JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status { get; set; }
        [JsonConverter(typeof(TimeInForceConverter))]
        public TimeInForce TimeInForce { get; set; }
        [JsonConverter(typeof(OrderTypeConverter))]
        public OrderType Type { get; set; }
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        public double StopPrice { get; set; }
        [JsonProperty("icebergQty")]
        public double IcebergQuantity { get; set; }
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
    }
}
