using Binance.Net.Objects;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    public class DepositStatusConverter: BaseConverter<DepositStatus>
    {
        public DepositStatusConverter(): this(true) { }
        public DepositStatusConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<DepositStatus, string> Mapping => new Dictionary<DepositStatus, string>
        {
            { DepositStatus.Pending, "0" },
            { DepositStatus.Success, "1" }
        };
    }
}
