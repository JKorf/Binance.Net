namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Commission
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageSubAccountCommission
    {
        /// <summary>
        /// ["<c>subaccountId</c>"] Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>makerCommission</c>"] Maker Commission
        /// </summary>
        [JsonPropertyName("makerCommission")]
        public decimal MakerCommission { get; set; }

        /// <summary>
        /// ["<c>takerCommission</c>"] Taker Commission
        /// </summary>
        [JsonPropertyName("takerCommission")]
        public decimal TakerCommission { get; set; }

        /// <summary>
        /// ["<c>marginMakerCommission</c>"] Margin Maker Commission
        /// <para>If margin disabled, return -1</para>
        /// </summary>
        [JsonPropertyName("marginMakerCommission")]
        public decimal MarginMakerCommission { get; set; }

        /// <summary>
        /// ["<c>marginTakerCommission</c>"] Margin Taker Commission
        /// <para>If margin disabled, return -1</para>
        /// </summary>
        [JsonPropertyName("marginTakerCommission")]
        public decimal MarginTakerCommission { get; set; }
    }
}