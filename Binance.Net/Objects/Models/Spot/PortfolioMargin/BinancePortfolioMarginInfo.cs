using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.PortfolioMargin
{
    /// <summary>
    /// Portfolio margin account info
    /// </summary>
    [SerializationModel]
    public record BinancePortfolioMarginInfo
    {
        /// <summary>
        /// ["<c>uniMMR</c>"] Portfolio margin account maintenance margin rate
        /// </summary>
        [JsonPropertyName("uniMMR")]
        public decimal UniMaintenanceMarginRate { get; set; }
        /// <summary>
        /// ["<c>accountEquity</c>"] Account equity, in USD
        /// </summary>
        [JsonPropertyName("accountEquity")]
        public decimal AccountEquity { get; set; }
        /// <summary>
        /// ["<c>actualEquity</c>"] Portfolio margin account actual equity, in USD
        /// </summary>
        [JsonPropertyName("actualEquity")]
        public decimal ActualEquity { get; set; }
        /// <summary>
        /// ["<c>accountMaintMargin</c>"] Portfolio margin account maintenance margin, in USD
        /// </summary>
        [JsonPropertyName("accountMaintMargin")]
        public decimal AccountMaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>accountStatus</c>"] Account status
        /// </summary>
        [JsonPropertyName("accountStatus")]
        public PortfolioMarginAccountStatus AccountStatus { get; set; }
        /// <summary>
        /// ["<c>accountType</c>"] Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public string AccountType { get; set; } = string.Empty;
    }
}

