namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Commission
    /// </summary>
    public record BinanceBrokerageSubAccountCommission
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// Maker Commission
        /// </summary>
        [JsonPropertyName("makerCommission")]
        public decimal MakerCommission { get; set; }

        /// <summary>
        /// Taker Commission
        /// </summary>
        [JsonPropertyName("takerCommission")]
        public decimal TakerCommission { get; set; }

        /// <summary>
        /// Margin Maker Commission
        /// <para>If margin disabled, return -1</para>
        /// </summary>
        [JsonPropertyName("marginMakerCommission")]
        public decimal MarginMakerCommission { get; set; }

        /// <summary>
        /// Margin Taker Commission
        /// <para>If margin disabled, return -1</para>
        /// </summary>
        [JsonPropertyName("marginTakerCommission")]
        public decimal MarginTakerCommission { get; set; }
    }
}