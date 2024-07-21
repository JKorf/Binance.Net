namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// The result of cancel all orders
    /// </summary>
    public record BinanceFuturesCancelAllOrders
    {
        /// <summary>
        /// The execution code
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
        /// The execution message
        /// </summary>
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
    }
}
