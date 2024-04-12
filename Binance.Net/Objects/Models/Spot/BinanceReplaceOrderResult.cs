using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// The result of replacing an order
    /// </summary>
    public class BinanceReplaceOrderResult: BinanceReplaceResult
    {
        /// <summary>
        /// Cancel result
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public OrderOperationResult CancelResult { get; set; }
        /// <summary>
        /// New order result
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public OrderOperationResult NewOrderResult { get; set; }
        /// <summary>
        /// Cancel order response. Make sure to check that the CancelResult is Success, else the CancelResponse.Message will contain more info
        /// </summary>
        public BinanceReplaceCancelOrder? CancelResponse { get; set; }
        /// <summary>
        /// New order response. Make sure to check that the NewOrderResult is Success, else the NewOrderResponse.Message will contain more info
        /// </summary>
        public BinanceReplaceOrder? NewOrderResponse { get; set; }
    }

    /// <summary>
    /// Replace order
    /// </summary>
    public class BinanceReplaceOrder: BinancePlacedOrder
    {
        /// <summary>
        /// Failure message
        /// </summary>
        [JsonProperty("msg")]
        public string? Message { get; set; }
        /// <summary>
        /// Error code if not successful
        /// </summary>
        public int? Code { get; set; }
    }

    /// <summary>
    /// Replace cancel order info
    /// </summary>
    public class BinanceReplaceCancelOrder: BinanceOrderBase
    {
        /// <summary>
        /// Failure message
        /// </summary>
        [JsonProperty("msg")]
        public string? Message { get; set; }
        /// <summary>
        /// Error code if not successful
        /// </summary>
        public int? Code { get; set; }
    }

    /// <summary>
    /// Replace result
    /// </summary>
    public class BinanceReplaceResult
    {
        /// <summary>
        /// Failure message
        /// </summary>
        [JsonProperty("msg")]
        public string? Message { get; set; }
        /// <summary>
        /// Error code if not successful
        /// </summary>
        public int? Code { get; set; }
    }
}
