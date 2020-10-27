using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Binance.Net.Converters
{
    internal class AccountUpdateReasonConverter : BaseConverter<AccountUpdateReason>
    {
        public AccountUpdateReasonConverter() : this(true) { }
        public AccountUpdateReasonConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<AccountUpdateReason, string>> Mapping => new List<KeyValuePair<AccountUpdateReason, string>>
        {
            new KeyValuePair<AccountUpdateReason, string>(AccountUpdateReason.Deposit, "DEPOSIT"),
            new KeyValuePair<AccountUpdateReason, string>(AccountUpdateReason.Withdraw, "WITHDRAW"),
            new KeyValuePair<AccountUpdateReason, string>(AccountUpdateReason.Order, "ORDER"),
            new KeyValuePair<AccountUpdateReason, string>(AccountUpdateReason.FundingFee, "FUNDING_FEE"),
            new KeyValuePair<AccountUpdateReason, string>(AccountUpdateReason.WithdrawReject, "WITHDRAW_REJECT"),
            new KeyValuePair<AccountUpdateReason, string>(AccountUpdateReason.Adjustment, "ADJUSTMENT"),
            new KeyValuePair<AccountUpdateReason, string>(AccountUpdateReason.InsuranceClear, "INSURANCE_CLEAR"),
            new KeyValuePair<AccountUpdateReason, string>(AccountUpdateReason.AdminDeposit, "ADMIN_DEPOSIT"),
            new KeyValuePair<AccountUpdateReason, string>(AccountUpdateReason.AdminWithdraw, "ADMIN_WITHDRAW"),
            new KeyValuePair<AccountUpdateReason, string>(AccountUpdateReason.MarginTransfer, "MARGIN_TRANSFER"),
            new KeyValuePair<AccountUpdateReason, string>(AccountUpdateReason.MarginTypeChange, "MARGIN_TYPE_CHANGE"),
            new KeyValuePair<AccountUpdateReason, string>(AccountUpdateReason.AssetTransfer, "ASSET_TRANSFER"),
            new KeyValuePair<AccountUpdateReason, string>(AccountUpdateReason.OptionsPremiumFee, "OPTIONS_PREMIUM_FEE"),
            new KeyValuePair<AccountUpdateReason, string>(AccountUpdateReason.OptionsSettleProfit, "OPTIONS_SETTLE_PROFIT"),
        };
    }
}
