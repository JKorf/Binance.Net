namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Account Info
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageAccountInfo
    {
        /// <summary>
        /// Max Maker Commission
        /// </summary>
        [JsonPropertyName("maxMakerCommission")]
        public decimal MaxMakerCommission { get; set; }

        /// <summary>
        /// Min Maker Commission
        /// </summary>
        [JsonPropertyName("minMakerCommission")]
        public decimal MinMakerCommission { get; set; }

        /// <summary>
        /// Max Taker Commission
        /// </summary>
        [JsonPropertyName("maxTakerCommission")]
        public decimal MaxTakerCommission { get; set; }

        /// <summary>
        /// Min Taker Commission
        /// </summary>
        [JsonPropertyName("minTakerCommission")]
        public decimal MinTakerCommission { get; set; }

        /// <summary>
        /// Sub Account Quantity
        /// </summary>
        [JsonPropertyName("subAccountQty")]
        public int SubAccountQuantity { get; set; }

        /// <summary>
        /// Max Sub Account Quantity
        /// </summary>
        [JsonPropertyName("maxSubAccountQty")]
        public int MaxSubAccountQuantity { get; set; }
    }
}