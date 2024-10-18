namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Transaction
    /// </summary>
    public record BinanceSubAccountTransaction
    {
        /// <summary>
        /// The transaction id
        /// </summary>
        [JsonPropertyName("txnId"), JsonConverter(typeof(NumberStringConverter))]
        public string TransactionId { get; set; } = string.Empty;
    }
}
