namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// The result quantity of getting maxBorrowable or maxTransferable 
    /// </summary>
    [SerializationModel]
    public record BinanceMarginAmount
    {
        /// <summary>
        /// ["<c>amount</c>"] The quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// ["<c>borrowLimit</c>"] The borrow limit
        /// </summary>
        [JsonPropertyName("borrowLimit")]
        public decimal BorrowLimit { get; set; }
    }
}

