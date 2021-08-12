namespace Binance.Net.Objects.Spot.SubAccountData
{
    /// <summary>
    /// Sub account futures trading enabled
    /// </summary>
    public class BinanceSubAccountFuturesEnabled
    {
        /// <summary>
        /// Email of the account
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Whether futures trading is enabled
        /// </summary>
        public bool IsFuturesEnabled { get; set; }
    }
}
