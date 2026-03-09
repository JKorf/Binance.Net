using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Income type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<LoanIncomeType>))]
    public enum LoanIncomeType
    {
        /// <summary>
        /// ["<c>borrowIn</c>"] Borrow in
        /// </summary>
        [Map("borrowIn")]
        BorrowIn,
        /// <summary>
        /// ["<c>collateralSpent</c>"] Collateral spent
        /// </summary>
        [Map("collateralSpent")]
        CollateralSpent,
        /// <summary>
        /// ["<c>repayAmount</c>"] Repay amount
        /// </summary>
        [Map("repayAmount")]
        RepayAmount,
        /// <summary>
        /// ["<c>collateralReturn</c>"] Collateral return
        /// </summary>
        [Map("collateralReturn")]
        CollateralReturn,
        /// <summary>
        /// ["<c>addCollateral</c>"] Add collateral
        /// </summary>
        [Map("addCollateral")]
        AddCollateral,
        /// <summary>
        /// ["<c>removeCollateral</c>"] Remove collateral
        /// </summary>
        [Map("removeCollateral")]
        RemoveCollateral,
        /// <summary>
        /// ["<c>collateralReturnAfterLiquidation</c>"] Collateral return after liquidation
        /// </summary>
        [Map("collateralReturnAfterLiquidation")]
        CollateralReturnAfterLiquidation
    }
}

