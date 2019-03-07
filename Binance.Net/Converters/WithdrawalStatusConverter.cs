using Binance.Net.Objects;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    public class WithdrawalStatusConverter : BaseConverter<WithdrawalStatus>
    {
        public WithdrawalStatusConverter() : this(true)
        {
        }

        public WithdrawalStatusConverter(bool quotes) : base(quotes)
        {
        }

        protected override List<KeyValuePair<WithdrawalStatus, string>> Mapping => new List<KeyValuePair<WithdrawalStatus, string>>
        {
            new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.EmailSend, "0"),
            new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.Canceled, "1"),
            new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.AwaitingApproval, "2"),
            new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.Rejected, "3"),
            new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.Processing, "4"),
            new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.Failure, "5"),
            new KeyValuePair<WithdrawalStatus, string>(WithdrawalStatus.Completed, "6")
        };
    }
}
