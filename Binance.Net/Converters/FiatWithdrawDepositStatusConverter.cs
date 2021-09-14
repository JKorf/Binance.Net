using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Binance.Net.Converters
{
    internal class FiatWithdrawDepositStatusConverter : BaseConverter<FiatWithdrawDepositStatus>
    {
        public FiatWithdrawDepositStatusConverter() : this(true) { }
        public FiatWithdrawDepositStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FiatWithdrawDepositStatus, string>> Mapping => new List<KeyValuePair<FiatWithdrawDepositStatus, string>>
        {
            new KeyValuePair<FiatWithdrawDepositStatus, string>(FiatWithdrawDepositStatus.Processing, "Processing"),
            new KeyValuePair<FiatWithdrawDepositStatus, string>(FiatWithdrawDepositStatus.Finished, "Finished"),
            new KeyValuePair<FiatWithdrawDepositStatus, string>(FiatWithdrawDepositStatus.Failed, "Failed"),
            new KeyValuePair<FiatWithdrawDepositStatus, string>(FiatWithdrawDepositStatus.Refunded, "Refunded"),
            new KeyValuePair<FiatWithdrawDepositStatus, string>(FiatWithdrawDepositStatus.Refunding, "Refunding"),
            new KeyValuePair<FiatWithdrawDepositStatus, string>(FiatWithdrawDepositStatus.Successful, "Successful"),
            new KeyValuePair<FiatWithdrawDepositStatus, string>(FiatWithdrawDepositStatus.RefundFailed, "Refund Failed"),
            new KeyValuePair<FiatWithdrawDepositStatus, string>(FiatWithdrawDepositStatus.Expired, "Expired"),
        };
    }
}
