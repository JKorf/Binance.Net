namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Transaction
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountTransaction
    {
        /// <summary>
        /// ["<c>txnId</c>"] The transaction identifier.
        /// </summary>
        [JsonPropertyName("txnId"), JsonConverter(typeof(NumberStringConverter))]
        public string TransactionId { get; set; } = string.Empty;
    }
}

