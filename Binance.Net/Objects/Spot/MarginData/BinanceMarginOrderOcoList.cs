using Binance.Net.Objects.Spot.SpotData;

namespace Binance.Net.Objects.Spot.MarginData
{
    /// <summary>
    /// Oco info
    /// </summary>
    public class BinanceMarginOrderOcoList: BinanceOrderOcoList
    {
        /// <summary>
        /// Margin buy borrow amount
        /// </summary>
        public decimal? MarginBuyBorrowAmount { get; set; }
        /// <summary>
        /// Margin buy borrow asset
        /// </summary>
        public string? MarginBuyBorrowAsset { get; set; }
        /// <summary>
        /// Is isolated margin
        /// </summary>
        public bool IsIsolated { get; set; }
    }
}
