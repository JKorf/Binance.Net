using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Income type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<LoanIncomeType>))] public  enum LoanIncomeType
    {
        /// <summary>
        /// Borrow in
        /// </summary>
        [Map("borrowIn")]
        BorrowIn,
        /// <summary>
        /// Collateral spent
        /// </summary>
        [Map("collateralSpent")]
        CollateralSpent,
        /// <summary>
        /// Repay amount
        /// </summary>
        [Map("repayAmount")]
        RepayAmount,
        /// <summary>
        /// Collateral return
        /// </summary>
        [Map("collateralReturn")]
        CollateralReturn,
        /// <summary>
        /// Add collateral
        /// </summary>
        [Map("addCollateral")]
        AddCollateral,
        /// <summary>
        /// Remove collateral
        /// </summary>
        [Map("removeCollateral")]
        RemoveCollateral,
        /// <summary>
        /// Collateral return after liquidation
        /// </summary>
        [Map("collateralReturnAfterLiquidation")]
        CollateralReturnAfterLiquidation
    }
}
