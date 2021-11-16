
namespace Binance.Net.Objects.Spot.IsolatedMarginData
{
    /// <summary>
    /// Enabled account limit
    /// </summary>
    public class IsolatedMarginAccountLimit
    {
        /// <summary>
        /// Current enabled accounts
        /// </summary>
        public int EnabledAccount { get; set; }
        /// <summary>
        /// Max accounts
        /// </summary>
        public int MaxAccount { get; set; }
    }
}