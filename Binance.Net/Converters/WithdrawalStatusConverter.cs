using Binance.Net.Objects;
using System.Collections.Generic;
using CryptoExchange.Net;

namespace Binance.Net.Converters
{
    public class WithdrawalStatusConverter : BaseConverter<WithdrawalStatus>
    {
        public WithdrawalStatusConverter(): this(true) { }
        public WithdrawalStatusConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<WithdrawalStatus, string> Mapping => new Dictionary<WithdrawalStatus, string>()
        {
            { WithdrawalStatus.EmailSend, "0" },
            { WithdrawalStatus.Canceled, "1" },
            { WithdrawalStatus.AwaitingApproval, "2" },
            { WithdrawalStatus.Rejected, "3" },
            { WithdrawalStatus.Proccessing, "4" },
            { WithdrawalStatus.Failure, "5" },
            { WithdrawalStatus.Completed, "6" }
        };
    }
}
