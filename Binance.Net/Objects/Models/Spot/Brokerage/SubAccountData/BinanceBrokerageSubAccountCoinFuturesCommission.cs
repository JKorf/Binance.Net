namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Coin Futures Commission
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageSubAccountCoinFuturesCommission
    {
        /// <summary>
        /// ["<c>subaccountId</c>"] Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>pair</c>"] Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>makerAdjustment</c>"] COIN-Ⓜ futures commission adjustment for maker
        /// </summary>
        [JsonPropertyName("makerAdjustment")]
        public int MakerAdjustment { get; set; }

        /// <summary>
        /// ["<c>takerAdjustment</c>"] COIN-Ⓜ futures commission adjustment for taker
        /// </summary>
        [JsonPropertyName("takerAdjustment")]
        public int TakerAdjustment { get; set; }

        /// <summary>
        /// ["<c>makerCommission</c>"] COIN-Ⓜ futures commission (after adjusted) for maker
        /// </summary>
        [JsonPropertyName("makerCommission")]
        public decimal MakerCommission { get; set; }

        /// <summary>
        /// ["<c>takerCommission</c>"] COIN-Ⓜ futures commission (after adjusted) for taker
        /// </summary>
        [JsonPropertyName("takerCommission")]
        public decimal TakerCommission { get; set; }
    }
}