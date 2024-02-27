using System.Collections.Generic;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    internal class ExecutionTypeConverter: BaseConverter<ExecutionType>
    {
        public ExecutionTypeConverter(): this(true) { }
        public ExecutionTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<ExecutionType, string>> Mapping => new List<KeyValuePair<ExecutionType, string>>
        {
            new KeyValuePair<ExecutionType, string>(ExecutionType.New, "NEW"),
            new KeyValuePair<ExecutionType, string>(ExecutionType.Canceled, "CANCELED"),
            new KeyValuePair<ExecutionType, string>(ExecutionType.Replaced, "REPLACED"),
            new KeyValuePair<ExecutionType, string>(ExecutionType.Rejected, "REJECTED"),
            new KeyValuePair<ExecutionType, string>(ExecutionType.Trade, "TRADE"),
            new KeyValuePair<ExecutionType, string>(ExecutionType.Expired, "EXPIRED"),
            new KeyValuePair<ExecutionType, string>(ExecutionType.Amendment, "AMENDMENT"),
            new KeyValuePair<ExecutionType, string>(ExecutionType.Amendment, "TRADE_PREVENTION "),
        };
    }
}
