using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class LoanIncomeTypeConverter : BaseConverter<LoanIncomeType>
    {
        public LoanIncomeTypeConverter(): this(true) { }
        public LoanIncomeTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<LoanIncomeType, string>> Mapping => new List<KeyValuePair<LoanIncomeType, string>>
        {
            new KeyValuePair<LoanIncomeType, string>(LoanIncomeType.AddCollateral, "addCollateral"),
            new KeyValuePair<LoanIncomeType, string>(LoanIncomeType.BorrowIn, "borrowIn"),
            new KeyValuePair<LoanIncomeType, string>(LoanIncomeType.CollateralReturn, "collateralReturn"),
            new KeyValuePair<LoanIncomeType, string>(LoanIncomeType.CollateralReturnAfterLiquidation, "collateralReturnAfterLiquidation"),
            new KeyValuePair<LoanIncomeType, string>(LoanIncomeType.CollateralSpent, "collateralSpent"),
            new KeyValuePair<LoanIncomeType, string>(LoanIncomeType.RemoveCollateral, "removeCollateral"),
            new KeyValuePair<LoanIncomeType, string>(LoanIncomeType.RepayAmount, "repayAmount"),
        };
    }
}
