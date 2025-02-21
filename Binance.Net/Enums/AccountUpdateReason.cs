using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Account update reason
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountUpdateReason>))] public  enum AccountUpdateReason
    {
        /// <summary>
        /// Deposit
        /// </summary>
        [Map("DEPOSIT")]
        Deposit,
        /// <summary>
        /// Withdraw
        /// </summary>
        [Map("WITHDRAW")]
        Withdraw,
        /// <summary>
        /// Order
        /// </summary>
        [Map("ORDER")]
        Order,
        /// <summary>
        /// Funding Fee
        /// </summary>
        [Map("FUNDING_FEE")]
        FundingFee,
        /// <summary>
        /// Withdraw Reject
        /// </summary>
        [Map("WITHDRAW_REJECT")]
        WithdrawReject,
        /// <summary>
        /// Adjustment
        /// </summary>
        [Map("ADJUSTMENT")]
        Adjustment,
        /// <summary>
        /// Insurance Clear
        /// </summary>
        [Map("INSURANCE_CLEAR")]
        InsuranceClear,
        /// <summary>
        /// Admin Deposit
        /// </summary>
        [Map("ADMIN_DEPOSIT")]
        AdminDeposit,
        /// <summary>
        /// Admin Withdraw
        /// </summary>
        [Map("ADMIN_WITHDRAW")]
        AdminWithdraw,
        /// <summary>
        /// Margin Transfer
        /// </summary>
        [Map("MARGIN_TRANSFER")]
        MarginTransfer,
        /// <summary>
        /// Margin Type Change
        /// </summary>
        [Map("MARGIN_TYPE_CHANGE")]
        MarginTypeChange,
        /// <summary>
        ///  Asset Transfer
        /// </summary>
        [Map("ASSET_TRANSFER")]
        AssetTransfer,
        /// <summary>
        /// Options Premium Fee
        /// </summary>
        [Map("OPTIONS_PREMIUM_FEE")]
        OptionsPremiumFee,
        /// <summary>
        /// Options Settle Profit
        /// </summary>
        [Map("OPTIONS_SETTLE_PROFIT")]
        OptionsSettleProfit,
        /// <summary>
        /// Auto exchange
        /// </summary>
        [Map("AUTO_EXCHANGE")]
        AutoExchange,
        /// <summary>
        /// Coin swap withdraw
        /// </summary>
        [Map("COIN_SWAP_WITHDRAW")]
        CoinSwapWithdraw,
        /// <summary>
        /// Coin swap deposit
        /// </summary>
        [Map("COIN_SWAP_DEPOSIT")]
        CoinSwapDeposit
    }
}
