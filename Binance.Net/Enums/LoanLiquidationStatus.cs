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
        /// Liquidated
        /// </summary>
        [Map("Liquidated")]
        Liquidated,
        /// <summary>
        /// Liquidating
        /// </summary>
        [Map("Liquidating")]
        Liquidating
    }
}
