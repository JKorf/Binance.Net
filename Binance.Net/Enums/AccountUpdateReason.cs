using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Account update reason
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountUpdateReason>))]
    public enum AccountUpdateReason
    {
        /// <summary>
        /// ["<c>DEPOSIT</c>"] Deposit
        /// </summary>
        [Map("DEPOSIT")]
        Deposit,
        /// <summary>
        /// ["<c>WITHDRAW</c>"] Withdraw
        /// </summary>
        [Map("WITHDRAW")]
        Withdraw,
        /// <summary>
        /// ["<c>ORDER</c>"] Order
        /// </summary>
        [Map("ORDER")]
        Order,
        /// <summary>
        /// ["<c>FUNDING_FEE</c>"] Funding Fee
        /// </summary>
        [Map("FUNDING_FEE")]
        FundingFee,
        /// <summary>
        /// ["<c>WITHDRAW_REJECT</c>"] Withdraw Reject
        /// </summary>
        [Map("WITHDRAW_REJECT")]
        WithdrawReject,
        /// <summary>
        /// ["<c>ADJUSTMENT</c>"] Adjustment
        /// </summary>
        [Map("ADJUSTMENT")]
        Adjustment,
        /// <summary>
        /// ["<c>INSURANCE_CLEAR</c>"] Insurance Clear
        /// </summary>
        [Map("INSURANCE_CLEAR")]
        InsuranceClear,
        /// <summary>
        /// ["<c>ADMIN_DEPOSIT</c>"] Admin Deposit
        /// </summary>
        [Map("ADMIN_DEPOSIT")]
        AdminDeposit,
        /// <summary>
        /// ["<c>ADMIN_WITHDRAW</c>"] Admin Withdraw
        /// </summary>
        [Map("ADMIN_WITHDRAW")]
        AdminWithdraw,
        /// <summary>
        /// ["<c>MARGIN_TRANSFER</c>"] Margin Transfer
        /// </summary>
        [Map("MARGIN_TRANSFER")]
        MarginTransfer,
        /// <summary>
        /// ["<c>MARGIN_TYPE_CHANGE</c>"] Margin Type Change
        /// </summary>
        [Map("MARGIN_TYPE_CHANGE")]
        MarginTypeChange,
        /// <summary>
        /// ["<c>ASSET_TRANSFER</c>"] Asset Transfer
        /// </summary>
        [Map("ASSET_TRANSFER")]
        AssetTransfer,
        /// <summary>
        /// ["<c>OPTIONS_PREMIUM_FEE</c>"] Options Premium Fee
        /// </summary>
        [Map("OPTIONS_PREMIUM_FEE")]
        OptionsPremiumFee,
        /// <summary>
        /// ["<c>OPTIONS_SETTLE_PROFIT</c>"] Options Settle Profit
        /// </summary>
        [Map("OPTIONS_SETTLE_PROFIT")]
        OptionsSettleProfit,
        /// <summary>
        /// ["<c>AUTO_EXCHANGE</c>"] Auto exchange
        /// </summary>
        [Map("AUTO_EXCHANGE")]
        AutoExchange,
        /// <summary>
        /// ["<c>COIN_SWAP_WITHDRAW</c>"] Coin swap withdraw
        /// </summary>
        [Map("COIN_SWAP_WITHDRAW")]
        CoinSwapWithdraw,
        /// <summary>
        /// ["<c>COIN_SWAP_DEPOSIT</c>"] Coin swap deposit
        /// </summary>
        [Map("COIN_SWAP_DEPOSIT")]
        CoinSwapDeposit
    }
}

