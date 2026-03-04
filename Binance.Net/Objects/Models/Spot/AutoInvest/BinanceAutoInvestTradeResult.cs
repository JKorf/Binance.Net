namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Trade result
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestTradeResult
    {
        /// <summary>
        /// The transaction identifier.
        /// </summary>
        [JsonPropertyName("transactionId")]
        public long? TransactionId { get; set; }
        /// <summary>
        /// Wait seconds after which the status should be checked
        /// </summary>
        [JsonPropertyName("waitSecond")]
        public decimal WaitSecond { get; set; }
    }

}
