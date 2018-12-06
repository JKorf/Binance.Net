using System.Collections.Generic;
using Binance.Net.Objects;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    public class ExecutionTypeConverter: BaseConverter<ExecutionType>
    {
        public ExecutionTypeConverter(): this(true) { }
        public ExecutionTypeConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<ExecutionType, string> Mapping => new Dictionary<ExecutionType, string>
        {
            { ExecutionType.New, "NEW" },
            { ExecutionType.Canceled, "CANCELED" },
            { ExecutionType.Replaced, "REPLACED" },
            { ExecutionType.Rejected, "REJECTED" },
            { ExecutionType.Trade, "TRADE" },
            { ExecutionType.Expired, "EXPIRED" }
        };
    }
}
