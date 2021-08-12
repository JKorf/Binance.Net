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
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// Address tag
        /// </summary>
        public string Tag { get; set; } = string.Empty;
        /// <summary>
        /// Coin the address is for
        /// </summary>
        public string Coin { get; set; } = string.Empty;
    }
}
