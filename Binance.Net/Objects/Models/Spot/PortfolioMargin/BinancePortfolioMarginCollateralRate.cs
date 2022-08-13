using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
namespace Binance.Net.Objects.Models.Spot.PortfolioMargin
{
    /// <summary>
    /// Portfolio margin collateral rate info
    /// </summary>
    public class BinancePortfolioMarginCollateralRate
    {
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Collateral rate
        /// </summary>
        public decimal CollateralRate { get; set; }
    }
}
