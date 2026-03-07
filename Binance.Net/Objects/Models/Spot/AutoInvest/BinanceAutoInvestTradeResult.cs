namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Trade result
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestTradeResult
    {
        /// <summary>
        /// ["<c>transactionId</c>"] The transaction identifier.
        /// </summary>
        [JsonPropertyName("transactionId")]
        public long? TransactionId { get; set; }
        /// <summary>
        /// ["<c>waitSecond</c>"] Wait seconds after which the status should be checked
        /// </summary>
        [JsonPropertyName("waitSecond")]
        public decimal WaitSecond { get; set; }
    }

}

