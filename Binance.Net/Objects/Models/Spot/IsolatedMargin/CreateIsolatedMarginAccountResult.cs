namespace Binance.Net.Objects.Models.Spot.IsolatedMargin
{
    /// <summary>
    /// Result of creating isolated margin account
    /// </summary>
    public class CreateIsolatedMarginAccountResult
    {
        /// <summary>
        /// Success
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
    }
}
