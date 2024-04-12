using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class IncomeTypeConverter: BaseConverter<IncomeType>
    {
        public IncomeTypeConverter(): this(true) { }
        public IncomeTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<IncomeType, string>> Mapping => new List<KeyValuePair<IncomeType, string>>
        {
            new KeyValuePair<IncomeType, string>(IncomeType.Transfer, "TRANSFER"),
            new KeyValuePair<IncomeType, string>(IncomeType.WelcomeBonus, "WELCOME_BONUS"),
            new KeyValuePair<IncomeType, string>(IncomeType.RealizedPnl, "REALIZED_PNL"),
            new KeyValuePair<IncomeType, string>(IncomeType.FundingFee, "FUNDING_FEE"),
            new KeyValuePair<IncomeType, string>(IncomeType.Commission, "COMMISSION"),
            new KeyValuePair<IncomeType, string>(IncomeType.InsuranceClear, "INSURANCE_CLEAR"),
            new KeyValuePair<IncomeType, string>(IncomeType.ReferralKickback, "REFERRAL_KICKBACK"),
            new KeyValuePair<IncomeType, string>(IncomeType.CommissionRebate, "COMMISSION_REBATE"),
            new KeyValuePair<IncomeType, string>(IncomeType.ApiRebate, "API_REBATE"),
            new KeyValuePair<IncomeType, string>(IncomeType.ContestReward, "CONTEST_REWARD"),
            new KeyValuePair<IncomeType, string>(IncomeType.CrossCollateralTransfer, "CROSS_COLLATERAL_TRANSFER"),
            new KeyValuePair<IncomeType, string>(IncomeType.OptionsPremiumFee, "OPTIONS_PREMIUM_FEE"),
            new KeyValuePair<IncomeType, string>(IncomeType.OptionsSettleProfit, "OPTIONS_SETTLE_PROFIT"),
            new KeyValuePair<IncomeType, string>(IncomeType.InternalTransfer, "INTERNAL_TRANSFER"),
            new KeyValuePair<IncomeType, string>(IncomeType.AutoExchange, "AUTO_EXCHANGE"),
            new KeyValuePair<IncomeType, string>(IncomeType.DeliveredSettlement, "DELIVERED_SETTELMENT"),
            new KeyValuePair<IncomeType, string>(IncomeType.CoinSwapDeposit, "COIN_SWAP_DEPOSIT"),
            new KeyValuePair<IncomeType, string>(IncomeType.CoinSwapWithdraw, "COIN_SWAP_WITHDRAW"),
            new KeyValuePair<IncomeType, string>(IncomeType.PositionLimitIncreaseFee, "POSITION_LIMIT_INCREASE_FEE"),
        };
    }
}
