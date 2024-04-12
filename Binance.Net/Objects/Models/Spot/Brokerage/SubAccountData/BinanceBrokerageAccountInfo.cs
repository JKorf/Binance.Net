namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Account Info
    /// </summary>
    public class BinanceBrokerageAccountInfo
    {
        /// <summary>
        /// Max Maker Commission
        /// </summary>
        public decimal MaxMakerCommission { get; set; }
        
        /// <summary>
        /// Min Maker Commission
        /// </summary>
        public decimal MinMakerCommission { get; set; }
        
        /// <summary>
        /// Max Taker Commission
        /// </summary>
        public decimal MaxTakerCommission { get; set; }
        
        /// <summary>
        /// Min Taker Commission
        /// </summary>
        public decimal MinTakerCommission { get; set; }
        
        /// <summary>
        /// Sub Account Quantity
        /// </summary>
        [JsonProperty("subAccountQty")]
        public int SubAccountQuantity { get; set; }
        
        /// <summary>
        /// Max Sub Account Quantity
        /// </summary>
        [JsonProperty("maxSubAccountQty")]
        public int MaxSubAccountQuantity { get; set; }
    }
}