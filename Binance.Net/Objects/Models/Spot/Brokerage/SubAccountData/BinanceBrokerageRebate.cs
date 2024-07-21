namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Brokerage Rebate
    /// </summary>
    public record BinanceBrokerageRebate
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        [JsonPropertyName("subAccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// Income
        /// </summary>
        [JsonPropertyName("income")]
        public decimal Income { get; set; }

        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        
        /// <summary>
        /// Trade Id
        /// </summary>
        [JsonPropertyName("tradeId")]
        public string TradeId { get; set; } = string.Empty;

        /// <summary>
        /// Date
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Date { get; set; }
    }
}