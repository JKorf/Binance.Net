namespace Binance.Net.Objects.Models.Spot.Lending
{
    /// <summary>
    /// Purchase result
    /// </summary>
    public record BinanceLendingPurchaseResult
    {
        /// <summary>
        /// The id of the purchase
        /// </summary>
        public long PurchaseId { get; set; }
    }
}
