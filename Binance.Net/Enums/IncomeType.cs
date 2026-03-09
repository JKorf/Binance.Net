using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of futures income
    /// </summary>
    [JsonConverter(typeof(EnumConverter<IncomeType>))]
    public enum IncomeType
    {
        /// <summary>
        /// ["<c>TRANSFER</c>"] Transfer into account
        /// </summary>
        [Map("TRANSFER")]
        Transfer,
        /// <summary>
        /// ["<c>WELCOME_BONUS</c>"] Futures welcome bonus
        /// </summary>
        [Map("WELCOME_BONUS")]
        WelcomeBonus,
        /// <summary>
        /// ["<c>REALIZED_PNL</c>"] Futures realized profit
        /// </summary>
        [Map("REALIZED_PNL")]
        RealizedPnl,
        /// <summary>
        /// ["<c>FUNDING_FEE</c>"] Futures funding fee
        /// </summary>
        [Map("FUNDING_FEE")]
        FundingFee,
        /// <summary>
        /// ["<c>COMMISSION</c>"] Futures trading commission
        /// </summary>
        [Map("COMMISSION")]
        Commission,
        /// <summary>
        /// ["<c>INSURANCE_CLEAR</c>"] Insurance clear
        /// </summary>
        [Map("INSURANCE_CLEAR")]
        InsuranceClear,
        /// <summary>
        /// ["<c>REFERRAL_KICKBACK</c>"] Referral kickback
        /// </summary>
        [Map("REFERRAL_KICKBACK")]
        ReferralKickback,
        /// <summary>
        /// ["<c>COMMISSION_REBATE</c>"] Commission rebate
        /// </summary>
        [Map("COMMISSION_REBATE")]
        CommissionRebate,
        /// <summary>
        /// ["<c>API_REBATE</c>"] Api rebate
        /// </summary>
        [Map("API_REBATE")]
        ApiRebate,
        /// <summary>
        /// ["<c>CONTEST_REWARD</c>"] Contest reward
        /// </summary>
        [Map("CONTEST_REWARD")]
        ContestReward,
        /// <summary>
        /// ["<c>CROSS_COLLATERAL_TRANSFER</c>"] Cross collateral transfer
        /// </summary>
        [Map("CROSS_COLLATERAL_TRANSFER")]
        CrossCollateralTransfer,
        /// <summary>
        /// ["<c>OPTIONS_PREMIUM_FEE</c>"] Options premium fee
        /// </summary>
        [Map("OPTIONS_PREMIUM_FEE")]
        OptionsPremiumFee,
        /// <summary>
        /// ["<c>OPTIONS_SETTLE_PROFIT</c>"] Options settle profit
        /// </summary>
        [Map("OPTIONS_SETTLE_PROFIT")]
        OptionsSettleProfit,
        /// <summary>
        /// ["<c>INTERNAL_TRANSFER</c>"] Internal transfer
        /// </summary>
        [Map("INTERNAL_TRANSFER")]
        InternalTransfer,
        /// <summary>
        /// ["<c>AUTO_EXCHANGE</c>"] Auto exchange
        /// </summary>
        [Map("AUTO_EXCHANGE")]
        AutoExchange,
        /// <summary>
        /// ["<c>DELIVERED_SETTELMENT</c>"] Delivered settlement
        /// </summary>
        [Map("DELIVERED_SETTELMENT")]
        DeliveredSettlement,
        /// <summary>
        /// ["<c>COIN_SWAP_DEPOSIT</c>"] Coin swap deposit
        /// </summary>
        [Map("COIN_SWAP_DEPOSIT")]
        CoinSwapDeposit,
        /// <summary>
        /// ["<c>COIN_SWAP_WITHDRAW</c>"] Coin swap withdraw
        /// </summary>
        [Map("COIN_SWAP_WITHDRAW")]
        CoinSwapWithdraw,
        /// <summary>
        /// ["<c>POSITION_LIMIT_INCREASE_FEE</c>"] Position limit increase fee
        /// </summary>
        [Map("POSITION_LIMIT_INCREASE_FEE")]
        PositionLimitIncreaseFee
    }
}

