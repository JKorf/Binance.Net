namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// The result of transferring
    /// </summary>
    public record BinanceTransaction
    {
        /// <summary>
        /// The Transaction id as assigned by Binance
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }
    }
}
