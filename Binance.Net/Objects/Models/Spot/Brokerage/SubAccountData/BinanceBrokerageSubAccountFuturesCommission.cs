namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Futures Commission
    /// </summary>
    public class BinanceBrokerageSubAccountFuturesCommission
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Pair
        /// </summary>
        public string Pair { get; set; } = string.Empty;

        /// <summary>
        /// USDT-Ⓜ futures commission adjustment for maker
        /// </summary>
        public int MakerAdjustment { get; set; }

        /// <summary>
        /// USDT-Ⓜ futures commission adjustment for taker
        /// </summary>
        public int TakerAdjustment { get; set; }

        /// <summary>
        /// USDT-Ⓜ futures commission (after adjusted) for maker
        /// </summary>
        public decimal MakerCommission { get; set; }

        /// <summary>
        /// USDT-Ⓜ futures commission (after adjusted) for taker
        /// </summary>
        public decimal TakerCommission { get; set; }
    }
}