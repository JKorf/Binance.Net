namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Commission
    /// </summary>
    public class BinanceBrokerageSubAccountCommission
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = "";
        
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
        /// </summary>
        public decimal MarginMakerCommission { get; set; }
        
        /// <summary>
        /// Margin Taker Commission
        /// </summary>
        public decimal MarginTakerCommission { get; set; }
    }
}