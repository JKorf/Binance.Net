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
        /// Whether the call was successful
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Address tag
        /// </summary>
        public string AddressTag { get; set; } = "";
        /// <summary>
        /// Asset the address is for
        /// </summary>
        public string Asset { get; set; } = "";
    }
}
