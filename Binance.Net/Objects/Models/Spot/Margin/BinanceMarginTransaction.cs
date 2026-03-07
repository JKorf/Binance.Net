namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// The result of transferring
    /// </summary>
    [SerializationModel]
    public record BinanceTransaction
    {
        /// <summary>
        /// ["<c>tranId</c>"] The Transaction id as assigned by Binance
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }
    }
}

