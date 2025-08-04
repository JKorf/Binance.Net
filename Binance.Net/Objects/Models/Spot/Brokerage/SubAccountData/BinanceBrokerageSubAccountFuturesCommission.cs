namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Futures Commission
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageSubAccountFuturesCommission
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;

        /// <summary>
        /// USDT-Ⓜ futures commission adjustment for maker
        /// </summary>
        [JsonPropertyName("makerAdjustment")]
        public int MakerAdjustment { get; set; }

        /// <summary>
        /// USDT-Ⓜ futures commission adjustment for taker
        /// </summary>
        [JsonPropertyName("takerAdjustment")]
        public int TakerAdjustment { get; set; }

        /// <summary>
        /// USDT-Ⓜ futures commission (after adjusted) for maker
        /// </summary>
        [JsonPropertyName("makerCommission")]
        public decimal MakerCommission { get; set; }

        /// <summary>
        /// USDT-Ⓜ futures commission (after adjusted) for taker
        /// </summary>
        [JsonPropertyName("takerCommission")]
        public decimal TakerCommission { get; set; }
    }
}