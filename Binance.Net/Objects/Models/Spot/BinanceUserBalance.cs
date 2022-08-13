namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// User balance
    /// </summary>
    public class BinanceUserBalance : BinanceBalance
    {
        /// <summary>
        /// Frozen
        /// </summary>
        public decimal Freeze { get; set; }
        /// <summary>
        /// Currently withdrawing
        /// </summary>
        public decimal Withdrawing { get; set; }
        /// <summary>
        /// Ipoable amount
        /// </summary>
        public decimal Ipoable { get; set; }
        /// <summary>
        /// Value in btc
        /// </summary>
        public decimal BtcValuation { get; set; }
    }
}
