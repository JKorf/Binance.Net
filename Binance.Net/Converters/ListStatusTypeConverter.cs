using Binance.Net.Objects;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class ListStatusTypeConverter : BaseConverter<ListStatusType>
    {
        public ListStatusTypeConverter(): this(true) { }
        public ListStatusTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<ListStatusType, string>> Mapping => new List<KeyValuePair<ListStatusType, string>>
        {
            new KeyValuePair<ListStatusType, string>(ListStatusType.Response, "RESPONSE"),
            new KeyValuePair<ListStatusType, string>(ListStatusType.ExecutionStarted, "EXEC_STARTED"),
            new KeyValuePair<ListStatusType, string>(ListStatusType.Done, "ALL_DONE"),
        };
    }
}
