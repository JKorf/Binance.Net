namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Coin Futures Commission
    /// </summary>
    public record BinanceBrokerageSubAccountCoinFuturesCommission
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;

        /// <summary>
        /// COIN-Ⓜ futures commission adjustment for maker
        /// </summary>
        [JsonPropertyName("makerAdjustment")]
        public int MakerAdjustment { get; set; }

        /// <summary>
        /// COIN-Ⓜ futures commission adjustment for taker
        /// </summary>
        [JsonPropertyName("takerAdjustment")]
        public int TakerAdjustment { get; set; }

        /// <summary>
        /// COIN-Ⓜ futures commission (after adjusted) for maker
        /// </summary>
        [JsonPropertyName("makerCommission")]
        public decimal MakerCommission { get; set; }

        /// <summary>
        /// COIN-Ⓜ futures commission (after adjusted) for taker
        /// </summary>
        [JsonPropertyName("takerCommission")]
        public decimal TakerCommission { get; set; }
    }
}