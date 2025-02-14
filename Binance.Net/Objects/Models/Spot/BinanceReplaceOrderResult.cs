using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// The result of replacing an order
    /// </summary>
    public record BinanceReplaceOrderResult: BinanceReplaceResult
    {
        /// <summary>
        /// Cancel result
        /// </summary>
        [JsonConverter(typeof(PocAOTEnumConverter<OrderOperationResult>))]
        [JsonPropertyName("cancelResult")]
        public OrderOperationResult CancelResult { get; set; }
        /// <summary>
        /// New order result
        /// </summary>
        [JsonConverter(typeof(PocAOTEnumConverter<OrderOperationResult>))]
        [JsonPropertyName("newOrderResult")]
        public OrderOperationResult NewOrderResult { get; set; }
        /// <summary>
        /// Cancel order response. Make sure to check that the CancelResult is Success, else the CancelResponse.Message will contain more info
        /// </summary>
        [JsonPropertyName("cancelResponse")]
        public BinanceReplaceCancelOrder? CancelResponse { get; set; }
        /// <summary>
        /// New order response. Make sure to check that the NewOrderResult is Success, else the NewOrderResponse.Message will contain more info
        /// </summary>
        [JsonPropertyName("newOrderResponse")]
        public BinanceReplaceOrder? NewOrderResponse { get; set; }
    }

    /// <summary>
    /// Replace order
    /// </summary>
    public record BinanceReplaceOrder: BinancePlacedOrder
    {
        /// <summary>
        /// Failure message
        /// </summary>
        [JsonPropertyName("msg")]
        public string? Message { get; set; }
        /// <summary>
        /// Error code if not successful
        /// </summary>
        [JsonPropertyName("code")]
        public int? Code { get; set; }
    }

    /// <summary>
    /// Replace cancel order info
    /// </summary>
    public record BinanceReplaceCancelOrder: BinanceOrderBase
    {
        /// <summary>
        /// Failure message
        /// </summary>
        [JsonPropertyName("msg")]
        public string? Message { get; set; }
        /// <summary>
        /// Error code if not successful
        /// </summary>
        [JsonPropertyName("code")]
        public int? Code { get; set; }
    }

    /// <summary>
    /// Replace result
    /// </summary>
    public record BinanceReplaceResult
    {
        /// <summary>
        /// Failure message
        /// </summary>
        [JsonPropertyName("msg")]
        public string? Message { get; set; }
        /// <summary>
        /// Error code if not successful
        /// </summary>
        [JsonPropertyName("code")]
        public int? Code { get; set; }
    }
}
