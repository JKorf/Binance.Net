namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Futures Commission Rebate
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageFuturesRebate
    {
        /// <summary>
        /// ["<c>subaccountId</c>"] Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>income</c>"] Income
        /// </summary>
        [JsonPropertyName("income")]
        public decimal Income { get; set; }

        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>tradeId</c>"] TradeId
        /// </summary>
        [JsonPropertyName("tradeId")]
        public long TradeId { get; set; }

        /// <summary>
        /// ["<c>time</c>"] Date
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Date { get; set; }
    }
}