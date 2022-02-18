namespace Binance.Net.Objects.Models.Spot.Lending
{
    /// <summary>
    /// Purchase quota left
    /// </summary>
    public class BinancePurchaseQuotaLeft
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// The quota left
        /// </summary>
        public decimal LeftQuota { get; set; }
    }
}
