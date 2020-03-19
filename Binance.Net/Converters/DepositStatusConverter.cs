using Binance.Net.Objects;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class DepositStatusConverter: BaseConverter<DepositStatus>
    {
        public DepositStatusConverter(): this(true) { }
        public DepositStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<DepositStatus, string>> Mapping => new List<KeyValuePair<DepositStatus, string>>
        {
            new KeyValuePair<DepositStatus, string>(DepositStatus.Pending, "0"),
            new KeyValuePair<DepositStatus, string>(DepositStatus.Success, "1"),
            new KeyValuePair<DepositStatus, string>(DepositStatus.Completed, "6"),
        };
    }
}
