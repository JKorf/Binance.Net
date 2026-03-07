namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Futures Commission
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageSubAccountFuturesCommission
    {
        /// <summary>
        /// ["<c>subaccountId</c>"] Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>pair</c>"] Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>makerAdjustment</c>"] USDT-Ⓜ futures commission adjustment for maker
        /// </summary>
        [JsonPropertyName("makerAdjustment")]
        public int MakerAdjustment { get; set; }

        /// <summary>
        /// ["<c>takerAdjustment</c>"] USDT-Ⓜ futures commission adjustment for taker
        /// </summary>
        [JsonPropertyName("takerAdjustment")]
        public int TakerAdjustment { get; set; }

        /// <summary>
        /// ["<c>makerCommission</c>"] USDT-Ⓜ futures commission (after adjusted) for maker
        /// </summary>
        [JsonPropertyName("makerCommission")]
        public decimal MakerCommission { get; set; }

        /// <summary>
        /// ["<c>takerCommission</c>"] USDT-Ⓜ futures commission (after adjusted) for taker
        /// </summary>
        [JsonPropertyName("takerCommission")]
        public decimal TakerCommission { get; set; }
    }
}