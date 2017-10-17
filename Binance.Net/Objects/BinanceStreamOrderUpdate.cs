using System;
using Newtonsoft.Json;
using Binance.Net.Converters;

namespace Binance.Net.Objects
{
    public class BinanceStreamOrderUpdate: BinanceStreamEvent
    {
        [JsonProperty("s")]
        public string Symbol { get; set; }
        [JsonProperty("c")]
        public string NewClientOrderId { get; set; }
        [JsonProperty("S"), JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        [JsonProperty("o"), JsonConverter(typeof(OrderTypeConverter))]
        public OrderType Type { get; set; }
        [JsonProperty("f"), JsonConverter(typeof(TimeInForceConverter))]
        public TimeInForce TimeInForce { get; set; }
        [JsonProperty("q")]
        public double Quantity { get; set; }
        [JsonProperty("p")]
        public double Price { get; set; }
        [JsonProperty("P")]
        public double P { get; set; }
        [JsonProperty("F")]
        public double F { get; set; }
        [JsonProperty("g")]
        public double g { get; set; }
        [JsonProperty("C")]
        public object C { get; set; }
        [JsonProperty("x"), JsonConverter(typeof(ExecutionTypeConverter))]
        public ExecutionType ExecutionType { get; set; }
        [JsonProperty("X"), JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status { get; set; }
        [JsonProperty("r"), JsonConverter(typeof(OrderRejectReasonConverter))]
        public OrderRejectReason RejectReason { get; set; }
        [JsonProperty("i")]
        public int OrderId { get; set; }
        [JsonProperty("l")]
        public double QuantityOfLastFilledTrade { get; set; }
        [JsonProperty("z")]
        public double AccumulatedQuantityOfFilledTrades { get; set; }
        [JsonProperty("L")]
        public double PriceLastFilledTrade { get; set; }
        [JsonProperty("n")]
        public double Commission { get; set; }
        [JsonProperty("N")]
        public string CommissionAsset { get; set; }
        [JsonProperty("T"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
        [JsonProperty("t")]
        public double TradeId { get; set; }
        [JsonProperty("I")]
        public long I { get; set; }
        [JsonProperty("m")]
        public bool BuyerIsMaker { get; set; }
    }
}
