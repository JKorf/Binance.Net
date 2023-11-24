using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Binance.Net.Converters
{
    internal class ConvertOrderStatusConverter : BaseConverter<ConvertOrderStatus>
    {
        public ConvertOrderStatusConverter() : this(true) { }
        public ConvertOrderStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<ConvertOrderStatus, string>> Mapping => new List<KeyValuePair<ConvertOrderStatus, string>>
        {
            new KeyValuePair<ConvertOrderStatus, string>(ConvertOrderStatus.AcceptSuccess, "ACCEPT_SUCCESS"),
            new KeyValuePair<ConvertOrderStatus, string>(ConvertOrderStatus.Success, "SUCCESS"),
            new KeyValuePair<ConvertOrderStatus, string>(ConvertOrderStatus.Process, "PROCESS"),
            new KeyValuePair<ConvertOrderStatus, string>(ConvertOrderStatus.Fail, "FAIL"),
        };
    }
}
