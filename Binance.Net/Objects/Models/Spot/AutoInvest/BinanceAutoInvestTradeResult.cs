namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Trade result
    /// </summary>
    public record BinanceAutoInvestTradeResult
    {
        /// <summary>
        /// Transaction id
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
