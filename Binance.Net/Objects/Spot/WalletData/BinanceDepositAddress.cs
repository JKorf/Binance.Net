namespace Binance.Net.Objects.Spot.WalletData
{
    /// <summary>
    /// Deposit address info
    /// </summary>
    public class BinanceDepositAddress
    {
        /// <summary>
        /// The deposit address
        /// </summary>
        public string Address { get; set; } = "";
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; } = "";
        /// <summary>
        /// Address tag
        /// </summary>
        public string Tag { get; set; } = "";
        /// <summary>
        /// Coin the address is for
        /// </summary>
        public string Coin { get; set; } = "";
    }
}
