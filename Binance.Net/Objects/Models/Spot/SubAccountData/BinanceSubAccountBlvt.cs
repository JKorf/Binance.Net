namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account details
    /// </summary>
    public record BinanceSubAccountBlvt
    {
        /// <summary>
        /// The email associated with the sub account
        /// </summary>
        public string Email { get; set; } = string.Empty;      
        /// <summary>
        /// Blvt enabled
        /// </summary>
        public bool EnableBlvt { get; set; }
    }
}
