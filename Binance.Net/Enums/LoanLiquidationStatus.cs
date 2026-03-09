using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Loan liquidation status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<LoanLiquidationStatus>))]
    public enum LoanLiquidationStatus
    {
        /// <summary>
        /// ["<c>Liquidated</c>"] Liquidated
        /// </summary>
        [Map("Liquidated")]
        Liquidated,
        /// <summary>
        /// ["<c>Liquidating</c>"] Liquidating
        /// </summary>
        [Map("Liquidating")]
        Liquidating
    }
}

