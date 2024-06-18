namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Convert result
    /// </summary>
    public record BinanceBusdConvertResult
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("tranId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;
    }
}
