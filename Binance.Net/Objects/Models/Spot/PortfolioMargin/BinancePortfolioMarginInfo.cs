using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.PortfolioMargin
{
    /// <summary>
    /// Portfolio margin account info
    /// </summary>
    public record BinancePortfolioMarginInfo
    {
        /// <summary>
        /// Portfolio margin account maintenance margin rate
        /// </summary>
        [JsonPropertyName("uniMMR")]
        public decimal UniMaintenanceMarginRate { get; set; }
        /// <summary>
        /// Account equity, in USD
        /// </summary>
        [JsonPropertyName("accountEquity")]
        public decimal AccountEquity { get; set; }
        /// <summary>
        /// Portfolio margin account actual equity, in USD
        /// </summary>
        [JsonPropertyName("actualEquity")]
        public decimal ActualEquity { get; set; }
        /// <summary>
        /// Portfolio margin account maintenance margin, in USD
        /// </summary>
        [JsonPropertyName("accountMaintMargin")]
        public decimal AccountMaintenanceMargin { get; set; }
        /// <summary>
        /// Account status
        /// </summary>
        [JsonConverter(typeof(PocAOTEnumConverter<PortfolioMarginAccountStatus>))]
        [JsonPropertyName("accountStatus")]
        public PortfolioMarginAccountStatus AccountStatus { get; set; }
        /// <summary>
        /// Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public string AccountType { get; set; } = string.Empty;
    }
}
