namespace Binance.Net.Objects.Spot.SubAccountData
{
    /// <summary>
    /// Deposit address info for a sub-account
    /// </summary>
    public class BinanceSubAccountDepositAddress
    {
        /// <summary>
        /// The deposit address
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Coin type
        /// </summary>
        public string Coin { get; set; } = string.Empty;
        /// <summary>
        /// Tag for the deposit address
        /// </summary>
        public string Tag { get; set; } = string.Empty;
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; } = string.Empty;
    }
}
