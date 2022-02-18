namespace Binance.Net.Enums
{
    /// <summary>
    /// Income type
    /// </summary>
    public enum LoanIncomeType
    {
        /// <summary>
        /// Borrow in
        /// </summary>
        BorrowIn,
        /// <summary>
        /// Collateral spent
        /// </summary>
        CollateralSpent,
        /// <summary>
        /// Repay amount
        /// </summary>
        RepayAmount,
        /// <summary>
        /// Collateral return
        /// </summary>
        CollateralReturn,
        /// <summary>
        /// Add collateral
        /// </summary>
        AddCollateral,
        /// <summary>
        /// Remove collateral
        /// </summary>
        RemoveCollateral,
        /// <summary>
        /// Collateral return after liquidation
        /// </summary>
        CollateralReturnAfterLiquidation
    }
}
