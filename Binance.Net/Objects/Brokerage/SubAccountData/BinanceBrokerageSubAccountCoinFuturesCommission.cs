namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Coin Futures Commission
    /// </summary>
    public class BinanceBrokerageSubAccountCoinFuturesCommission
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = "";
        
        /// <summary>
        /// Pair
        /// </summary>
        public string Pair { get; set; } = "";
        
        /// <summary>
        /// COIN-Ⓜ futures commission adjustment for maker
        /// </summary>
        public int MakerAdjustment { get; set; }
        
        /// <summary>
        /// COIN-Ⓜ futures commission adjustment for taker
        /// </summary>
        public int TakerAdjustment { get; set; }
        
        /// <summary>
        /// COIN-Ⓜ futures commission (after adjusted) for maker
        /// </summary>
        public decimal MakerCommission { get; set; }
        
        /// <summary>
        /// COIN-Ⓜ futures commission (after adjusted) for taker
        /// </summary>
        public decimal TakerCommission { get; set; }
    }
}