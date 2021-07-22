namespace Binance.Net.Objects.Spot.WalletData
{
    /// <summary>
    /// Funding wallet asset
    /// </summary>
    public class BinanceFundingAsset
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Amount available
        /// </summary>
        public decimal Free { get; set; }
        /// <summary>
        /// Amount locked
        /// </summary>
        public decimal Locked { get; set; }
        /// <summary>
        /// Amount frozen
        /// </summary>
        public decimal Freeze { get; set; }
        /// <summary>
        /// Amount withdrawing
        /// </summary>
        public decimal Withdrawing { get; set; }
        /// <summary>
        /// Value in btc
        /// </summary>
        public decimal BtcValuation { get; set; }
    }
}
