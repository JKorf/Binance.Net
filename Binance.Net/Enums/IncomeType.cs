using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of futures income
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<IncomeType>))] public  enum IncomeType
    {
        /// <summary>
        /// Transfer into account
        /// </summary>
        [Map("TRANSFER")]
        Transfer,
        /// <summary>
        /// Futures welcome bonus
        /// </summary>
        [Map("WELCOME_BONUS")]
        WelcomeBonus,
        /// <summary>
        /// Futures realized profit
        /// </summary>
        [Map("REALIZED_PNL")]
        RealizedPnl,
        /// <summary>
        /// Futures funding fee
        /// </summary>
        [Map("FUNDING_FEE")]
        FundingFee,
        /// <summary>
        /// Futures trading commission
        /// </summary>
        [Map("COMMISSION")]
        Commission,
        /// <summary>
        /// Insurance clear
        /// </summary>
        [Map("INSURANCE_CLEAR")]
        InsuranceClear,
        /// <summary>
        /// Referral kickback
        /// </summary>
        [Map("REFERRAL_KICKBACK")]
        ReferralKickback,
        /// <summary>
        /// Commission rebate
        /// </summary>
        [Map("COMMISSION_REBATE")]
        CommissionRebate,
        /// <summary>
        /// Api rebate
        /// </summary>
        [Map("API_REBATE")]
        ApiRebate,
        /// <summary>
        /// Contest reward
        /// </summary>
        [Map("CONTEST_REWARD")]
        ContestReward,
        /// <summary>
        /// Cross collateral transfer
        /// </summary>
        [Map("CROSS_COLLATERAL_TRANSFER")]
        CrossCollateralTransfer,
        /// <summary>
        /// Options premium fee
        /// </summary>
        [Map("OPTIONS_PREMIUM_FEE")]
        OptionsPremiumFee,
        /// <summary>
        /// Options settle profit
        /// </summary>
        [Map("OPTIONS_SETTLE_PROFIT")]
        OptionsSettleProfit,
        /// <summary>
        /// Internal transfer
        /// </summary>
        [Map("INTERNAL_TRANSFER")]
        InternalTransfer,
        /// <summary>
        /// Auto exchange
        /// </summary>
        [Map("AUTO_EXCHANGE")]
        AutoExchange,
        /// <summary>
        /// Delivered settlement
        /// </summary>
        [Map("DELIVERED_SETTELMENT")]
        DeliveredSettlement,
        /// <summary>
        /// Coin swap deposit
        /// </summary>
        [Map("COIN_SWAP_DEPOSIT")]
        CoinSwapDeposit,
        /// <summary>
        /// Coin swap withdraw
        /// </summary>
        [Map("COIN_SWAP_WITHDRAW")]
        CoinSwapWithdraw,
        /// <summary>
        /// Position limit increase fee
        /// </summary>
        [Map("POSITION_LIMIT_INCREASE_FEE")]
        PositionLimitIncreaseFee
    }
}
