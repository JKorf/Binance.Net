namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Account Info
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageAccountInfo
    {
        /// <summary>
        /// ["<c>maxMakerCommission</c>"] Max Maker Commission
        /// </summary>
        [JsonPropertyName("maxMakerCommission")]
        public decimal MaxMakerCommission { get; set; }

        /// <summary>
        /// ["<c>minMakerCommission</c>"] Min Maker Commission
        /// </summary>
        [JsonPropertyName("minMakerCommission")]
        public decimal MinMakerCommission { get; set; }

        /// <summary>
        /// ["<c>maxTakerCommission</c>"] Max Taker Commission
        /// </summary>
        [JsonPropertyName("maxTakerCommission")]
        public decimal MaxTakerCommission { get; set; }

        /// <summary>
        /// ["<c>minTakerCommission</c>"] Min Taker Commission
        /// </summary>
        [JsonPropertyName("minTakerCommission")]
        public decimal MinTakerCommission { get; set; }

        /// <summary>
        /// ["<c>subAccountQty</c>"] Sub Account Quantity
        /// </summary>
        [JsonPropertyName("subAccountQty")]
        public int SubAccountQuantity { get; set; }

        /// <summary>
        /// ["<c>maxSubAccountQty</c>"] Max Sub Account Quantity
        /// </summary>
        [JsonPropertyName("maxSubAccountQty")]
        public int MaxSubAccountQuantity { get; set; }
    }
}