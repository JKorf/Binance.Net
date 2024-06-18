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
        public string SubAccountId { get; set; } = string.Empty;
        
        /// <summary>
        /// Maker Commission
        /// </summary>
        public decimal MakerCommission { get; set; }
        
        /// <summary>
        /// Taker Commission
        /// </summary>
        public decimal TakerCommission { get; set; }
        
        /// <summary>
        /// Margin Maker Commission
        /// <para>If margin disabled, return -1</para>
        /// </summary>
        public decimal MarginMakerCommission { get; set; }
        
        /// <summary>
        /// Margin Taker Commission
        /// <para>If margin disabled, return -1</para>
        /// </summary>
        public decimal MarginTakerCommission { get; set; }
    }
}